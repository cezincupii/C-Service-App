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
using System.Net.Mail;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class Login : Form {
        Thread th;
        string connString;
        public Login() {
            InitializeComponent();
            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Users.accdb";
           
           
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
            if(textBoxPassword.Text=="") {
                textBoxPassword.Text = "Parola";
                textBoxPassword.PasswordChar = (char)0;
            }
            

        }

        private void button1_Click(object sender, EventArgs e) {
            MsgBox msg = new MsgBox();
            OleDbConnection conexiune = new OleDbConnection(connString);
            if(textBoxUser.Text==""||textBoxUser.Text=="Username" || textBoxPassword.Text == "") {
                msg.textProperty = "Introdu username/parola";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
            try {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                comanda.CommandText = "select * from users where username='" + textBoxUser.Text + "' and password='" + textBoxPassword.Text + "'";
                OleDbDataReader reader = comanda.ExecuteReader();
                int count = 0;
                while (reader.Read())
                    count++;
                reader.Close();
                if (count==1) {
                    comanda.CommandText = "UPDATE users SET isLogged=1 WHERE username='" + textBoxUser.Text + "'";
                    comanda.ExecuteNonQuery();
                    this.Close();
                    th = new Thread(openApp);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                if(count==0) {
                    msg.textProperty = "User/parola gresite";
                    msg.Show();
                }
                conexiune.Close();
            }
            catch(Exception ex) {
                msg.textProperty = ex.Message;
                msg.Show();
            }
            finally {

            }

        }

        private void openApp() {
            Application.Run(new Form1());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if(checkBox1.Checked==true) {
                textBoxPassword.PasswordChar = (char)0;

            }
            else {
                textBoxPassword.PasswordChar ='*';
            }
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.Close();
            th = new Thread(openRegister);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openRegister() {
            Application.Run(new Register());
        }

        private void Login_Load(object sender, EventArgs e) {
            textBoxPassword.PasswordChar = '*';
            OleDbConnection conexiune = new OleDbConnection(connString);
            try {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                comanda.CommandText = "UPDATE users SET isLogged=0 WHERE 1<2";
                comanda.ExecuteNonQuery();
                comanda.CommandText = "SELECT * FROM USERS WHERE rememberMe=1";
                OleDbDataReader reader = comanda.ExecuteReader();
                while (reader.Read()) {
                    textBoxUser.Text = reader["username"].ToString();
                    textBoxPassword.Text = reader["password"].ToString();
                }   
                reader.Close();
                
            }
            catch(Exception ex) {
                
            }
            conexiune.Close();

        }

        private void checkBoxRememberMe_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxRememberMe.Checked==true) {
                OleDbConnection conexiune = new OleDbConnection(connString);
                try {
                    conexiune.Open();
                    OleDbCommand comanda = new OleDbCommand();
                    comanda.Connection = conexiune;
                    comanda.CommandText = "UPDATE users SET rememberMe=0 WHERE 1<2";
                    comanda.ExecuteNonQuery();
                    comanda.CommandText = "UPDATE users SET rememberMe=1 WHERE username='"+textBoxUser.Text+"'";
                    comanda.ExecuteNonQuery();
                }
                catch (Exception ex) {

                }
                conexiune.Close();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            MsgBox msg = new MsgBox();
            OleDbConnection conexiune = new OleDbConnection(connString);
            if (textBoxUser.Text == "" || textBoxUser.Text == "Username" || textBoxPassword.Text == "") {
                msg.textProperty = "Introdu username/parola";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
            try {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                comanda.CommandText = "select * from users where username='" + textBoxUser.Text+"'" ;
                OleDbDataReader reader = comanda.ExecuteReader();
                int count = 0;
                while (reader.Read())
                    count++;
                reader.Close();
                if (count == 1) {
                   sendEmail();

                }
                if (count == 0) {
                    msg.textProperty = "Contul nu exista, te rugam sa iti creezi unul";
                    msg.Show();
                }
                conexiune.Close();
            }
            catch (Exception ex) {
                msg.textProperty = ex.Message;
                msg.Show();
            }
            finally {

            }

        }

        private void sendEmail() {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("sisc.devops@gmail.com");
            mail.To.Add("cupiicezin18@stud.ase.ro");
            mail.Subject = "Cerere resetare parola user:" + textBoxUser.Text;
            mail.Body = "#Acest mail este trimis automat#\n\n" +
                "Va rog sa imi resetati parola de pe contul" + textBoxUser.Text + "\n" + DateTime.Now;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("sisc.devops@gmail.com", "devops2020");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            MsgBox msg = new MsgBox();
            msg.textProperty = "A fost trimis un mail catre administrator \n" +
                "In maxim 2 zile lucratoare veti primi un raspuns";
            msg.Show();
        }
    }
}
