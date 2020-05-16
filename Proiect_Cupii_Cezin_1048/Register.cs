using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class Register : Form {
        Thread th;
        string connString;
        public Register() {
            InitializeComponent();
            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Users.accdb ";

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
            panelPassword.BackColor = Color.FromArgb(104, 142, 38);
            textBoxPassword.ForeColor = Color.FromArgb(255, 255, 255);

            pictureBoxUser.Image = Properties.Resources.user;
            panelUser.BackColor = Color.WhiteSmoke;
            panelUser.ForeColor = Color.WhiteSmoke;
        }

        private void textBoxUser_Leave(object sender, EventArgs e) {
            if (textBoxUser.Text == "") {
                textBoxUser.Text = "Username";
                panelUser.BackColor = Color.White;
            }

        }

        private void textBoxPassword_Leave(object sender, EventArgs e) {
            if (textBoxPassword.Text == "") {
                textBoxPassword.Text = "Parola";
                textBoxPassword.PasswordChar = (char)0;
            }


        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (checkBox1.Checked == true) {
                textBoxPassword.PasswordChar = (char)0;

            }
            else {
                textBoxPassword.PasswordChar = '*';
            }
        }
        private void buttonClose_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) {
            MsgBox msg = new MsgBox();
            OleDbConnection conexiune = new OleDbConnection(connString);
            if (textBoxUser.Text == "" || textBoxUser.Text == "Username" || textBoxPassword.Text == "") {
                msg.textProperty = "Introdu username/parola";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
            if (!Regex.IsMatch(textBoxPassword.Text, "^^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")) {
                msg.textProperty = "Parola trebuie sa fie de minim 8 caractere";
                msg.textProperty += "\n Parola trebuie sa contina cel putin o litera mica \n Parola trebuie sa contina cel putin o litera mare \n Parola trebuie sa contina o cifra";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
            try {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                comanda.CommandText = "select * from users where username='" + textBoxUser.Text +"'";
                OleDbDataReader reader = comanda.ExecuteReader();
                int count = 0;
                while (reader.Read())
                    count++;
                reader.Close();
                if (count==1) {
                    msg.textProperty = "Username deja existent!";
                    msg.Show();
                }
                if(count==0) {
                    comanda.CommandText = "INSERT INTO users VALUES(@username,@password,@isLogged,@rememberMe)";
                    comanda.Parameters.AddWithValue("@username", textBoxUser.Text);
                    comanda.Parameters.AddWithValue("@password", textBoxPassword.Text);
                    comanda.Parameters.AddWithValue("@isLogged", 1);
                    comanda.Parameters.AddWithValue("@rememberMe", 0);
                    comanda.ExecuteNonQuery();
                    comanda.CommandText = "UPDATE users SET isLogged=1 WHERE username='" + textBoxUser.Text + "'";
                    comanda.ExecuteNonQuery();
                    this.Close();
                    th = new Thread(openApp);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();


                }

            }
            catch(Exception ex) {
                msg.textProperty = ex.Message;
                msg.Show();
            }
        }

        private void openApp() {
            Application.Run(new Form1());
        }
    }
}
