namespace PluginsConventionLibraryNet60
{
    public interface IPluginsConvention
    {
        /// <summary>
        /// Название плагина
        /// </summary>
        string PluginName { get; }
        /// <summary>
        /// Получение контрола для вывода набора данных
        /// </summary>
        UserControl GetControl { get; }
        /// <summary>
        /// Получение элемента, выбранного в контроле
        /// </summary>
        PluginsConventionElement GetElement { get; }
        /// <summary>
        /// Получение формы для создания/редактирования объекта
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        Form GetForm(PluginsConventionElement element);
        /// <summary>
        /// Получение формы для работы со справочником
        /// </summary>
        /// <returns></returns>
        Form GetThesaurus();
        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool DeleteElement(PluginsConventionElement element);
        /// <summary>
        /// Обновление набора данных в контроле
        /// </summary>
        void ReloadData();
        /// <summary>
        /// Создание простого документа
        /// </summary>
        /// <param name="saveDocument"></param>
        /// <returns></returns>
        bool CreateSimpleDocument(PluginsConventionElement element, PluginsConventionSaveDocument saveDocument);
        /// <summary>
        /// Создание простого документа
        /// </summary>
        /// <param name="saveDocument"></param>
        /// <returns></returns>
        bool CreateTableDocument(PluginsConventionSaveDocument saveDocument);
        /// <summary>
        /// Создание документа с диаграммой
        /// </summary>
        /// <param name="saveDocument"></param>
        /// <returns></returns>
        bool CreateChartDocument(PluginsConventionSaveDocument saveDocument);
    }
}