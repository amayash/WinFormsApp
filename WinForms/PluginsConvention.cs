using EmployeeTracking.DTOs;
using EmployeeTracking.Storages;
using System.Reflection;
using VisualComponents;
using WinForms;
using WinFormsLibrary.OfficePackage.HelperModels;
using WinFormsLibrary;
using NonVisualComponents;
using CustomNonVisualComponent;
using DocumentFormat.OpenXml.VariantTypes;
using System.Windows.Forms;
using WinFormsApp;

namespace PluginsConventionLibraryNet60
{
    public class PluginsConvention : IPluginsConvention
    {
        CustomListBox customListBox;
        EmployeeStorage employeeStorage;
        PositionStorage positionStorage;

        public PluginsConvention()
        {
            customListBox = new();
            employeeStorage = new();
            positionStorage = new();
            customListBox.FillTemplateProperties("Идентификатор @Id@, ФИО сотрудника @FullName@, должности @PositionsStr@, подразделение @Department@, стаж работы @YearsOfWork@", '@', '@');
        }

        public string PluginName
        {
            get { return "Стремный список"; }
        }

        public UserControl GetControl
        {
            get
            {
                return customListBox;
            }
        }

        public PluginsConventionElement GetElement
        {
            get
            {
                EmployeeListBoxViewModel obj = customListBox.GetObjectFromStr<EmployeeListBoxViewModel>();
                int? id = Convert.ToInt32(obj.Id);

                byte[] bytes = new byte[16];
                if (!id.HasValue)
                {
                    id = -1;
                }
                BitConverter.GetBytes(id.Value).CopyTo(bytes, 0);
                return new()
                {
                    Id = new Guid(bytes)
                };
            }
        }

        public bool CreateChartDocument(PluginsConventionSaveDocument saveDocument)
        {
            try
            {
                var employees = employeeStorage.GetFullList();

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

                new DiagramComponent().AddChartToPdf(new()
                {
                    Filepath = saveDocument.FileName,
                    Header = "Группировка сотрудников по подразделениям и диапазонам стажа",
                    DiagramHeader = "Линейная диаграмма",
                    LegendPosition = LegendPosition.Right,
                    XAxisHeaders = new List<string> { "1-5", "5-10", "10-20", "20-30" },
                    Series = experienceData,
                });
            } 
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool CreateSimpleDocument(PluginsConventionElement element, PluginsConventionSaveDocument saveDocument)
        {
            if (element.Id.GetHashCode() < 0)
            {
                return false;
            }

            var id = element.Id.GetHashCode();
            var empl = employeeStorage.GetElement(id);

            if (empl == null)
                return false;

            string filePath = saveDocument.FileName;

            List<string[,]> list1 = new();
            string[,] arr1 = new string[1, empl.Positions.Count];

            for (int i = 0; i < empl.Positions.Count; i++)
            {
                arr1[0, i] = empl.Positions[i];
            }

            list1.Add(arr1);

            new ComponentTable().CreateTableReport(new ExcelInfo()
            {
                FileName = filePath,
                Title = empl.FullName,
                Tables = list1
            });
            return true;
        }

        public bool CreateTableDocument(PluginsConventionSaveDocument saveDocument)
        {
            try
            {
                var employees = employeeStorage.GetFullList();
                TableInfo<EmployeeViewModel> list = new TableInfo<EmployeeViewModel>()
                {
                    FileName = saveDocument.FileName,
                    Header = "Сотрудники",
                    Data = employees,
                    Headers = new List<(int row, string header, string property, int height)>()
                        {
                            (0, "ФИО", "FullName", 100),
                            (1, "Должности", "PositionsStr", 200),
                            (2, "Подразделение", "Department", 100),
                            (3, "Стаж работы", "YearsOfWork", 100),
                        },
                    MergedCells = new List<(int startInd, int EndInd, string header)>()
                        {
                            (2,3,"Работа"),
                        }
                };
                new CustomDocumentTableWord().CreateTable(list);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteElement(PluginsConventionElement element)
        {
            var result = employeeStorage.Delete(new() { Id = element.Id.GetHashCode() });
            return result != null;
        }

        public System.Windows.Forms.Form GetForm(PluginsConventionElement element)
        {
            if (element == null)
            {
                return new FormEmployee(employeeStorage, positionStorage);
            }
            if (element.Id.GetHashCode() > 0)
            {
                var form = new FormEmployee(employeeStorage, positionStorage);
                form.Id = element.Id.GetHashCode();
                return form;
            }
            return null!;
        }

        public System.Windows.Forms.Form GetThesaurus()
        {
            return new FormPositions(positionStorage);
        }

        public void ReloadData()
        {
            var list = employeeStorage.GetFullList();
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
