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
            customListBox = new VisualComponents.CustomListBox();
            contextMenuStrip = new ContextMenuStrip(components);
            создатьСотрудникаToolStripMenuItem = new ToolStripMenuItem();
            изменитьСотрудникаToolStripMenuItem = new ToolStripMenuItem();
            удалитьСотрудникаToolStripMenuItem = new ToolStripMenuItem();
            создатьДолжностьToolStripMenuItem = new ToolStripMenuItem();
            созданиеПростогоДокументаToolStripMenuItem = new ToolStripMenuItem();
            cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem = new ToolStripMenuItem();
            cозданиеДокументаСДиаграммойToolStripMenuItem = new ToolStripMenuItem();
            componentTable = new WinFormsLibrary.ComponentTable(components);
            customDocumentTableWord = new CustomNonVisualComponent.CustomDocumentTableWord(components);
            diagramComponent = new NonVisualComponents.DiagramComponent(components);
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // customListBox
            // 
            customListBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            customListBox.ContextMenuStrip = contextMenuStrip;
            customListBox.CurrentRow = -1;
            customListBox.Dock = DockStyle.Fill;
            customListBox.Location = new Point(0, 0);
            customListBox.Margin = new Padding(2, 2, 2, 2);
            customListBox.Name = "customListBox";
            customListBox.Size = new Size(778, 450);
            customListBox.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { создатьСотрудникаToolStripMenuItem, изменитьСотрудникаToolStripMenuItem, удалитьСотрудникаToolStripMenuItem, создатьДолжностьToolStripMenuItem, созданиеПростогоДокументаToolStripMenuItem, cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem, cозданиеДокументаСДиаграммойToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(340, 158);
            // 
            // создатьСотрудникаToolStripMenuItem
            // 
            создатьСотрудникаToolStripMenuItem.Name = "создатьСотрудникаToolStripMenuItem";
            создатьСотрудникаToolStripMenuItem.Size = new Size(339, 22);
            создатьСотрудникаToolStripMenuItem.Text = "Создать сотрудника";
            создатьСотрудникаToolStripMenuItem.Click += создатьСотрудникаToolStripMenuItem_Click;
            // 
            // изменитьСотрудникаToolStripMenuItem
            // 
            изменитьСотрудникаToolStripMenuItem.Name = "изменитьСотрудникаToolStripMenuItem";
            изменитьСотрудникаToolStripMenuItem.Size = new Size(339, 22);
            изменитьСотрудникаToolStripMenuItem.Text = "Изменить сотрудника";
            изменитьСотрудникаToolStripMenuItem.Click += изменитьСотрудникаToolStripMenuItem_Click;
            // 
            // удалитьСотрудникаToolStripMenuItem
            // 
            удалитьСотрудникаToolStripMenuItem.Name = "удалитьСотрудникаToolStripMenuItem";
            удалитьСотрудникаToolStripMenuItem.Size = new Size(339, 22);
            удалитьСотрудникаToolStripMenuItem.Text = "Удалить сотрудника";
            удалитьСотрудникаToolStripMenuItem.Click += удалитьСотрудникаToolStripMenuItem_Click;
            // 
            // создатьДолжностьToolStripMenuItem
            // 
            создатьДолжностьToolStripMenuItem.Name = "создатьДолжностьToolStripMenuItem";
            создатьДолжностьToolStripMenuItem.Size = new Size(339, 22);
            создатьДолжностьToolStripMenuItem.Text = "Управление должностями";
            создатьДолжностьToolStripMenuItem.Click += создатьДолжностьToolStripMenuItem_Click;
            // 
            // созданиеПростогоДокументаToolStripMenuItem
            // 
            созданиеПростогоДокументаToolStripMenuItem.Name = "созданиеПростогоДокументаToolStripMenuItem";
            созданиеПростогоДокументаToolStripMenuItem.Size = new Size(339, 22);
            созданиеПростогоДокументаToolStripMenuItem.Text = "Cоздание простого документа";
            созданиеПростогоДокументаToolStripMenuItem.Click += созданиеПростогоДокументаToolStripMenuItem_Click;
            // 
            // cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem
            // 
            cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem.Name = "cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem";
            cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem.Size = new Size(339, 22);
            cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem.Text = "Cоздание документа с настраиваемой таблицей";
            cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem.Click += cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem_Click;
            // 
            // cозданиеДокументаСДиаграммойToolStripMenuItem
            // 
            cозданиеДокументаСДиаграммойToolStripMenuItem.Name = "cозданиеДокументаСДиаграммойToolStripMenuItem";
            cозданиеДокументаСДиаграммойToolStripMenuItem.Size = new Size(339, 22);
            cозданиеДокументаСДиаграммойToolStripMenuItem.Text = "Cоздание документа с диаграммой";
            cозданиеДокументаСДиаграммойToolStripMenuItem.Click += cозданиеДокументаСДиаграммойToolStripMenuItem_Click;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 450);
            Controls.Add(customListBox);
            Name = "Form";
            Text = "Form";
            Load += Form_Load;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TextBox textBoxDate;
        private WinFormsLibrary.ComponentHardTable componentHardTable;
        private VisualComponents.CustomListBox customListBox;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem создатьСотрудникаToolStripMenuItem;
        private ToolStripMenuItem изменитьСотрудникаToolStripMenuItem;
        private ToolStripMenuItem удалитьСотрудникаToolStripMenuItem;
        private ToolStripMenuItem создатьДолжностьToolStripMenuItem;
        private ToolStripMenuItem созданиеПростогоДокументаToolStripMenuItem;
        private ToolStripMenuItem cозданиеДокументаСНастраиваемойТаблицейToolStripMenuItem;
        private ToolStripMenuItem cозданиеДокументаСДиаграммойToolStripMenuItem;
        private WinFormsLibrary.ComponentTable componentTable;
        private CustomNonVisualComponent.CustomDocumentTableWord customDocumentTableWord;
        private NonVisualComponents.DiagramComponent diagramComponent;
    }
}