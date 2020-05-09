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
    public partial class Login : Form {
        public Login() {
            InitializeComponent();
        }

        private void textBoxUser_Click(object sender, EventArgs e) {
            textBoxUser.Clear();
            pictureBoxUser.Image = Properties.Resources.user2;
            panelUser.BackColor = Color.FromArgb(104, 142, 38);
            textBoxUser.ForeColor = Color.FromArgb(104, 142, 38);

            pictureBoxPassword.Image = Properties.Resources.password;
            panelPassword.BackColor = Color.WhiteSmoke;
            textBoxUser.ForeColor = Color.WhiteSmoke;
        }

        private void textBoxPassword_Click(object sender, EventArgs e) {
            textBoxPassword.Clear();
            textBoxPassword.PasswordChar = '*';
            pictureBoxPassword.Image = Properties.Resources.password2;
            panelPassword.ForeColor = Color.FromArgb(104, 142, 38);
            textBoxPassword.ForeColor = Color.FromArgb(255,255,255);

            pictureBoxUser.Image = Properties.Resources.user;
            panelUser.BackColor = Color.WhiteSmoke;
            panelUser.ForeColor = Color.WhiteSmoke;
        }

        private void textBoxUser_Leave(object sender, EventArgs e) {
            if(textBoxUser.Text=="") {
                textBoxUser.Text = "Username";
                panelUser.BackColor = Color.White;
            }
            
        }

        private void textBoxPassword_Leave(object sender, EventArgs e) {
            if(textBoxPassword.Text=="")
            textBoxPassword.Text = "Password";

        }
    }
}
