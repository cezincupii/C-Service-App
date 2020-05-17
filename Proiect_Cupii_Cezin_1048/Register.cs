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
        string connString;
        string userName = null;
        string email = null;
        string password = null;
        public Register() {
            InitializeComponent();
            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Users.accdb ";
            textBoxPassword.PasswordChar = '*';
            
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
                    comanda.CommandText = "INSERT INTO users VALUES(@username,@password,@isLogged,@rememberMe,@email)";
                    comanda.Parameters.AddWithValue("@username", textBoxUser.Text);
                    comanda.Parameters.AddWithValue("@password", textBoxPassword.Text);
                    comanda.Parameters.AddWithValue("@isLogged", 1);
                    comanda.Parameters.AddWithValue("@rememberMe", 0);
                    comanda.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    comanda.ExecuteNonQuery();
                    comanda.CommandText = "UPDATE users SET isLogged=1 WHERE username='" + textBoxUser.Text + "'";
                    comanda.ExecuteNonQuery();

                        userName = textBoxUser.Text;
                        password = textBoxPassword.Text;
                        email = textBoxEmail.Text;
                        sendEmail();


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
            mail.Subject = "Bine ai venit in lumea Only Logistics";
            mail.IsBodyHtml = true;
            //string htmlBody = File.ReadAllText("email.html");
            string htmlTemplate = "<!DOCTYPE html> <html> <head> </head> <body style=\"margin: 0; padding: 0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"padding: 10px 0 30px 0;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\"> <tr> <td align=\"center\" bgcolor=\"#70bbd9\" style=\"padding: 40px 0 30px 0; background-color: #153643; font-size: 28px; font-weight: bold; font-family: 'Montserrat' , sans-serif;\"> <img src=\"" + MakeImageSrcData("logo2.0.png") + "\" alt=\"Creating Email Magic\" width=\"200\" height=\"200\" style=\"display: block;\" /> </td> </tr> <tr> <td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 24px;\"> <b>Bine ai venit, {0} </b> </td> </tr> <tr> <td style=\"padding: 20px 0 20px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Contul tău a fost creat cu success și din acest moment ești rezident oficial al lumii Only Logistics. <p><b>Datele tale de acces sunt:</b></p> <p>Nume de utilizator: {1}</p> <p>Parola: {2}</p> <p>Suntem bucurosi ca ai ales serviciile noastre.</p> <b style=\"text-align: center; font-size: 18px;\">Aici sunt doar cateva avantaje in folosirea aplicatiei noastre:</b> </td> </tr> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> <img src=\"" + MakeImageSrcData("left.png") + "\" alt=\"\" width=\"100%\" height=\"140\" style=\"display: block;\" /> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify\"> Cu ajutorul Only Logistics poti inregistra cu usurinta toate noile masini, soferi, rute sau transporturi. In cadrul aplicatiei noastre poti adauga si modele fiecarei masini, km condusi de soferi in fiecare luna, greutate fiecarui colet, si fiecare oprire a rutelor </td> </tr> </table> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\"> &nbsp; </td> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> <img src=\"" + MakeImageSrcData("right.png") + "\" alt=\"\" width=\"100%\" height=\"140\" style=\"display: block;\" /> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify;\"> Prin aplicatia noastra poti uita de cautarea prin rapoarte, deoarece acestea pot fi primite printr-un singur click, direct pe Email. Raporturile contin toate elementele, cat si un grafic care arata numarul de km,salarii medii. </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#ee4c50\" style=\"padding: 30px 30px 30px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\"> &reg; Cezin Cupii, Bucharest 2020<br /> </td> <td align=\"right\" width=\"25%\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("tw.png") + "\" alt=\"Twitter\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("fb.png") + "\" alt=\"Facebook\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>";
            string mailBody = string.Format(htmlTemplate, userName, userName, password);
            //mail.Body = "<body style=\"margin: 0; padding: 0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"padding: 10px 0 30px 0;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\"> <tr> <td align=\"center\" bgcolor=\"#70bbd9\" style=\"padding: 40px 0 30px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;\"> </td> </tr> <tr> <td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 24px;\"> <b>Lorem ipsum dolor sit amet!</b> </td> </tr> <tr> <td style=\"padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> </table> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\"> &nbsp; </td> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#ee4c50\" style=\"padding: 30px 30px 30px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\"> &reg; Someone, somewhere 2013<br /> <a href=\"#\" style=\"color: #ffffff;\"> <font color=\"#ffffff\">Unsubscribe</font> </a> to this newsletter instantly </td> <td align=\"right\" width=\"25%\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> </a> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> </a> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> ";
            mail.Body = mailBody;

            

            //parolafoarteputernica
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("onlylogistics2020@gmail.com", "parolafoarteputernica");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            MsgBox msg = new MsgBox();
            msg.textProperty = "Raportul a fost trimis !";
            msg.Show();
        }

        private void openApp() {
            Application.Run(new Form1());
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
