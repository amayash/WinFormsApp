namespace WinFormsApp
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonGet = new Button();
            buttonTable = new Button();
            button2 = new Button();
            button3 = new Button();
            componentHardTable = new WinFormsLibrary.ComponentHardTable();
            SuspendLayout();
            // 
            // buttonGet
            // 
            buttonGet.Location = new Point(176, 12);
            buttonGet.Name = "buttonGet";
            buttonGet.Size = new Size(38, 23);
            buttonGet.TabIndex = 0;
            buttonGet.Text = "get";
            buttonGet.UseVisualStyleBackColor = true;
            buttonGet.Click += ButtonGet_Click;
            // 
            // buttonTable
            // 
            buttonTable.Location = new Point(8, 357);
            buttonTable.Name = "buttonTable";
            buttonTable.Size = new Size(75, 23);
            buttonTable.TabIndex = 1;
            buttonTable.Text = "table";
            buttonTable.UseVisualStyleBackColor = true;
            buttonTable.Click += ButtonTable_Click;
            // 
            // button2
            // 
            button2.Location = new Point(8, 386);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(8, 415);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 3;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(buttonTable);
            Controls.Add(buttonGet);
            Name = "Form";
            Text = "Form";
            Load += Form_Load;
            ResumeLayout(false);
        }

        #endregion
        private TextBox textBoxDate;
        private Button buttonGet;
        private Button buttonTable;
        private Button button2;
        private Button button3;
        private WinFormsLibrary.ComponentHardTable componentHardTable;
    }
}