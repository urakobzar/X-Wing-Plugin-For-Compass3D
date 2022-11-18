using System;
using System.Collections.Generic;

namespace XWingPluginForCompass3D.Model
{
    /// <summary>
    /// Класс параметра.
    /// </summary>
    public class Parameter: IEquatable<Parameter>
    {
        /// <summary>
        /// Значение параметра.
        /// </summary>
        private double _value;

        /// <summary>
        /// Минимальное допустимое значение параметра.
        /// </summary>
        private readonly int _minValue;

        /// <summary>
        /// Максимальное допустимое значение параметра.
        /// </summary>
        private readonly int _maxValue;
        
        /// <summary>
        /// Сообщение о несоблюдении границы минимума.
        /// </summary>
        private readonly string _minErrorMessage;
        
        /// <summary>
        /// Сообщение о несоблюдении границы максимума.
        /// </summary>
        private readonly string _maxErrorMessage;

        /// <summary>
        /// Тип параметра.
        /// </summary>
        private readonly XWingParameterType _parameterType;
        
        /// <summary>
        /// Список ошибок введенного параметра.
        /// </summary>
        private readonly Dictionary<XWingParameterType, string> _errors;

        /// <summary>
        /// Устанавливает и возвращает значение параметра.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (CheckRange(value))
                {
                    _value = value;
                }
            }
        }

        /// <summary>
        /// Создает объект класса параметра.
        /// </summary>
        /// <param name="value">Введенное значение параметра.</param>
        /// <param name="minValue">Минимальное возможное значение.</param>
        /// <param name="maxValue">Максимальное возможное значение.</param>
        /// <param name="errorMessage">Сообщение о том, какой это параметр.</param>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="errors"></param>
        public Parameter(double value, int minValue, int maxValue, 
            string errorMessage, XWingParameterType parameterType,
            Dictionary<XWingParameterType, string> errors)
        {
            _errors = errors;
            _minValue = minValue;
            _maxValue = maxValue;
            _minErrorMessage = errorMessage + " не может быть менее " +
                              _minValue + " мм.";
            _maxErrorMessage = errorMessage + " не может быть более " +
                              _maxValue + " мм.";
            _parameterType = parameterType;
            Value = value;
        }

        /// <summary>
        /// Проверка принадлежности диапазону введенного параметра.
        /// </summary>
        /// <param name="value">Введенное значение параметра.</param>
        private bool CheckRange(double value)
        {
            if (value < _minValue)
            {
                _errors.Add(_parameterType, _minErrorMessage);
                return false;
            }
            if (!(value > _maxValue)) return true;
            _errors.Add(_parameterType, _maxErrorMessage);
            return false;
        }

        /// <summary>
        /// Проверка на равенство объектов класса.
        /// </summary>
        /// <param name="expected">Сравниваемый объект.</param>
        /// <returns>Возвращает true, если элементы равны,
        /// false - в обратном случае.</returns>
        public bool Equals(Parameter expected)
        {
            return expected != null &&
                   expected.Value.Equals(Value) &&
                   expected._minValue.Equals(_minValue) &&
                   expected._maxValue.Equals(_maxValue) &&
                   expected._minErrorMessage.Equals(_minErrorMessage) &&
                   expected._maxErrorMessage.Equals(_maxErrorMessage) &&
                   expected._errors.Equals(_errors) &&
                   expected._parameterType.Equals(_parameterType);
        }
    }
}