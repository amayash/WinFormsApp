using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsLibrary
{
    public partial class ComponentTable : System.ComponentModel.Component
    {
        public ComponentTable()
        {
            InitializeComponent();
        }
        public ComponentTable(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void CheckFileExsists(string _fileName)
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                throw new ArgumentNullException(_fileName);
            }
            if (!File.Exists(_fileName))
            {
                throw new FileNotFoundException(_fileName);
            }
        }

        public void CreateSpreadsheetWorkbook(string filepath, string documentTitle, List<string[,]> tables)
        {
            // Открываем документ для редактирования.
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(filepath, true))
            {
                // Получаем часть SharedStringTable. Если она не существует, создаем новую.
                SharedStringTablePart shareStringPart;
                if (spreadSheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    shareStringPart = spreadSheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    shareStringPart = spreadSheet.WorkbookPart.AddNewPart<SharedStringTablePart>();
                }

                // Вставляем текст в часть SharedStringTable.
                int index = InsertSharedStringItem(documentTitle, shareStringPart);

                // Вставляем новый лист.
                WorksheetPart worksheetPart = InsertWorksheet(spreadSheet.WorkbookPart);

                // Вставляем ячейку A1 в новый лист.
                Cell cell = InsertCellInWorksheet("A", 1, worksheetPart);

                // Устанавливаем значение ячейки A1.
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                // Создаем стиль для жирного текста.
                CellFormat cellFormat = new CellFormat();
                cellFormat.FontId = 1; // Устанавливаем идентификатор шрифта для жирного стиля.

                // Добавляем стиль в SharedStringTablePart.
                if (shareStringPart.WorkbookStylesPart == null)
                {
                    shareStringPart.AddNewPart<WorkbookStylesPart>();
                }

                if (shareStringPart.WorkbookStylesPart.Stylesheet == null)
                {
                    shareStringPart.WorkbookStylesPart.Stylesheet = new Stylesheet();
                }

                shareStringPart.WorkbookStylesPart.Stylesheet.Fonts = new Fonts();
                shareStringPart.WorkbookStylesPart.Stylesheet.Fonts.AppendChild(new Font() { Bold = new Bold() });

                // Применяем созданный стиль к ячейке.
                cell.StyleIndex = 1;


                // Сохраняем новый лист.
                worksheetPart.Worksheet.Save();
            }
        }

        // По заданному тексту и SharedStringTablePart создает SharedStringItem с указанным текстом
        // и вставляет его в SharedStringTablePart. Если элемент уже существует, возвращает его индекс.
        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // Если часть не содержит SharedStringTable, создаем ее.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }
            int i = 0;

            // Перебираем все элементы в SharedStringTable. Если текст уже существует, возвращаем его индекс.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // Текст не существует в части. Создаем SharedStringItem и возвращаем его индекс.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        // Вставляет новый лист на основе части рабочей книги (WorkbookPart).
        private static WorksheetPart InsertWorksheet(WorkbookPart workbookPart)
        {
            // Добавляем новую часть листа в рабочую книгу.
            WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            newWorksheetPart.Worksheet.Save();

            Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

            // Получаем уникальный ID для нового листа.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            string sheetName = "Sheet" + sheetId;

            // Добавляем новый лист и связываем его с рабочей книгой.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
            workbookPart.Workbook.Save();

            return newWorksheetPart;
        }

        // По заданному имени столбца, индексу строки и части листа (WorksheetPart) вставляет ячейку в лист.
        // Если ячейка уже существует, возвращает ее.
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // Если лист не содержит строки с указанным индексом, вставляем новую строку.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // Если ячейка с указанным именем столбца не существует, вставляем ее.
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Ячейки должны быть упорядочены по CellReference. Определяем, куда вставить новую ячейку.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }
    }
}