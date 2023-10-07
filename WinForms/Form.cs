using WinFormsLibrary;
using WinFormsLibrary.OfficePackage.HelperEnums;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        DataGridViewUserControl dataGridViewUserControl;
        TextBoxUserControl textBoxUserControl;
        ComponentTable componentTable;
        ComponentChart componentChart;
        public Form()
        {
            InitializeComponent();
            dataGridViewUserControl = new();
            textBoxUserControl = new();
            componentTable = new();
            componentChart = new();
        }
        /// <summary>
        /// Метод, вызываемый после загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            // DataGridView

            dataGridViewUserControl.Location = new Point(250, 10);
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
            foreach (var item in items)
                listBoxUserControl.Strings.Add(item);

            listBoxUserControl.SelectedValueChanged += ListBoxUserControl_SelectedValueChanged;

            // TextBox

            textBoxUserControl.Location = new Point(10, 10);
            Controls.Add(textBoxUserControl);

            textBoxUserControl.ValueChanged += TextBoxUserControl_ValueChanged;
        }
        /// <summary>
        /// Получение выбранного объекта в пользовательском DataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Обработчик события при изменении значения в пользовательском TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxUserControl_ValueChanged(object sender, EventArgs e)
        {
            TextBoxUserControl textBoxUserControl = (TextBoxUserControl)sender;
            DateTime? selectedDate = textBoxUserControl.SelectedDate;

            if (selectedDate.HasValue)
            {
                MessageBox.Show($"Выбранная дата: {selectedDate.Value.ToShortDateString()}");
            }
        }
        /// <summary>
        /// Обработчик события при смене значения в пользовательском ListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxUserControl_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBoxUserControl listBoxUserControl = (ListBoxUserControl)sender;

            MessageBox.Show("Selected Value Changed to: " + listBoxUserControl.SelectedValue);
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "get".
        /// Отображает выбранную дату в диалоговом окне сообщения, если дата была выбрана.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGet_Click(object sender, EventArgs e)
        {
            DateTime? selectedDate = textBoxUserControl.SelectedDate;

            if (selectedDate.HasValue)
            {
                MessageBox.Show($"Выбранная дата: {selectedDate.Value.ToShortDateString()}");
            }
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "table".
        /// Создает отчет с таблицей в формате Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTable_Click(object sender, EventArgs e)
        {
            List<string[,]> list1 = new();
            string[,] arr1 = { { "1", "2" }, { "3", "4" } };
            string[,] arr2 = { { "7", "8" }, { "9", "10" } };
            string[,] arr3 = { { "a", "b" }, { "c", "d" } };
            list1.Add(arr1);
            list1.Add(arr2);
            list1.Add(arr3);
            componentTable.CreateTableReport(new ExcelInfo()
            {
                FileName = "C:\\myFiles\\test1.xlsx",
                Title = "title1",
                Tables = list1
            });
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "hard table".
        /// Создает отчет с таблицей в формате Excel, используя переданные данные об объекте.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonHardTable_Click(object sender, EventArgs e)
        {
            var props = new string[] { "Age", "FirstName", "LastName" };
            var titles = new string[] { "Возраст", "Имя", "Фамилия" };

            var people = new Person[]
            {
                new Person { FirstName = "John", LastName = "Doe", Age = 30 },
                new Person { FirstName = "Alice", LastName = "Smith", Age = 25 },
            };

            double[] widths = { 10, 15, 20 };
            double[] heights = { 25, 15, 15 };
            componentHardTable.CreateHardTableReport(new ExcelInfoTable<Person>
            {
                FileName = "C:\\myFiles\\test2.xlsx",
                Title = "main title",
                Titles = titles,
                Data = people,
                Props = props,
                RowHeight = heights,
                ColumnWidth = widths
            });
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "chart".
        /// Создает отчет в формате Excel с гистограммой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChart_Click(object sender, EventArgs e)
        {
            string[] seriesNames = { "1", "2", "3" };
            double[] data = { 5, 5, 10 };
            var serAndData = new List<(string, double)>();
            for (int i = 0; i < seriesNames.Length; i++)
            {
                serAndData.Add((seriesNames[i], data[i]));
            }
            componentChart.CreateChartReport(new ExcelInfoChart()
            {
                FileName = "C:\\myFiles\\test3.xlsx",
                Title = "Диаграммка",
                TitleChart = "Гистограммка",
                LegendPosition = LegendPosition.Bottom,
                SeriesAndData = serAndData
            });
        }
    }
}