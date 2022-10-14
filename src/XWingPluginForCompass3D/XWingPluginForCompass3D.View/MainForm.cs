using System;
using System.Collections.Generic;
using System.Drawing;
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
        private XWingBuilder _xWingBuilder;

        /// <summary>
        /// Словарь, где ключ: параметр звездолёта, значение: соотвествующий тексбокс.
        /// </summary>
        private Dictionary <XWingParameters, TextBox> _parameterToTextBox;

        /// <summary>
        /// Устанавливает и возвращает объект класса построения детали.
        /// </summary>
        public XWingBuilder XWingBuilder
        {
            set
            {
                _xWingBuilder = value;
            }
            get
            {
                return _xWingBuilder;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает словарь параметров-текстбоксов.
        /// </summary>
        public Dictionary <XWingParameters, TextBox> ParameterToTextBox
        {
            set
            {
                _parameterToTextBox = value;
            }
            get
            {
                return _parameterToTextBox;
            }            
        }

        /// <summary>
        /// Конструктор основной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            XWingBuilder = new XWingBuilder();
            ParameterToTextBox = new Dictionary <XWingParameters, TextBox>();
            ParameterToTextBox.Add(XWingParameters.BodyLength, BodyLengthTextBox);
            ParameterToTextBox.Add(XWingParameters.WingWidth, WingWidthTextBox);
            ParameterToTextBox.Add(XWingParameters.BowLength, BowLengthTextBox);
            ParameterToTextBox.Add(XWingParameters.WeaponBlasterTipLength, 
                WeaponBlasterTipLengthTextBox);
            ParameterToTextBox.Add(XWingParameters.AcceleratorTurbineLength, 
                AcceleratorTurbineLengthTextBox);
            ParameterToTextBox.Add(XWingParameters.AcceleratorNozzleLength, 
                AcceleratorNozzleLengthTextBox);

            // Добавления всем тексбоксам события, когда пользователь вводит символ.

            BodyLengthTextBox.KeyPress += 
                new KeyPressEventHandler(BanCharacterInput);
            WingWidthTextBox.KeyPress += 
                new KeyPressEventHandler(BanCharacterInput);
            BowLengthTextBox.KeyPress += 
                new KeyPressEventHandler(BanCharacterInput);
            WeaponBlasterTipLengthTextBox.KeyPress += 
                new KeyPressEventHandler(BanCharacterInput);
            AcceleratorTurbineLengthTextBox.KeyPress += 
                new KeyPressEventHandler(BanCharacterInput);
            AcceleratorNozzleLengthTextBox.KeyPress += 
                new KeyPressEventHandler(BanCharacterInput);
        }

        /// <summary>
        /// Построение по введённым параметрам звездолёт.
        /// </summary>
        /// <param name="sender">Кнопка.</param>
        /// <param name="e">Нажатие на кнопку.</param>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            SetWhiteColor();
            try
            {
                XWing xWing = null;
                double bodyLength = double.Parse(BodyLengthTextBox.Text);
                double wingWidth = double.Parse(WingWidthTextBox.Text);
                double bowLength = double.Parse(BowLengthTextBox.Text);
                double weaponBlasterTipLength = 
                    double.Parse(WeaponBlasterTipLengthTextBox.Text);
                double acceleratorTurbineLength = 
                    double.Parse(AcceleratorTurbineLengthTextBox.Text);
                double acceleratorNozzleLength = 
                    double.Parse(AcceleratorNozzleLengthTextBox.Text);
                xWing = new XWing(bodyLength, wingWidth, bowLength,weaponBlasterTipLength,
                    acceleratorTurbineLength, acceleratorNozzleLength);
                if (xWing.ErrorList.Count > 0)
                {
                    ShowErrorList(xWing.ErrorList);
                }
                else
                {
                    XWingBuilder.BuildDetail(xWing);
                }
            }
            catch
            {
                FindEmptyTextBox();
                MessageBox.Show("Ошибка при построении! Проверьте введенные данные.", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Запрет ввода символов и больше одной точки в число.
        /// </summary>
        /// <param name="sender">Текстбокс.</param>
        /// <param name="e">Нажатие на клавишу клавиатуры.</param>
        private void BanCharacterInput(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar)
                && !((e.KeyChar == ',') && 
                (((TextBox)sender).Text.IndexOf(",") == -1)))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Установка белого цвета тексбоксов.
        /// </summary>
        private void SetWhiteColor()
        {
            foreach (KeyValuePair <XWingParameters, TextBox> 
                keyValue in ParameterToTextBox)
            {
                keyValue.Value.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Демонстрация неправильно введённых параметров.
        /// </summary>
        /// <param name="errorList">Список выявленных ошибок</param>
        private void ShowErrorList(Dictionary <XWingParameters, string> errorList)
        {
            string message = "";
            foreach (KeyValuePair <XWingParameters, string> keyValue in errorList)
            {
                if (ParameterToTextBox.TryGetValue(keyValue.Key, out TextBox textBox))
                {
                    textBox.BackColor = Color.LightPink;
                    message += "• " + keyValue.Value + "\n" + "\n";
                }
            }
            MessageBox.Show(message, "Неверно введены данные!", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Поиск пустых тексбоксов.
        /// </summary>
        private void FindEmptyTextBox()
        {
            foreach (KeyValuePair<XWingParameters, TextBox>
                keyValue in ParameterToTextBox)
            {
                if (keyValue.Value.Text == "")
                {
                    keyValue.Value.BackColor = Color.LightPink;
                }
            }
        }
    }
}