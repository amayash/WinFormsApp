using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsLibrary
{
    public partial class Component : System.ComponentModel.Component
    {
        private string _fileName;
        public string FileName
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                if (!value.EndsWith(".txt"))
                {
                    throw new ArgumentException("No txt file");
                }
                _fileName = value;
            }
        }
        public Component()
        {
            InitializeComponent();
            _fileName = string.Empty;
        }
        public Component(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            _fileName = string.Empty;
        }
        public bool SaveToFile(string[] texts)
        {
            CheckFileExsists();
            using var writer = new StreamWriter(_fileName, true);
            foreach (var text in texts)
            {
                writer.WriteLine(text);
            }
            writer.Flush();
            return true;
        }
        private void CheckFileExsists()
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                throw new ArgumentNullException(_fileName);
            }
            if (!File.Exists(_fileName))
            {
                throw new FileNotFoundException(_fileName);
            }
        }
    }
}
