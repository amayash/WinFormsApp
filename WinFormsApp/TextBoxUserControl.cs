using System.Globalization;

namespace WinFormsApp
{
    public partial class TextBoxUserControl : UserControl
    {
        public TextBoxUserControl()
        {
            InitializeComponent();
        }

        // Свойство для получения и установки даты
        public DateTime? SelectedDate
        {
            get
            {
                if (checkBox.Checked || string.IsNullOrWhiteSpace(textBox.Text))
                {
                    return null; // Возвращает null
                }
                else
                {
                    if (DateTime.TryParseExact(textBox.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    {
                        return result; // Возвращает дату, если формат введенных данных верный
                    }
                    else
                    {
                        MessageBox.Show("Введенное значение не соответствует формату DD.MM.YYYY.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null; // Возвращает null, если формат неверен
                    }
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
                    textBox.Text = string.Empty; // Очищает TextBox, если дата null
                }
            }
        }

        // Событие, вызываемое при изменении значения
        public event EventHandler? ValueChanged;

        // Обработчик события CheckedChanged для CheckBox
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            textBox.Enabled = !checkBox.Checked; // Включает/отключает TextBox в зависимости от состояния CheckBox

            if (!checkBox.Checked && string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Значение не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ValueChanged?.Invoke(this, e);
        }
    }
}
