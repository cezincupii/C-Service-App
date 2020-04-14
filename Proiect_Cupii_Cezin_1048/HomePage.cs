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
    public partial class HomePage : Form {
        public HomePage() {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e) {
            timer1.Start();
            label2.Text = DateTime.Now.ToLongTimeString();
            //label3.Text = DateTime.Now.ToLongDateString();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            label2.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void panel5_Resize(object sender, EventArgs e) {
           
        }

        private void panel5_Resize_1(object sender, EventArgs e) {
            int x = (this.Width - panel5.Width) / 2;
            int y = (this.Width - panel5.Width) / 14;
            panel5.Location = new Point(x, y);
        }
    }
}
