using System.Reflection;

namespace WinFormsApp
{
    public partial class DataGridViewUserControl : UserControl
    {
        public DataGridViewUserControl()
        {
            InitializeComponent();

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.MultiSelect = false;
        }

        // Метод для настройки столбцов DataGridView
        public void ConfigureColumns(List<ColumnConfig> columnConfigs)
        {
            dataGridView.Columns.Clear();

            foreach (var config in columnConfigs)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = config.Header;
                column.DataPropertyName = config.DataPropertyName;
                column.Width = config.Width;
                column.Visible = config.Visible;
                dataGridView.Columns.Add(column);
            }
        }

        // Метод для очистки строк DataGridView
        public void ClearRows()
        {
            dataGridView.Rows.Clear();
        }

        // Свойство для установки и получения индекса выбранной строки
        public int SelectedRowIndex
        {
            get
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    return dataGridView.SelectedRows[0].Index;
                }
                return -1;
            }
            set
            {
                if (value >= 0 && value < dataGridView.RowCount)
                {
                    dataGridView.Rows[value].Selected = true;
                }
            }
        }

        // Метод для заполнения значения ячейки в DataGridView
        public void FillData<T>(T item, int row, int column)
        {
            if (column >= dataGridView.ColumnCount)
            {
                throw new ArgumentOutOfRangeException("column");
            }

            if (row >= dataGridView.RowCount)
            {
                dataGridView.Rows.Add(row - dataGridView.RowCount + 1);
            }

            string propertyName = dataGridView.Columns[column].DataPropertyName;

            PropertyInfo? prop = typeof(T).GetProperty(propertyName);
            if (prop != null)
            {
                object? propValue = prop.GetValue(item, null);
                dataGridView.Rows[row].Cells[column].Value = propValue;
            }
        }

        // Публичный параметризованный метод для получения объекта из выбранной строки
        public T? GetObjectFromSelectedRow<T>()
        {
            if (SelectedRowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                T obj = Activator.CreateInstance<T>();

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    string propertyName = dataGridView.Columns[cell.ColumnIndex].DataPropertyName;
                    PropertyInfo? prop = typeof(T).GetProperty(propertyName);

                    if (prop != null && cell.Value != null)
                    {
                        object cellValue = Convert.ChangeType(cell.Value, prop.PropertyType);
                        prop.SetValue(obj, cellValue);
                    }
                }

                return obj;
            }

            return default;
        }

        // Событие, вызываемое при смене значения в DataGridView
        private event EventHandler? selectedRowChanged;
        public event EventHandler? SelectedRowChanged
        {
            add => selectedRowChanged += value;
            remove => selectedRowChanged -= value;
        }

        // Обработчик события SelectionChanged для DataGridView
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            selectedRowChanged?.Invoke(this, e);
        }
    }
}
