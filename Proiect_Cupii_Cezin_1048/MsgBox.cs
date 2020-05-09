using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class MsgBox : Form {
        public string textProperty { get; set; }
        public MsgBox() {
            InitializeComponent();
            
        }

        private void MsgBox_Load(object sender, EventArgs e) {
            textLabel.Text = this.textProperty;
            
        }

        private void buttonAdaugaModel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
