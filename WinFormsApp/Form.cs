namespace WinFormsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        DataGridViewUserControl dataGridViewUserControl;

        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // DataGridView

            dataGridViewUserControl = new();
            dataGridViewUserControl.Location = new Point(180, 10);
            Controls.Add(dataGridViewUserControl);

            // Настройка столбцов
            List<ColumnConfig> columnConfigs = new List<ColumnConfig>
            {
                new ColumnConfig { Header = "Имя", DataPropertyName = "FirstName", Width = 100, Visible = true },
                new ColumnConfig { Header = "Фамилия", DataPropertyName = "LastName", Width = 100, Visible = true },
                new ColumnConfig { Header = "Возраст", DataPropertyName = "Age", Width = 70, Visible = true }
            };
            dataGridViewUserControl.ConfigureColumns(columnConfigs);

            // Заполнение данными
            List<Person> people = new List<Person>
            {
                new Person { FirstName = "Иван", LastName = "Иванов", Age = 30 },
                new Person { FirstName = "Петр", LastName = "Петров", Age = 25 },
                new Person { FirstName = "Анна", LastName = "Сидорова", Age = 28 }
            };

            for (int i = 0; i < people.Count; i++)
            {
                dataGridViewUserControl.FillData(people[i], i, 0);
                dataGridViewUserControl.FillData(people[i], i, 1);
                dataGridViewUserControl.FillData(people[i], i, 2);
            }

            dataGridViewUserControl.SelectedRowChanged += DataGridViewUserControl_SelectionChanged;

            // ListBox

            ListBoxUserControl listBoxUserControl = new();
            listBoxUserControl.Location = new Point(10, 50);
            Controls.Add(listBoxUserControl);

            string[] items = { "Item 1", "Item 2", "Item 3" };
            listBoxUserControl.Strings = items;

            listBoxUserControl.SelectedValueChanged += ListBoxUserControl_SelectedValueChanged;

            // TextBox

            TextBoxUserControl textBoxUserControl = new();
            textBoxUserControl.Location = new Point(10, 10);
            Controls.Add(textBoxUserControl);

            textBoxUserControl.ValueChanged += TextBoxUserControl_ValueChanged;
        }

        // Получение выбранного объекта в пользовательском DataGridView
        private void DataGridViewUserControl_SelectionChanged(object sender, EventArgs e)
        {
            Person? selectedPerson = dataGridViewUserControl?.GetObjectFromSelectedRow<Person>();

            if (selectedPerson != null)
            {
                MessageBox.Show($"Выбран: {selectedPerson.FirstName} {selectedPerson.LastName}, Возраст: {selectedPerson.Age}");
            }
            else
            {
                MessageBox.Show("Ничего не выбрано.");
            }
        }

        // Обработчик события при изменении значения в пользовательском TextBox
        private void TextBoxUserControl_ValueChanged(object sender, EventArgs e)
        {
            TextBoxUserControl textBoxUserControl = (TextBoxUserControl)sender;
            DateTime? selectedDate = textBoxUserControl.SelectedDate;

            if (selectedDate.HasValue)
            {
                MessageBox.Show($"Выбранная дата: {selectedDate.Value.ToShortDateString()}");
            }
        }

        // Обработчик события при смене значения в пользовательском ListBox
        private void ListBoxUserControl_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBoxUserControl listBoxUserControl = (ListBoxUserControl)sender;

            MessageBox.Show("Selected Value Changed to: " + listBoxUserControl.SelectedValue);
        }
    }
}