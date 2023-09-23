using System.Reflection.Emit;
using WinFormsLibrary;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        DataGridViewUserControl dataGridViewUserControl;
        TextBoxUserControl textBoxUserControl;
        ComponentTable componentTable;
        public Form()
        {
            InitializeComponent();
            dataGridViewUserControl = new();
            textBoxUserControl = new();
            componentTable = new();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // DataGridView

            dataGridViewUserControl.Location = new Point(250, 10);
            Controls.Add(dataGridViewUserControl);

            // ��������� ��������
            List<ColumnConfig> columnConfigs = new List<ColumnConfig>
            {
                new ColumnConfig { Header = "���", DataPropertyName = "FirstName", Width = 100, Visible = true },
                new ColumnConfig { Header = "�������", DataPropertyName = "LastName", Width = 100, Visible = true },
                new ColumnConfig { Header = "�������", DataPropertyName = "Age", Width = 70, Visible = true }
            };
            dataGridViewUserControl.ConfigureColumns(columnConfigs);

            // ���������� �������
            List<Person> people = new List<Person>
            {
                new Person { FirstName = "����", LastName = "������", Age = 30 },
                new Person { FirstName = "����", LastName = "������", Age = 25 },
                new Person { FirstName = "����", LastName = "��������", Age = 28 }
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

        // ��������� ���������� ������� � ���������������� DataGridView
        private void DataGridViewUserControl_SelectionChanged(object sender, EventArgs e)
        {
            Person? selectedPerson = dataGridViewUserControl?.GetObjectFromSelectedRow<Person>();

            if (selectedPerson != null)
            {
                MessageBox.Show($"������: {selectedPerson.FirstName} {selectedPerson.LastName}, �������: {selectedPerson.Age}");
            }
            else
            {
                MessageBox.Show("������ �� �������.");
            }
        }

        // ���������� ������� ��� ��������� �������� � ���������������� TextBox
        private void TextBoxUserControl_ValueChanged(object sender, EventArgs e)
        {
            TextBoxUserControl textBoxUserControl = (TextBoxUserControl)sender;
            DateTime? selectedDate = textBoxUserControl.SelectedDate;

            if (selectedDate.HasValue)
            {
                MessageBox.Show($"��������� ����: {selectedDate.Value.ToShortDateString()}");
            }
        }

        // ���������� ������� ��� ����� �������� � ���������������� ListBox
        private void ListBoxUserControl_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBoxUserControl listBoxUserControl = (ListBoxUserControl)sender;

            MessageBox.Show("Selected Value Changed to: " + listBoxUserControl.SelectedValue);
        }

        private void ButtonGet_Click(object sender, EventArgs e)
        {
            DateTime? selectedDate = textBoxUserControl.SelectedDate;

            if (selectedDate.HasValue)
            {
                MessageBox.Show($"��������� ����: {selectedDate.Value.ToShortDateString()}");
            }
        }

        private void ButtonTable_Click(object sender, EventArgs e)
        {
            List<string[,]> list1 = new();
            string[,] arr1 = { { "1", "2" }, { "3", "4" } };
            string[,] arr2 = { { "7", "8" }, { "9", "10" } };
            string[,] arr3 = { { "a", "b" }, { "c", "d" } };
            list1.Add(arr1);
            list1.Add(arr2);
            list1.Add(arr3);
            List<string[,]> list2 = new();
            string[,] arr4 = { { "asd", "2" }, { "3", "4" } };
            string[,] arr5 = { { "7", "8" }, { "9", "10" } };
            string[,] arr6 = { { "a", "b" }, { "c", "d" } };
            list2.Add(arr6);
            list2.Add(arr5);
            list2.Add(arr4);
            componentTable.CreateTableReport(new List<ExcelInfo>() {
                new()
                {
                    FileName = "D:\\test1.xlsx",
                    Title = "title1",
                    Tables = list1
                },
                new()
                {
                    FileName = "D:\\test2.xlsx",
                    Title = "title2",
                    Tables = list2
                }
            });
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var props = new string[] { "Age", "FirstName", "LastName" };
            var titles = new string[] { "�������", "���", "�������" };

            var people = new Person[]
            {
                new Person { FirstName = "John", LastName = "Doe", Age = 30 },
                new Person { FirstName = "Alice", LastName = "Smith", Age = 25 },
            };

            double[] widths = { 10, 15, 20 };
            double[] heights = { 25, 15, 15 };
            componentHardTable.CreateHardTableReport(new ExcelInfoTable<Person>
            {
                FileName = "D:\\output.xlsx",
                Title = "main title",
                Titles = titles,
                Data = people,
                Props = props,
                RowHeight = heights,
                ColumnWidth = widths
            });
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}