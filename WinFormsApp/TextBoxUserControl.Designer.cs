namespace WinFormsApp
{
    partial class TextBoxUserControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            textBox = new TextBox();
            checkBox = new CheckBox();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Location = new Point(0, 0);
            textBox.Name = "textBox";
            textBox.Size = new Size(159, 23);
            textBox.TabIndex = 0;
            // 
            // checkBox
            // 
            checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox.AutoSize = true;
            checkBox.Location = new Point(3, 29);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(60, 19);
            checkBox.TabIndex = 1;
            checkBox.Text = "empty";
            checkBox.UseVisualStyleBackColor = true;
            checkBox.CheckedChanged += CheckBox_CheckedChanged;
            // 
            // TextBoxUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkBox);
            Controls.Add(textBox);
            Name = "TextBoxUserControl";
            Size = new Size(162, 51);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox;
        private CheckBox checkBox;
    }
}
