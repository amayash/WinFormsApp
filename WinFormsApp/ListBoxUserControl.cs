using System.Data;

namespace WinFormsApp
{
    public partial class ListBoxUserControl : UserControl
    {
        public ListBoxUserControl()
        {
            InitializeComponent();
        }

        // Публичное свойство для установки строковых значений в список
        public string[] Strings
        {
            get { return listBox.Items.Cast<string>().ToArray(); }
            set
            {
                // Очищаем список элементов и устанавливаем новые значения
                listBox.Items.Clear();
                if (value != null)
                {
                    foreach (string item in value)
                    {
                        listBox.Items.Add(item);
                    }
                }
            }
        }

        // Публичный метод для очистки списка
        public void ClearItems()
        {
            listBox.Items.Clear();
        }

        // Публичное свойство для установки и получения выбранного значения
        public string SelectedValue
        {
            get
            {
                if (listBox.SelectedIndex >= 0)
                {
                    return listBox.Items[listBox.SelectedIndex].ToString();
                }
                return string.Empty;
            }
            set
            {
                // Поиск индекса элемента в списке с точным совпадением строки
                int index = listBox.FindStringExact(value);
                if (index >= 0)
                {
                    listBox.SelectedIndex = index;
                }
                else
                {
                    listBox.ClearSelected();
                }
            }
        }

        // Событие, вызываемое при смене значения в ListBox
        public event EventHandler? SelectedValueChanged;

        // Обработчик события SelectedIndexChanged для ListBox
        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedValueChanged?.Invoke(this, e);
        }
    }
}
