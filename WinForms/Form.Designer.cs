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
            components = new System.ComponentModel.Container();
            buttonGet = new Button();
            component1 = new WinFormsLibrary.Component(components);
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
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 450);
            Controls.Add(buttonGet);
            Name = "Form";
            Text = "Form";
            Load += Form_Load;
            ResumeLayout(false);
        }

        #endregion
        private TextBox textBoxDate;
        private Button buttonGet;
        private WinFormsLibrary.Component component1;
    }
}