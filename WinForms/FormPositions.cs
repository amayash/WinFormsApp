using EmployeeTracking.DTOs;
using EmployeeTracking.Storages;

namespace WinForms
{
    public partial class FormPositions : Form
    {
        /*private readonly IPositionStorage positionStorage;
        public FormPositions(IPositionStorage positionStorage)
        {
            InitializeComponent();
            this.positionStorage = positionStorage;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.MultiSelect = true;
            LoadData();
        }

        private void LoadData()
        {
            dataGridView.DataSource = positionStorage.GetFullList();
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((int)dataGridView.Rows[e.RowIndex].Cells[0].Value == 0)
                {
                    *//*positionStorage.Insert(new() { Id = 0, 
                        Name = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()! 
                    });*//*
                }
                else
                {
                    positionStorage.Update(new()
                    {
                        Id = (int)dataGridView.Rows[e.RowIndex].Cells[0].Value,
                        Name = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()!
                    });
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellEventArgs e)
        {
            // Сохранение изменений при завершении редактирования ячейки
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                string? name = row.Cells["Name"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(name))
                {
                    // Запрещаем сохранение пустого имени
                    MessageBox.Show("Нельзя сохранить запись с пустым именем!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadData(); // Восстанавливаем исходные данные
                }
                else
                {
                    var model = new PositionBindingModel
                    {
                        Id = id,
                        Name = name
                    };
                    if (model.Id == 0)
                    {
                        positionStorage.Insert(model);
                    }
                    else
                    {
                        positionStorage.Update(model);
                    }
                    LoadData();
                }
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                var list = positionStorage.GetFullList();
                list.Add(new());
                dataGridView.DataSource = list;
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранные должности?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var list = dataGridView.Rows.Cast<DataGridViewRow>()
                        .Where(row => row.Selected)
                        .Select(x => new PositionBindingModel()
                        {
                            Id = (int)(x.Cells["Id"].Value)
                        })
                        .ToList();
                    positionStorage.DeleteList(list);

                    e.Handled = true;

                    LoadData();
                }
            }
        }*/

        public readonly IPositionStorage _statusDirectory;
        public FormPositions(IPositionStorage statusDirectory)
        {
            InitializeComponent();
            _statusDirectory = statusDirectory;
        }

        private void FormPosition_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = _statusDirectory.GetFullList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns["Id"].Visible = false;
                    dataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                dataGridView.KeyDown += DataGridView_KeyDown;
                dataGridView.CellValueChanged += DataGridView_CellValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                // Добавление новой строки при нажатии на клавишу Insert
                var list = _statusDirectory.GetFullList();
                list.Add(new());
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns["Id"].Visible = false;
                    dataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                e.Handled = true;

            }
            else if (e.KeyCode == Keys.Delete)
            {
                // Удаление выбранных строк при нажатии на клавишу Delete
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранные записи?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView.SelectedRows)
                        {
                            if (!row.IsNewRow)
                            {
                                int id = Convert.ToInt32(row.Cells["Id"].Value);
                                _statusDirectory.Delete(new() { Id = id });
                            }
                        }
                        LoadData();
                    }
                }
                e.Handled = true;
            }
        }
        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Сохранение изменений при завершении редактирования ячейки
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                string? name = row.Cells["Name"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(name))
                {
                    // Запрещаем сохранение пустого имени
                    MessageBox.Show("Нельзя сохранить запись с пустым именем!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadData(); // Восстанавливаем исходные данные
                }
                else
                {
                    var model = new PositionBindingModel
                    {
                        Id = id,
                        Name = name
                    };
                    if (model.Id == 0)
                    {
                        _statusDirectory.Insert(model);
                    }
                    else
                    {
                        _statusDirectory.Update(model);
                    }
                    LoadData();
                }
            }
        }
    }
}
