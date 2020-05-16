using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class Copyright : UserControl {
        public Copyright() {
            InitializeComponent();
        }

        private void Copyright_Load(object sender, EventArgs e) {
            label1.Text = "Copyright© \nOnly Logistics RO, Inc. \nAll rights reserved.";
        }

        private void label1_Click(object sender, EventArgs e) {

        }
    }
}
