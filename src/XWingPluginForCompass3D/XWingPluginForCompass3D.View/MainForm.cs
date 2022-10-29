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
        private readonly XWing _xWing;

        /// <summary>
        /// Конструктор основной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _xWing = new XWing();
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

            // Добавление всем TextBox события, когда пользователь вводит символ.

            BodyLengthTextBox.KeyPress += BanCharacterInput;
            WingWidthTextBox.KeyPress += BanCharacterInput;
            BowLengthTextBox.KeyPress += BanCharacterInput;
            WeaponBlasterTipLengthTextBox.KeyPress += BanCharacterInput;
            AcceleratorTurbineLengthTextBox.KeyPress += BanCharacterInput;
            AcceleratorNozzleLengthTextBox.KeyPress += BanCharacterInput;

            // Добавление всем TextBox события, когда изменятся текст.

            BodyLengthTextBox.TextChanged += FindError;
            WingWidthTextBox.TextChanged += FindError;
            BowLengthTextBox.TextChanged += FindError;
            WeaponBlasterTipLengthTextBox.TextChanged += FindError;
            AcceleratorTurbineLengthTextBox.TextChanged += FindError;
            AcceleratorNozzleLengthTextBox.TextChanged += FindError;
        }

        /// <summary>
        /// Проверка введенных значений в режиме реального времени.
        /// </summary>
        /// <param name="sender">TextBox.</param>
        /// <param name="e">Изменение текста в TextBox.</param>
        private void FindError(object sender, EventArgs e)
        {
            foreach (var keyValue in _parameterToTextBox)
            {
                keyValue.Value.BackColor = Color.White;
            }
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
                _xWing.SetParameters(bodyLength, wingWidth,
                    bowLength, weaponBlasterTipLength,
                    acceleratorTurbineLength, acceleratorNozzleLength);

                foreach (var keyValue in _xWing.Errors)
                {
                    _parameterToTextBox[keyValue.Key].BackColor = Color.LightPink;
                }
            }
            catch
            {
                CheckEmptyTextBox();
            }

        }

        /// <summary>
        /// Построение по введенным параметрам звездолет.
        /// </summary>
        /// <param name="sender">Кнопка.</param>
        /// <param name="e">Нажатие на кнопку.</param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            if(CheckEmptyTextBox())
            {
                if (_xWing.Errors.Count > 0)
                {
                    var message = string.Empty;
                    foreach (var keyValue in _xWing.Errors)
                    {
                        message += "• " + keyValue.Value + "\n" + "\n";
                    }

                    MessageBox.Show(message, @"Неверно введены данные!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _xWingBuilder.BuildDetail(_xWing);
                }
            }
            else
            {
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
        /// Поиск пустых TextBox.
        /// </summary>
        /// <returns>Возвращает true, если нет пустых ячеек,
        /// возвращает false в обратном случае.</returns>
        private bool CheckEmptyTextBox()
        {
            var counter = 0;
	        // TODO: string.Empty   ИСПРАВИЛ
			foreach (var keyValue in _parameterToTextBox.Where
                         (keyValue => keyValue.Value.Text == string.Empty))
            {
                counter += 1;
                keyValue.Value.BackColor = Color.LightPink;
            }

            return counter == 0;
        }
    }
}