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
        private XWingBuilder XWingBuilder { get; }

        /// <summary>
        /// Словарь Тип параметра-TextBox.
        /// </summary>
        private Dictionary <XWingParameterType, TextBox> ParameterToTextBox { get; }

        /// <summary>
        /// Объект параметров X-Wing.
        /// </summary>
        private XWing XWingObject { get; }

        /// <summary>
        /// Конструктор основной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            XWingObject = new XWing();
            XWingBuilder = new XWingBuilder();
            ParameterToTextBox = new Dictionary <XWingParameterType, TextBox>
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
                XWingObject.SetParameters(bodyLength, wingWidth, 
                    bowLength,weaponBlasterTipLength,
                    acceleratorTurbineLength, acceleratorNozzleLength);
                if (XWingObject.ErrorList.Count > 0)
                {
                    ShowErrorList(XWingObject.ErrorList);
                }
                else
                {
                    XWingBuilder.BuildDetail(XWingObject);
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
            foreach (var keyValue in ParameterToTextBox)
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
            // TODO: string.Empty ИСПРАВИЛ
            var message = string.Empty;
            foreach (var keyValue in errorList)
            {
                if (!ParameterToTextBox.TryGetValue(keyValue.Key, out var textBox)) 
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
            foreach (var keyValue in ParameterToTextBox.Where
                         (keyValue => keyValue.Value.Text == ""))
            {
                keyValue.Value.BackColor = Color.LightPink;
            }
        }
    }
}