namespace WinFormsApp
{
    public partial class ListBoxUserControl : UserControl
    {
        public ListBoxUserControl()
        {
            InitializeComponent();
        }

        public ListBox.ObjectCollection Strings
        {
            get { return listBox.Items; }
        }

        // Публичный метод для очистки списка
        public void ClearItems()
        {
            listBox.Items.Clear();
        }

        // Публичное свойство для установки и получения выбранного значения
        public string? SelectedValue
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
        private event EventHandler? selectedValueChanged;
        public event EventHandler? SelectedValueChanged
        {
            add => selectedValueChanged += value;
            remove => selectedValueChanged -= value;
        }

        // Обработчик события SelectedIndexChanged для ListBox
        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedValueChanged?.Invoke(this, e);
        }
    }
}
