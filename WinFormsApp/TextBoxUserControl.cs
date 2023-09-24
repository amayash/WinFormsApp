using System.Globalization;

namespace WinFormsApp
{
    public partial class TextBoxUserControl : UserControl
    {
        public TextBoxUserControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Возвращает или задает значение, указывающее на наличие ошибки.
        /// </summary>
        public bool Error { get; private set; }
        /// <summary>
        /// Свойство для получения и установки даты.
        /// </summary>
        public DateTime? SelectedDate
        {
            get
            {
                if (checkBox.Checked)
                {
                    return null; // Возвращает null
                }
                if (DateTime.TryParseExact(textBox.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    return result; // Возвращает дату, если формат введенных данных верный
                }
                else
                {
                    Error = true;
                    return null; // Возвращает null, если формат неверен
                }
            }
            set
            {
                if (value.HasValue)
                {
                    textBox.Text = value.Value.ToString("dd.MM.yyyy"); // Устанавливает текст в TextBox
                }
                else
                {
                    checkBox.Checked = true;
                }
            }
        }
        /// <summary>
        /// Событие, вызываемое при изменении значения.
        /// </summary>
        private event EventHandler? valueChanged;
        public event EventHandler? ValueChanged
        {
            add => valueChanged += value;
            remove => valueChanged -= value;
        }
        /// <summary>
        /// Обработчик события CheckedChanged для CheckBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            textBox.Enabled = !checkBox.Checked; // Включает/отключает TextBox в зависимости от состояния CheckBox
            if (checkBox.Checked)
            {
                textBox.Text = null;
            }
        }
    }
}
