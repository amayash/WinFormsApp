using EmployeeTracking.DTOs;
using EmployeeTracking.Models;
using EmployeeTracking.Storages;

namespace WinForms
{
    public partial class FormEmployee : Form
    {
        private readonly IEmployeeStorage employeeStorage;
        private readonly IPositionStorage positionStorage;
        private int? _id;
        public int Id { set { _id = value; } }
        public FormEmployee(IEmployeeStorage employeeStorage, IPositionStorage positionStorage)
        {
            InitializeComponent();
            this.employeeStorage = employeeStorage;
            this.positionStorage = positionStorage;

            string[] departments = { "Department 1", "Department 2", "Department 3" };
            foreach (var item in departments)
                listBoxUserControlDepartments.Strings.Add(item);

            var positions = positionStorage.GetFullList();
            foreach (var item in positions)
                customCheckedListBoxPositions.Items.Add(item.Name);
            dataGridViewEmployeePositions.DataSource = new List<Position>();

            dataGridViewEmployeePositions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEmployeePositions.Columns["Id"].Visible = false;
            dataGridViewEmployeePositions.Columns["GetViewModel"].Visible = false;
            dataGridViewEmployeePositions.AllowUserToAddRows = false;

            customNumericUpDown.MinValue = 1;
            customNumericUpDown.MaxValue = 30;
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                try
                {
                    var view = employeeStorage.GetElement(_id.Value);
                    if (view != null)
                    {
                        textBoxFullName.Text = view.FullName;
                        int indexToSelect = listBoxUserControlDepartments.Strings.IndexOf(view.Department);
                        listBoxUserControlDepartments.SelectedValue = listBoxUserControlDepartments.Strings[indexToSelect].ToString();

                        var listPos = new List<Position>();
                        foreach (var pos in view.Positions)
                        {
                            var elem = positionStorage.GetElement(new() { Name = pos });
                            if (elem != null)
                            {
                                var temp = Position.Create(new() { Id = elem.Id, Name = elem.Name });
                                listPos.Add(temp);
                            }
                        }
                        dataGridViewEmployeePositions.DataSource = listPos;

                        customNumericUpDown.Value = view.YearsOfWork;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void buttonEditPosition_Click(object sender, EventArgs e)
        {
            var selectedPosition = customCheckedListBoxPositions.SelectedItem;

            if (!string.IsNullOrEmpty(selectedPosition))
            {
                bool employeeHasPosition = false;
                int rowIndex = 0;
                var selectedPositionModel = positionStorage.GetElement(new() { Name = selectedPosition });

                if (selectedPositionModel == null)
                    return;
                // Перебираем строки и столбцы DataGridView
                foreach (DataGridViewRow row in dataGridViewEmployeePositions.Rows)
                {
                    var cell = row.Cells[0];
                    if (cell.Value != null)
                    {
                        if ((int)cell.Value == selectedPositionModel.Id)
                        {
                            employeeHasPosition = true;
                            break;
                        }
                    }
                    rowIndex++;
                }
                List<Position> dataSource = (List<Position>)dataGridViewEmployeePositions.DataSource;

                if (employeeHasPosition)
                {
                    dataSource.RemoveAt(rowIndex);
                }
                else
                {
                    Position pos = Position.Create(new() { Id = selectedPositionModel.Id, Name = selectedPositionModel.Name });
                    dataSource.Add(pos);
                }
                dataGridViewEmployeePositions.DataSource = new List<Position>();
                dataGridViewEmployeePositions.DataSource = dataSource;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFullName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(listBoxUserControlDepartments.SelectedValue))
            {
                MessageBox.Show("Выберите подразделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridViewEmployeePositions.RowCount == 0)
            {
                MessageBox.Show("Заполните должности сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!string.IsNullOrEmpty(customNumericUpDown.Error))
            {
                MessageBox.Show(customNumericUpDown.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var model = new EmployeeBindingModel
                {
                    Id = _id ?? 0,
                    FullName = textBoxFullName.Text,
                    Positions = dataGridViewEmployeePositions.Rows.Cast<DataGridViewRow>()
                        .Select(row => row.Cells[1].Value.ToString())
                        .ToList(),
                    YearsOfWork = (int)customNumericUpDown.Value,
                    Department = listBoxUserControlDepartments.SelectedValue
                };
                var operationResult = _id.HasValue ? employeeStorage.Update(model) : employeeStorage.Insert(model);
                if (operationResult == null)
                {
                    throw new Exception("Ошибка при сохранении.");
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
