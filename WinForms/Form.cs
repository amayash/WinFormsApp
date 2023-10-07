using CustomNonVisualComponent;
using EmployeeTracking.DTOs;
using EmployeeTracking.Storages;
using NonVisualComponents;
using System.Reflection;
using WinForms;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        IEmployeeStorage _employeeStorage;
        public Form(IEmployeeStorage employeeStorage)
        {
            InitializeComponent();
            _employeeStorage = employeeStorage;
            customListBox.FillTemplateProperties("������������� @Id@, ��� ���������� @FullName@, ��������� @PositionsStr@, ������������� @Department@, ���� ������ @YearsOfWork@", '@', '@');
        }


        /// <summary>
        /// �����, ���������� ����� �������� �����.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var list = _employeeStorage.GetFullList();
            customListBox.ClearCustomListBox();
            try
            {

                for (int i = 0; i < list.Count; i++)
                {
                    foreach (PropertyInfo propertyInfo in list[i].GetType().GetProperties())
                    {
                        customListBox.AddToList(list[i], i, propertyInfo.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormEmployee));
            if (service is FormEmployee form)
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void ������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormEmployee));
            if (service is FormEmployee form)
            {
                EmployeeListBoxViewModel obj = customListBox.GetObjectFromStr<EmployeeListBoxViewModel>();
                form.Id = Convert.ToInt32(obj.Id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (customListBox.CurrentRow >= 0)
            {
                EmployeeListBoxViewModel obj = customListBox.GetObjectFromStr<EmployeeListBoxViewModel>();
                int id = Convert.ToInt32(obj.Id);
                _employeeStorage.Delete(new() { Id = id });
                LoadData();
            }
        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormPositions));
            if (service is FormPositions form)
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void �������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (customListBox.CurrentRow < 0)
            {
                MessageBox.Show("�������� ����������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EmployeeListBoxViewModel obj = customListBox.GetObjectFromStr<EmployeeListBoxViewModel>();
            int id = Convert.ToInt32(obj.Id);
            var empl = _employeeStorage.GetElement(id);

            if (empl == null)
                return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    List<string[,]> list1 = new();
                    string[,] arr1 = new string[1, empl.Positions.Count];

                    for (int i = 0; i < empl.Positions.Count; i++)
                    {
                        arr1[0, i] = empl.Positions[i];
                    }

                    list1.Add(arr1);

                    componentTable.CreateTableReport(new ExcelInfo()
                    {
                        FileName = filePath,
                        Title = empl.FullName,
                        Tables = list1
                    });

                    MessageBox.Show("���������� ������ �������", "���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void c��������������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word Files (*.docx)|*.docx|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    var employees = _employeeStorage.GetFullList();
                    TableInfo<EmployeeViewModel> list = new TableInfo<EmployeeViewModel>()
                    {
                        FileName = filePath,
                        Header = "����������",
                        Data = employees,
                        Headers = new List<(int row, string header, string property, int height)>()
                {
                    (0, "���", "FullName", 100),
                    (1, "���������", "PositionsStr", 200),
                    (2, "�������������", "Department", 100),
                    (3, "���� ������", "YearsOfWork", 100),
                },
                        MergedCells = new List<(int startInd, int EndInd, string header)>()
                {
                    (2,3,"������"),
                }
                    };
                    customDocumentTableWord.CreateTable(list);

                    MessageBox.Show("���������� ������ �������", "���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void c���������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var employees = _employeeStorage.GetFullList();

            var experienceData = employees
                .GroupBy(e => e.Department)
                .ToDictionary(
                    group => group.Key,
                    group => new List<double>
                    {
            group.Count(e => e.YearsOfWork >= 1 && e.YearsOfWork <= 5),
            group.Count(e => e.YearsOfWork > 5 && e.YearsOfWork <= 10),
            group.Count(e => e.YearsOfWork > 10 && e.YearsOfWork <= 20),
            group.Count(e => e.YearsOfWork > 20 && e.YearsOfWork <= 30)
                    }
                );

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    diagramComponent.AddChartToPdf(new()
                    {
                        Filepath = filePath,
                        Header = "����������� ����������� �� �������������� � ���������� �����",
                        DiagramHeader = "�������� ���������",
                        LegendPosition = LegendPosition.Right,
                        XAxisHeaders = new List<string> { "1-5", "5-10", "10-20", "20-30" },
                        Series = experienceData,
                    });
                    MessageBox.Show("���������� ������ �������", "���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}