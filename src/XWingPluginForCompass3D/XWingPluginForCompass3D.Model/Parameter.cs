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
        /// Сообщение о несоблюдении границы минимума.
        /// </summary>
        private string _minErrorMessage;

        /// <summary>
        /// Сообщение о несоблюдении границы максимума.
        /// </summary>
        private string _maxErrorMessage;

        /// <summary>
        /// Устанавливает и возвращает значение параметра.
        /// </summary>
        public double Value
        {
            set
            {
                if (CheckRange(value))
                {
                    _value = value;
                }
            }
            get => _value;
        }

        /// <summary>
        /// Минимальное допустимое значение параметра.
        /// </summary>
        private int MinValue { get; }

        /// <summary>
        /// Максимальное допустимое значение параметра.
        /// </summary>
        private int MaxValue { get; }

        /// <summary>
        /// Устанавливает и возвращает сообщение о несоблюдении границы минимума.
        /// </summary>
        private string MinErrorMessage
        {
            set => _minErrorMessage = value + " не может быть менее " +
                                      MinValue + " мм.";
            get => _minErrorMessage;
        }

        /// <summary>
        /// Устанавливает и возвращает сообщение о несоблюдении границы максимума.
        /// </summary>
        private string MaxErrorMessage
        {
            set => _maxErrorMessage = value + " не может быть более " +
                                      MaxValue + " мм.";
            get => _maxErrorMessage;
        }

        /// <summary>
        /// Тип параметра.
        /// </summary>
        private XWingParameterType ParameterType { get; }

        /// <summary>
        /// Список ошибок введенного параметра.
        /// </summary>
        private Dictionary<XWingParameterType, string> ErrorList { get; }

        /// <summary>
        /// Создает объект класса параметра.
        /// </summary>
        /// <param name="value">Введенное значение параметра.</param>
        /// <param name="minValue">Минимальное возможное значение.</param>
        /// <param name="maxValue">Максимальное возможное значение.</param>
        /// <param name="errorMessage">Сообщение о том, какой это параметр.</param>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="errorList"></param>
        public Parameter(double value, int minValue, int maxValue, 
            string errorMessage, XWingParameterType parameterType,
            Dictionary<XWingParameterType, string> errorList)
        {
            ErrorList = errorList;
            MinValue = minValue;
            MaxValue = maxValue;
            MinErrorMessage = errorMessage;
            MaxErrorMessage = errorMessage;
            ParameterType = parameterType;
            Value = value;
        }

        /// <summary>
        /// Проверка принадлежности диапазону введенного параметра.
        /// </summary>
        /// <param name="value">Введенное значение параметра.</param>
        private bool CheckRange(double value)
        {
            if (value < MinValue)
            {
                ErrorList.Add(ParameterType, MinErrorMessage);
                return false;
            }
            if (!(value > MaxValue)) return true;
            ErrorList.Add(ParameterType, MaxErrorMessage);
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
                   expected.MinValue.Equals(MinValue) &&
                   expected.MaxValue.Equals(MaxValue) &&
                   expected.MinErrorMessage.Equals(MinErrorMessage) &&
                   expected.MaxErrorMessage.Equals(MaxErrorMessage) &&
                   expected.ErrorList.Equals(ErrorList) &&
                   expected.ParameterType.Equals(ParameterType);
        }
    }
}