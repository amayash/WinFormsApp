using PluginsConventionLibraryNet60;
using System.Reflection;

namespace WinFormsAppByPluginsNet60
{
    public partial class FormMain : Form
    {
        private readonly Dictionary<string, IPluginsConvention> _plugins;
        private string _selectedPlugin;
        public FormMain()
        {
            InitializeComponent();
            _plugins = LoadPlugins();
            _selectedPlugin = string.Empty;
            LoadDropDownItems();
        }
        private Dictionary<string, IPluginsConvention> LoadPlugins()
        {
            Dictionary<string, IPluginsConvention> _plugins = new();
            string currentDir = Environment.CurrentDirectory;
            string pluginsDir = Directory.GetParent(currentDir).Parent.Parent.Parent.FullName + "\\Plugins";
            string[] dllFiles = Directory.GetFiles(pluginsDir, "*.dll", SearchOption.AllDirectories);

            foreach (string dllFile in dllFiles)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllFile);
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (typeof(IPluginsConvention).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            if (Activator.CreateInstance(type) is IPluginsConvention plugin)
                            {
                                string pluginName = plugin.PluginName;
                                _plugins.Add(pluginName, plugin);
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке сборки {dllFile}: {ex.Message}");
                }
            }
            return _plugins;
        }
        private void LoadDropDownItems()
        {
            foreach (var plugin in _plugins)
            {
                ToolStripMenuItem pluginMenuItem = new ToolStripMenuItem(plugin.Key);
                pluginMenuItem.Click += (object? sender, EventArgs a) =>
                {
                    _selectedPlugin = plugin.Key;
                    var userControl = _plugins[plugin.Key].GetControl;
                    if (userControl != null)
                    {
                        panelControl.Controls.Clear();
                        userControl.Dock = DockStyle.Fill;
                        _plugins[plugin.Key].ReloadData();
                        panelControl.Controls.Add(userControl);
                    }
                };
                ControlsStripMenuItem.DropDownItems.Add(pluginMenuItem); 
            }
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPlugin) || !_plugins.ContainsKey(_selectedPlugin))
            {
                return;
            }
            if (!e.Control)
            {
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.I:
                    ShowThesaurus();
                    break;
                case Keys.A:
                    AddNewElement();
                    break;
                case Keys.U:
                    UpdateElement();
                    break;
                case Keys.D:
                    DeleteElement();
                    break;
                case Keys.S:
                    CreateSimpleDoc();
                    break;
                case Keys.T:
                    CreateTableDoc();
                    break;
                case Keys.C:
                    CreateChartDoc();
                    break;
            }
        }
        private void ShowThesaurus()
        {
            _plugins[_selectedPlugin].GetThesaurus()?.Show();
        }
        private void AddNewElement()
        {
            var form = _plugins[_selectedPlugin].GetForm(null);
            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }
        private void UpdateElement()
        {
            var element = _plugins[_selectedPlugin].GetElement;
            if (element == null)
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var form = _plugins[_selectedPlugin].GetForm(element);
            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }
        private void DeleteElement()
        {
            if (MessageBox.Show("Удалить выбранный элемент", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            var element = _plugins[_selectedPlugin].GetElement;
            if (element == null)
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_plugins[_selectedPlugin].DeleteElement(element))
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }
        private void CreateSimpleDoc()
        {
            IPluginsConvention plugin;
            if (!_plugins.TryGetValue(_selectedPlugin, out plugin))
            {
                MessageBox.Show("Ауу, загрузи плагин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (plugin.CreateSimpleDocument(plugin.GetElement, 
                        new PluginsConventionSaveDocument() { FileName = saveFileDialog.FileName }))
                    {
                        MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при создании документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void CreateTableDoc()
        {
            IPluginsConvention plugin;
            if (!_plugins.TryGetValue(_selectedPlugin, out plugin))
            {
                MessageBox.Show("Ауу, загрузи плагин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word Files (*.docx)|*.docx|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (plugin.CreateTableDocument(new PluginsConventionSaveDocument() { FileName = saveFileDialog.FileName }))
                    {
                        MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при создании документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void CreateChartDoc()
        {
            IPluginsConvention plugin;
            if (!_plugins.TryGetValue(_selectedPlugin, out plugin))
            {
                MessageBox.Show("Ауу, загрузи плагин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (plugin.CreateChartDocument(new PluginsConventionSaveDocument() { FileName = saveFileDialog.FileName }))
                    {
                        MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при создании документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ThesaurusToolStripMenuItem_Click(object sender, EventArgs e) => ShowThesaurus();
        private void AddElementToolStripMenuItem_Click(object sender, EventArgs e) => AddNewElement();
        private void UpdElementToolStripMenuItem_Click(object sender,EventArgs e) => UpdateElement();
        private void DelElementToolStripMenuItem_Click(object sender,EventArgs e) => DeleteElement();
        private void SimpleDocToolStripMenuItem_Click(object sender,EventArgs e) => CreateSimpleDoc();
        private void TableDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateTableDoc();
        private void ChartDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateChartDoc();
    }
}
