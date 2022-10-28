using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XWingPluginForCompass3D.Model;
using XWingPluginForCompass3D.Wrapper;

namespace XWingPluginForCompass3D.View
{
    /// <summary>
    /// Класс основной формы, являющейся пользовательским интерфейсом.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объект класса построения детали.
        /// </summary>
        // TODO: заменить на поля   ИСПРАВИЛ
        private readonly XWingBuilder _xWingBuilder;

        /// <summary>
        /// Словарь Тип параметра-TextBox.
        /// </summary>
        // TODO: заменить на поля   ИСПРАВИЛ
        private readonly Dictionary<XWingParameterType, TextBox> 
            _parameterToTextBox;

        /// <summary>
        /// Объект параметров X-Wing.
        /// </summary>
        // TODO: заменить на поля   ИСПРАВИЛ
        private readonly XWing _xWingObject;

        /// <summary>
        /// Конструктор основной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _xWingObject = new XWing();
            _xWingBuilder = new XWingBuilder();
            _parameterToTextBox = new Dictionary <XWingParameterType, TextBox>
            {
	            { XWingParameterType.BodyLength, BodyLengthTextBox },
	            { XWingParameterType.WingWidth, WingWidthTextBox },
	            { XWingParameterType.BowLength, BowLengthTextBox },
	            { XWingParameterType.WeaponBlasterTipLength, 
                    WeaponBlasterTipLengthTextBox },
	            { XWingParameterType.AcceleratorTurbineLength, 
                    AcceleratorTurbineLengthTextBox },
	            { XWingParameterType.AcceleratorNozzleLength, 
                    AcceleratorNozzleLengthTextBox }
            };

            // Добавления всем TextBox события, когда пользователь вводит символ.

            BodyLengthTextBox.KeyPress += BanCharacterInput;
            WingWidthTextBox.KeyPress += BanCharacterInput;
            BowLengthTextBox.KeyPress += BanCharacterInput;
            WeaponBlasterTipLengthTextBox.KeyPress += BanCharacterInput;
            AcceleratorTurbineLengthTextBox.KeyPress += BanCharacterInput;
            AcceleratorNozzleLengthTextBox.KeyPress += BanCharacterInput;
        }

        /// <summary>
        /// Построение по введенным параметрам звездолет.
        /// </summary>
        /// <param name="sender">Кнопка.</param>
        /// <param name="e">Нажатие на кнопку.</param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            SetWhiteColor();
            try
            {
                var bodyLength = double.Parse(BodyLengthTextBox.Text);
                var wingWidth = double.Parse(WingWidthTextBox.Text);
                var bowLength = double.Parse(BowLengthTextBox.Text);
                var weaponBlasterTipLength = 
                    double.Parse(WeaponBlasterTipLengthTextBox.Text);
                var acceleratorTurbineLength = 
                    double.Parse(AcceleratorTurbineLengthTextBox.Text);
                var acceleratorNozzleLength = 
                    double.Parse(AcceleratorNozzleLengthTextBox.Text);
                _xWingObject.SetParameters(bodyLength, wingWidth, 
                    bowLength,weaponBlasterTipLength,
                    acceleratorTurbineLength, acceleratorNozzleLength);
                if (_xWingObject.ErrorList.Count > 0)
                {
                    ShowErrorList(_xWingObject.ErrorList);
                }
                else
                {
                    _xWingBuilder.BuildDetail(_xWingObject);
                }
            }
            catch
            {
                FindEmptyTextBox();
                MessageBox.Show(
	                @"Ошибка при построении! Проверьте введенные данные.", 
                    @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Запрет ввода символов и больше одной точки в число.
        /// </summary>
        /// <param name="sender">TextBox.</param>
        /// <param name="e">Нажатие на клавишу клавиатуры.</param>
        private static void BanCharacterInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                && !((e.KeyChar == ',') && 
                (((TextBox)sender).Text.IndexOf
                    (",", StringComparison.Ordinal) == -1)))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Установка белого цвета TextBox.
        /// </summary>
        private void SetWhiteColor()
        {
            foreach (var keyValue in _parameterToTextBox)
            {
                keyValue.Value.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Демонстрация неправильно введенных параметров.
        /// </summary>
        /// <param name="errorList">Список выявленных ошибок</param>
        private void ShowErrorList(Dictionary <XWingParameterType, string> errorList)
        {
            var message = string.Empty;
            foreach (var keyValue in errorList)
            {
                if (!_parameterToTextBox.TryGetValue(keyValue.Key, out var textBox)) 
                    continue;
                textBox.BackColor = Color.LightPink;
                message += "• " + keyValue.Value + "\n" + "\n";
            }
            MessageBox.Show(message, @"Неверно введены данные!", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Поиск пустых TextBox.
        /// </summary>
        private void FindEmptyTextBox()
        {
	        // TODO: string.Empty   ИСПРАВИЛ
			foreach (var keyValue in _parameterToTextBox.Where
                         (keyValue => keyValue.Value.Text == string.Empty))
            {
                keyValue.Value.BackColor = Color.LightPink;
            }
        }
    }
}