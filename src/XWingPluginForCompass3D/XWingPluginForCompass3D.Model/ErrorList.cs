using System.Collections.Generic;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс списка ошибок.
    /// </summary>
    public static class ErrorList
    {
        /// <summary>
        /// Словарь, где ключ: параметр звездолёта, значение: соотвествующее 
        /// сообщение об ошибке при попытке присвоить параметр.
        /// </summary>
        private static Dictionary<XWingParameters, string> _errors;

        /// <summary>
        /// Устанавливает и возвращает словарь параметров-сообщений.
        /// </summary>
        public static Dictionary<XWingParameters, string> Errors
        {
            set
            {
                _errors = value;
            }
            get
            {
                return _errors;
            }
        }
    }
}