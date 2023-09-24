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
        /// <summary>
        /// Публичный метод для очистки списка.
        /// </summary>
        public void ClearItems()
        {
            listBox.Items.Clear();
        }
        /// <summary>
        /// Публичное свойство для установки и получения выбранного значения.
        /// </summary>
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
        /// <summary>
        /// Событие, вызываемое при смене значения в ListBox.
        /// </summary>
        private event EventHandler? selectedValueChanged;
        public event EventHandler? SelectedValueChanged
        {
            add => selectedValueChanged += value;
            remove => selectedValueChanged -= value;
        }
        /// <summary>
        /// Обработчик события SelectedIndexChanged для ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedValueChanged?.Invoke(this, e);
        }
    }
}
