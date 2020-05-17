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
using System.IO;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class Login : Form {
        Thread th;
        string connString;
        string userName = null;
        string email = null;
        string password = null;
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
            } else
            try {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                comanda.CommandText = "select * from users where username='" + textBoxUser.Text+"'" ;
                OleDbDataReader reader = comanda.ExecuteReader();
                int count = 0;
                while (reader.Read()) {
                        count++;
                        userName = reader["username"].ToString();
                        email = reader["email"].ToString();
                        password = reader["password"].ToString();
                    }
                    
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

        string MakeImageSrcData(string filename) {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return "data:image/png;base64," +
              Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
        }

        private void sendEmail() {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("onlylogistics2020@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Recuperare parola " + userName;
            mail.IsBodyHtml = true;
            //string htmlBody = File.ReadAllText("email.html");
            string htmlTemplate = "<!DOCTYPE html> <html> <head> </head> <body style=\"margin: 0; padding: 0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"padding: 10px 0 30px 0;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\"> <tr> <td align=\"center\" bgcolor=\"#70bbd9\" style=\"padding: 40px 0 30px 0; background-color: #153643; font-size: 28px; font-weight: bold; font-family: 'Montserrat' , sans-serif;\"> <img src=\"" + MakeImageSrcData("logo2.0.png") + "\" alt=\"Creating Email Magic\" width=\"200\" height=\"200\" style=\"display: block;\" /> </td> </tr> <tr> <td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 24px;\"> <b>Draga {0}, </b> </td> </tr> <tr> <td style=\"padding: 20px 0 20px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Intrucat ai facut o cerere pentru recuperarea contului, il vei gasi atasat mai jos. <p><b>Datele tale de acces sunt:</b></p> <p>Nume de utilizator: {1}</p> <p>Parola: {2}</p> <br> <p>Suntem bucurosi ca ai ales serviciile noastre.</p> </td> </tr> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify\"> <b>Cu drag,<br></b><b>Echipa Only Logistics</b> </td> </tr> </table> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\"> &nbsp; </td> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify;\"> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#ee4c50\" style=\"padding: 30px 30px 30px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\"> &reg; Cezin Cupii, Bucharest 2020<br /> </td> <td align=\"right\" width=\"25%\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("tw.png") + "\" alt=\"Twitter\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("fb.png") + "\" alt=\"Facebook\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>";
            string mailBody = string.Format(htmlTemplate, userName, userName,password);
            //mail.Body = "<body style=\"margin: 0; padding: 0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"padding: 10px 0 30px 0;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\"> <tr> <td align=\"center\" bgcolor=\"#70bbd9\" style=\"padding: 40px 0 30px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;\"> </td> </tr> <tr> <td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 24px;\"> <b>Lorem ipsum dolor sit amet!</b> </td> </tr> <tr> <td style=\"padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> </table> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\"> &nbsp; </td> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#ee4c50\" style=\"padding: 30px 30px 30px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\"> &reg; Someone, somewhere 2013<br /> <a href=\"#\" style=\"color: #ffffff;\"> <font color=\"#ffffff\">Unsubscribe</font> </a> to this newsletter instantly </td> <td align=\"right\" width=\"25%\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> </a> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> </a> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> ";
            mail.Body = mailBody;

 
            //parolafoarteputernica
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("onlylogistics2020@gmail.com", "parolafoarteputernica");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            MsgBox msg = new MsgBox();
            msg.textProperty = "Te rugam sa iti verifici adresa de email";
            msg.Show();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e) {
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
