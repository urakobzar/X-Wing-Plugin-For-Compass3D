namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс параметра.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Значение параметра.
        /// </summary>
        private double _value;

        /// <summary>
        /// Минимальное допустимое значение параметра.
        /// </summary>
        private int _minValue;

        /// <summary>
        /// Максимальное допустимое значение параметра.
        /// </summary>
        private int _maxValue;

        /// <summary>
        /// Сообщение о несоблюдении границы минимума.
        /// </summary>
        private string _minErrorMessage;

        /// <summary>
        /// Сообщение о несоблюдении границы максимума.
        /// </summary>
        private string _maxErrorMessage;

        /// <summary>
        /// Тип параметра.
        /// </summary>
        private XWingParameters _parameterType;

        /// <summary>
        /// Устанавливает и возвращает значение параметра.
        /// </summary>
        public double Value
        {
            set
            {
                if(CheckRange(value, MinValue, MaxValue, ParameterType, 
                    MinErrorMessage, MaxErrorMessage))
                {
                    _value = value;
                }
            }
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает минимальное допустимое значение параметра.
        /// </summary>
        public int MinValue
        {
            set
            {
                _minValue = value;
            }
            get
            {
                return _minValue;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает максимальное допустимое значение параметра.
        /// </summary>
        public int MaxValue
        {
            set
            {
                _maxValue = value;
            }
            get
            {
                return _maxValue;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает сообщение о несоблюдении границы минимума.
        /// </summary>
        public string MinErrorMessage
        {
            set
            {
                _minErrorMessage = value + " не может быть менее " + 
                    MinValue + " мм.";
            }
            get
            {
                return _minErrorMessage;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает сообщение о несоблюдении границы максимума.
        /// </summary>
        public string MaxErrorMessage
        {
            set
            {
                _maxErrorMessage = value + " не может быть более " + 
                    MaxValue + " мм.";
            }
            get
            {
                return _maxErrorMessage;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает тип параметра.
        /// </summary>
        public XWingParameters ParameterType
        {
            set
            {
                _parameterType = value;
            }
            get
            {
                return _parameterType;
            }
        }

        /// <summary>
        /// Создает объект класса параметра.
        /// </summary>
        /// <param name="value">Введеное значение параметра.</param>
        /// <param name="minValue">Минимальное возможное значение.</param>
        /// <param name="maxValue">Максимальное возможное значение.</param>
        /// <param name="errorMessage">Сообщение о том, какой это параметр.</param>
        /// <param name="parameterType">Тип параметра.</param>
        public Parameter(double value, int minValue, int maxValue, 
            string errorMessage, XWingParameters parameterType)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            MinErrorMessage = errorMessage;
            MaxErrorMessage = errorMessage;
            ParameterType = parameterType;
            Value = value;
        }

        /// <summary>
        /// Проверка принадлежности диапазону введённого параметра.
        /// </summary>
        /// <param name="value">Введеное значение параметра.</param>
        /// <param name="minValue">Минимальное возможное значение.</param>
        /// <param name="maxValue">Максимальное возможное значение.</param>
        /// <param name="parameter">Параметр.</param>
        /// <param name="minMessage">Сообщение, если меньше минимума.</param>
        /// <param name="maxMessage">Сообщение, если больше максимума.</param>
        private bool CheckRange(double value, double minValue,
            double maxValue, XWingParameters parameter,
            string minMessage, string maxMessage)
        {
            if (value < minValue)
            {
                ErrorList.Errors.Add(parameter, minMessage);
                return false;
            }
            else if (value > maxValue)
            {
                ErrorList.Errors.Add(parameter, maxMessage);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}