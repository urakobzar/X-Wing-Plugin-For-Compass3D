using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XWingPluginForCompass3D.Model;

namespace XWingPluginForCompass3D.View
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объект связи с Компас-3D
        /// </summary>
        private KompasWrapper _kompasWrapper;

        public MainForm()
        {
            InitializeComponent();
            _kompasWrapper = new KompasWrapper();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {
                XWingParameters xWing = null;
                xWing = new XWingParameters(300, 300, 50, 80, 150, 50);
                _kompasWrapper.StartKompas();
                _kompasWrapper.BuildXWing(xWing);
            }
            catch
            {
                MessageBox.Show("Невозможно построить деталь!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
