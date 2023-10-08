namespace WinForms
{
    partial class FormEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxFullName = new TextBox();
            label1 = new Label();
            labelPositions = new Label();
            listBoxUserControlDepartments = new WinFormsApp.ListBoxUserControl();
            customNumericUpDown = new CustomVisualComponent.CustomNumericUpDown();
            labelAgeOfWork = new Label();
            labelDepartment = new Label();
            customCheckedListBoxPositions = new CustomVisualComponent.CustomCheckedListBox();
            buttonSave = new Button();
            labelEmployeePositions = new Label();
            buttonAddPosition = new Button();
            dataGridViewEmployeePositions = new DataGridView();
            buttonDeletePosition = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployeePositions).BeginInit();
            SuspendLayout();
            // 
            // textBoxFullName
            // 
            textBoxFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxFullName.Location = new Point(154, 6);
            textBoxFullName.Name = "textBoxFullName";
            textBoxFullName.Size = new Size(252, 23);
            textBoxFullName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 1;
            label1.Text = "ФИО";
            // 
            // labelPositions
            // 
            labelPositions.AutoSize = true;
            labelPositions.Location = new Point(12, 288);
            labelPositions.Name = "labelPositions";
            labelPositions.Size = new Size(90, 15);
            labelPositions.TabIndex = 2;
            labelPositions.Text = "Все должности";
            // 
            // listBoxUserControlDepartments
            // 
            listBoxUserControlDepartments.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBoxUserControlDepartments.Location = new Point(154, 41);
            listBoxUserControlDepartments.Name = "listBoxUserControlDepartments";
            listBoxUserControlDepartments.SelectedValue = "";
            listBoxUserControlDepartments.Size = new Size(252, 103);
            listBoxUserControlDepartments.TabIndex = 3;
            // 
            // customNumericUpDown
            // 
            customNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            customNumericUpDown.Location = new Point(154, 444);
            customNumericUpDown.MaxValue = null;
            customNumericUpDown.MinValue = null;
            customNumericUpDown.Name = "customNumericUpDown";
            customNumericUpDown.Size = new Size(252, 23);
            customNumericUpDown.TabIndex = 7;
            customNumericUpDown.Value = null;
            // 
            // labelAgeOfWork
            // 
            labelAgeOfWork.AutoSize = true;
            labelAgeOfWork.Location = new Point(11, 444);
            labelAgeOfWork.Name = "labelAgeOfWork";
            labelAgeOfWork.Size = new Size(79, 15);
            labelAgeOfWork.TabIndex = 8;
            labelAgeOfWork.Text = "Стаж работы";
            // 
            // labelDepartment
            // 
            labelDepartment.AutoSize = true;
            labelDepartment.Location = new Point(12, 41);
            labelDepartment.Name = "labelDepartment";
            labelDepartment.Size = new Size(92, 15);
            labelDepartment.TabIndex = 9;
            labelDepartment.Text = "Подразделение";
            // 
            // customCheckedListBoxPositions
            // 
            customCheckedListBoxPositions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            customCheckedListBoxPositions.Location = new Point(154, 288);
            customCheckedListBoxPositions.Name = "customCheckedListBoxPositions";
            customCheckedListBoxPositions.SelectedItem = "";
            customCheckedListBoxPositions.Size = new Size(252, 150);
            customCheckedListBoxPositions.TabIndex = 10;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonSave.Location = new Point(154, 473);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(252, 23);
            buttonSave.TabIndex = 11;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += ButtonSave_Click;
            // 
            // labelEmployeePositions
            // 
            labelEmployeePositions.AutoSize = true;
            labelEmployeePositions.Location = new Point(12, 144);
            labelEmployeePositions.Name = "labelEmployeePositions";
            labelEmployeePositions.Size = new Size(136, 15);
            labelEmployeePositions.TabIndex = 13;
            labelEmployeePositions.Text = "Должности сотрудника";
            // 
            // buttonAddPosition
            // 
            buttonAddPosition.Location = new Point(15, 306);
            buttonAddPosition.Name = "buttonAddPosition";
            buttonAddPosition.Size = new Size(133, 54);
            buttonAddPosition.TabIndex = 14;
            buttonAddPosition.Text = "Добавить должность сотруднику";
            buttonAddPosition.UseVisualStyleBackColor = true;
            buttonAddPosition.Click += ButtonAddPosition_Click;
            // 
            // dataGridViewEmployeePositions
            // 
            dataGridViewEmployeePositions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewEmployeePositions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmployeePositions.Location = new Point(154, 144);
            dataGridViewEmployeePositions.Name = "dataGridViewEmployeePositions";
            dataGridViewEmployeePositions.RowTemplate.Height = 25;
            dataGridViewEmployeePositions.Size = new Size(252, 138);
            dataGridViewEmployeePositions.TabIndex = 15;
            // 
            // buttonDeletePosition
            // 
            buttonDeletePosition.Location = new Point(15, 162);
            buttonDeletePosition.Name = "buttonDeletePosition";
            buttonDeletePosition.Size = new Size(133, 54);
            buttonDeletePosition.TabIndex = 16;
            buttonDeletePosition.Text = "Удалить должность у сотрудника";
            buttonDeletePosition.UseVisualStyleBackColor = true;
            buttonDeletePosition.Click += ButtonDeletePosition_Click;
            // 
            // FormEmployee
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 508);
            Controls.Add(buttonDeletePosition);
            Controls.Add(dataGridViewEmployeePositions);
            Controls.Add(buttonAddPosition);
            Controls.Add(labelEmployeePositions);
            Controls.Add(buttonSave);
            Controls.Add(customCheckedListBoxPositions);
            Controls.Add(labelDepartment);
            Controls.Add(labelAgeOfWork);
            Controls.Add(customNumericUpDown);
            Controls.Add(listBoxUserControlDepartments);
            Controls.Add(labelPositions);
            Controls.Add(label1);
            Controls.Add(textBoxFullName);
            Name = "FormEmployee";
            Text = "Сотрудник";
            FormClosing += Form_FormClosing;
            Load += FormEmployee_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployeePositions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxFullName;
        private Label label1;
        private Label labelPositions;
        private WinFormsApp.ListBoxUserControl listBoxUserControlDepartments;
        private CustomVisualComponent.CustomNumericUpDown customNumericUpDown;
        private Label labelAgeOfWork;
        private Label labelDepartment;
        private CustomVisualComponent.CustomCheckedListBox customCheckedListBoxPositions;
        private Button buttonSave;
        private Label labelEmployeePositions;
        private Button buttonAddPosition;
        private DataGridView dataGridViewEmployeePositions;
        private Button buttonDeletePosition;
    }
}