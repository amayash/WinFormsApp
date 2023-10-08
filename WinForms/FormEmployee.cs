using EmployeeTracking.DTOs;
using EmployeeTracking.Models;
using EmployeeTracking.Storages;

namespace WinForms
{
    public partial class FormEmployee : Form
    {
        private readonly IEmployeeStorage employeeStorage;
        private readonly IPositionStorage positionStorage;
        private bool edited;
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

        private void InputChanged(object sender, EventArgs e)
        {
            edited = true;
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
                            Position temp;
                            if (elem != null)
                            {
                                temp = Position.Create(new() { Id = elem.Id, Name = elem.Name });
                            }
                            else
                            {
                                temp = Position.Create(new() { Id = 0, Name = pos });
                            }
                            listPos.Add(temp);
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
            textBoxFullName.TextChanged += InputChanged;
            customNumericUpDown.ElementChanged += InputChanged;
            listBoxUserControlDepartments.SelectedValueChanged += InputChanged;
        }

        private void ButtonAddPosition_Click(object sender, EventArgs e)
        {
            var selectedPosition = customCheckedListBoxPositions.SelectedItem;

            if (!string.IsNullOrEmpty(selectedPosition))
            {
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
                            return;
                        }
                    }
                }
                List<Position> dataSource = (List<Position>)dataGridViewEmployeePositions.DataSource;

                Position pos = Position.Create(new() { Id = selectedPositionModel.Id, Name = selectedPositionModel.Name });
                dataSource.Add(pos);

                RefreshDataGridView(dataSource);

                InputChanged(sender, e);
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (edited)
            {
                DialogResult result = MessageBox.Show("Есть несохраненные изменения. Хотите сохранить перед закрытием?",
                    "Предупреждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ButtonSave_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    // Отменить закрытие формы
                    e.Cancel = true;
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(e))
                return;

            try
            {
                var model = new EmployeeBindingModel
                {
                    Id = _id ?? 0,
                    FullName = textBoxFullName.Text,
                    Positions = dataGridViewEmployeePositions.Rows.Cast<DataGridViewRow>()
                        .Select(row => row.Cells["Name"].Value.ToString())
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            edited = false;
        }

        private void ButtonDeletePosition_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployeePositions.SelectedRows.Count == 0)
                return;

            int rowIndex = dataGridViewEmployeePositions.SelectedRows[0].Index;

            if (rowIndex < 0)
                return;

            List<Position> dataSource = (List<Position>)dataGridViewEmployeePositions.DataSource;

            if (rowIndex >= 0 && rowIndex < dataSource.Count)
            {
                dataSource.RemoveAt(rowIndex);
            }

            RefreshDataGridView(dataSource);
            InputChanged(sender, e);
        }

        private bool ValidateInput(EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFullName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelFormClosing(e);
                return false;
            }
            if (string.IsNullOrEmpty(listBoxUserControlDepartments.SelectedValue))
            {
                MessageBox.Show("Выберите подразделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelFormClosing(e);
                return false;
            }
            if (dataGridViewEmployeePositions.RowCount == 0)
            {
                MessageBox.Show("Заполните должности сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelFormClosing(e);
                return false;
            }
            if (dataGridViewEmployeePositions.RowCount > 5)
            {
                MessageBox.Show("Максимум должностей у сотрудника: 5", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelFormClosing(e);
                return false;
            }
            if (!string.IsNullOrEmpty(customNumericUpDown.Error))
            {
                MessageBox.Show(customNumericUpDown.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelFormClosing(e);
                return false;
            }
            return true;
        }

        private void RefreshDataGridView(List<Position> dataSource)
        {
            dataGridViewEmployeePositions.DataSource = new List<Position>();
            dataGridViewEmployeePositions.DataSource = dataSource;
        }

        private void CancelFormClosing(EventArgs e)
        {
            if (e is FormClosingEventArgs formClosingEventArgs)
            {
                formClosingEventArgs.Cancel = true;
            }
        }
    }
}
