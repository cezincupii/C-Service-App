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
using System.IO;
using System.Net.Mail;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class Register : Form {
        Thread th;
        Random rnd = new Random();
        int random;
        string connString;
        string userName = null;
        string email = null;
        string password = null;
        public Register() {
           
            InitializeComponent();
            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Users.accdb ";
            textBoxPassword.PasswordChar = '*';
            random = rnd.Next(1000, 10000);
            
        }
        private void textBoxUser_Click(object sender, EventArgs e) {
            textBoxUser.Clear();
            pictureBoxUser.Image = Properties.Resources.user2;
            panelUser.BackColor = Color.FromArgb(104, 142, 38);
            textBoxUser.ForeColor = Color.FromArgb(104, 142, 38);

            pictureBoxPassword.Image = Properties.Resources.password;
            panelPassword.BackColor = Color.WhiteSmoke;
            textBoxUser.ForeColor = Color.WhiteSmoke;

            pictureBoxEmail.Image = Properties.Resources.email__4_;
            panelEmail.BackColor = Color.WhiteSmoke;
            panelEmail.ForeColor = Color.WhiteSmoke;
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

       
        private void textBoxEmail_Click(object sender, EventArgs e) {
            textBoxEmail.Clear();
            pictureBoxEmail.Image = Properties.Resources.email__3_;
            panelEmail.BackColor = Color.FromArgb(104, 142, 38);
            textBoxEmail.ForeColor = Color.WhiteSmoke;

            pictureBoxPassword.Image = Properties.Resources.password;
            panelPassword.BackColor = Color.WhiteSmoke;
            textBoxUser.ForeColor = Color.WhiteSmoke;

            pictureBoxUser.Image = Properties.Resources.user;
            panelUser.BackColor = Color.WhiteSmoke;
            panelUser.ForeColor = Color.WhiteSmoke;
        }
        private void textBoxEmail_Leave(object sender, EventArgs e) {
            if(textBoxEmail.Text=="") {
                textBoxEmail.Text = "Email";
                panelEmail.BackColor = Color.White;
            }
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
            else
            if (!Regex.IsMatch(textBoxEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")) {
                msg.textProperty = "Introdu o adresa de email valida!";
                msg.Show();
            }
            else
            if (!Regex.IsMatch(textBoxPassword.Text, "^^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")) {
                msg.textProperty = "Parola trebuie sa fie de minim 8 caractere";
                msg.textProperty += "\n Parola trebuie sa contina cel putin o litera mica \n Parola trebuie sa contina cel putin o litera mare \n Parola trebuie sa contina o cifra";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
            else
            try {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                comanda.CommandText = "select * from users where email='" + textBoxEmail.Text +"'";
                OleDbDataReader reader = comanda.ExecuteReader();
                int count = 0;
                while (reader.Read())
                    count++;
                reader.Close();
                if (count==1) {
                    msg.textProperty = "Email deja existent!";
                    msg.Show();
                }
                if(count==0) {
                    comanda.CommandText = "INSERT INTO users VALUES(@username,@password,@isLogged,@rememberMe,@email,@activationCode)";
                    comanda.Parameters.AddWithValue("@username", textBoxUser.Text);
                    comanda.Parameters.AddWithValue("@password", textBoxPassword.Text);
                    comanda.Parameters.AddWithValue("@isLogged", 1);
                    comanda.Parameters.AddWithValue("@rememberMe", 0);
                    comanda.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    comanda.Parameters.AddWithValue("@activationCode", random);
                        
                    comanda.ExecuteNonQuery();
                    comanda.CommandText = "UPDATE users SET isLogged=1 WHERE username='" + textBoxUser.Text + "'";
                    comanda.ExecuteNonQuery();

                        userName = textBoxUser.Text;
                        password = textBoxPassword.Text;
                        email = textBoxEmail.Text;


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
            Application.Run(new ActivationForm(email));
        }

        private void Register_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode==Keys.Enter) {
                button1.PerformClick();
            }
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                button1.PerformClick();
            }
        }

        
    }
}
