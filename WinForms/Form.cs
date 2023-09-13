namespace WinFormsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        DataGridViewUserControl dataGridViewUserControl;
        TextBoxUserControl textBoxUserControl;

        public Form()
        {
            InitializeComponent();
            dataGridViewUserControl = new();
            textBoxUserControl = new();
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
    }
}