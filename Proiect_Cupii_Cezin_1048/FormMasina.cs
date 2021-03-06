﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Net.Mail;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class FormMasina : Form {
        string connString;
        List<Masina> listaMasina = new List<Masina>();
        string userName = null;
        string email = null;
        string password = null;
        int random;

        public FormMasina() {
            InitializeComponent();
            panelLeft.Hide();
            Random rnd = new Random();
            random = rnd.Next(1, 100);
            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Users.accdb ";
            OleDbConnection conexiune = new OleDbConnection(connString);
            conexiune.Open();
            OleDbCommand comanda = new OleDbCommand("SELECT username,email,password from users where isLogged=1");
            comanda.Connection = conexiune;
            OleDbDataReader reader = comanda.ExecuteReader();
            while (reader.Read()) {
                userName = reader["username"].ToString();
                email = reader["email"].ToString();
                password = reader["password"].ToString();
            }
            reader.Close();
            conexiune.Close();
        }

        //private void FormMasina_Resize(object sender, EventArgs e) {
        //    int x = (this.Width - panel5.Width) /2;
        //    int y = (this.Width - panel5.Width) /14;
        //    panel5.Location = new Point(x, y);
        //}

        private void tbID_Click(object sender, EventArgs e) {
            tbID.Clear();
            tbID.ForeColor = Color.WhiteSmoke;
            panel2.BackColor = Color.FromArgb(104, 142, 38);

         

        }

        private void tbFirma_Click(object sender, EventArgs e) {
            tbFirma.Clear();
            tbFirma.ForeColor = Color.WhiteSmoke;
            panel1.BackColor = Color.FromArgb(104, 142, 38);

        
        }

        private void tbAnFabricatie_Click(object sender, EventArgs e) {
            tbAnFabricatie.Clear();
            tbAnFabricatie.ForeColor = Color.WhiteSmoke;
            panel4.BackColor = Color.FromArgb(104, 142, 38);

        }

        private void tbPret_Click(object sender, EventArgs e) {
            tbPret.Clear();
            tbPret.ForeColor = Color.WhiteSmoke;
            panel3.BackColor = Color.FromArgb(104, 142, 38);


        }

        private void button1_Click(object sender, EventArgs e) {
            MsgBox msg = new MsgBox();
                bool valid = true;
            if (tbID.Text == "" || tbID.Text == "ID") {
                valid = false;
                msg.textProperty += "INTRODU ID UL! \n";  
            }
            else if (tbFirma.Text == "" || tbFirma.Text == "Firma") {
                msg.textProperty += "INTRODU FIRMA! \n";
                valid = false;
            }
            else if (tbAnFabricatie.Text == "" || tbAnFabricatie.Text == "An Fabricatie") {
                msg.textProperty += "INTRODU ANUL FABRICATIEI! \n";
                valid = false;
            }
            else if (tbPret.Text == "" || tbPret.Text == "Pret") {
                msg.textProperty += "INTRODU PRETUL!\n";
                valid = false;
            }
            else if (!Regex.IsMatch(tbFirma.Text, @"^[a-zA-Z]+$")) {
                valid = false;
                msg.textProperty += "Numele firmei trebuie sa contina doar litere";
                tbFirma.Text = "";
            }
            else if (!Regex.IsMatch(tbAnFabricatie.Text, "^[0-9]*$")) {
                valid = false;
                msg.textProperty += "Anul fabricatiei trebuie sa fie alcatuit din cifre";
                tbAnFabricatie.Text = "";
            }
            else if (Convert.ToInt32(tbAnFabricatie.Text) > 2020) {
                valid = false;
                msg.textProperty += "Anul fabricatiei trebuie sa fie < 2020";
                tbAnFabricatie.Text = "";
            }
            else if (!Regex.IsMatch(tbPret.Text, "^[0-9]*$")) {
                valid = false;
                msg.textProperty += "Pretul trebuie sa fie alcatuit din cifre";
                tbPret.Text = "";
            }
            else if (Convert.ToInt32(tbPret.Text)<0) {
                valid = false;
                msg.textProperty += "Pretul trebuie sa fie >0";
                tbPret.Text = "";
            }

            if(valid)
            try {

                int id = Convert.ToInt32(tbID.Text);
                string firma = tbFirma.Text;
                int anFabricatie = Convert.ToInt32(tbAnFabricatie.Text);
                int pret = Convert.ToInt32(tbPret.Text);
                int[] modele = new int[10];
                for (int i = 0; i < 10; i++)
                    modele[i] = 0;       
                    Masina m = new Masina(id, firma, anFabricatie, pret, modele);
                    listaMasina.Add(m);
                    msg.textProperty = "Felicitari! Ai introdus un sofer cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }
            catch (Exception ex) {
                //MessageBox.Show(ex.Message);              
            }
            finally {
                tbID.Text = "ID";
                tbFirma.Text = "Firma";
                tbAnFabricatie.Text = "An Fabricatie";
                tbPret.Text = "Pret";
            }
            else {
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }

        }

        private void FormMasina_Load(object sender, EventArgs e) {
            labelDashed.Hide();
            panelAfisareMasini.Hide();
            panelAdaugaModel.Hide();
            this.KeyPreview = true;
            
        }

        private void buttonAdaugareMasina_Click(object sender, EventArgs e) {
            panelAfisareMasini.Hide();
            panelAdaugareMasina.Show();
            panelAdaugaModel.Hide();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareMasina.Height;
            panelLeft.Top = buttonAdaugareMasina.Top;
        }

        private void buttonVizualizareMasini_Click(object sender, EventArgs e) {
            panelAdaugareMasina.Hide();
            panelAfisareMasini.Show();
            panelAdaugaModel.Hide();
            panelLeft.Show();
            panelLeft.Height = buttonVizualizareMasini.Height;
            panelLeft.Top = buttonVizualizareMasini.Top;

            //foreach (Masina m in listaMasina)
            //    tbAfisareMasini.Text += m.ToString() + Environment.NewLine;
            foreach (Masina m in listaMasina) {
                ListViewItem itm = new ListViewItem(m.Id.ToString());
                itm.SubItems.Add(m.Firma);
                itm.SubItems.Add(m.AnFabricate.ToString());
                itm.SubItems.Add(m.Pret.ToString());
                listView1.Items.Add(itm);
                
            }

        }

        private void buttonSalvareMasini_Click(object sender, EventArgs e) {
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach(Masina m in listaMasina) {
                    sw.WriteLine(m.ToString());
                }
                
                sw.Close(); 
            }
        }

        private void buttonCitireMasini_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = linie.Split(',');
                        int id = Convert.ToInt32(elements[0]);
                        string firma = elements[1];
                        int anFabricatie = Convert.ToInt32(elements[2]);
                        int pret = Convert.ToInt32(elements[3]);
                        int[] modele = new int[10];
                        for (int i = 0; i < 6; i++) {
                            modele[i] = Convert.ToInt32(elements[i + 4]);
                        }
                        Masina m = new Masina(id, firma, anFabricatie, pret, modele);
                        listaMasina.Add(m);
                    }
                }
                catch(Exception ex) {
                    MsgBox msg = new MsgBox {
                        textProperty = "DATELE S-AU INCARCAT CU SUCCES!"
                    };
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                   
                }

                sr.Close();
            }
        }

      

       

        private void buttonAdaugareModel_Click(object sender, EventArgs e) {
            panelAfisareMasini.Hide();
            panelAdaugareMasina.Hide();
            panelAdaugaModel.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareModel.Height;
            panelLeft.Top = buttonAdaugareModel.Top;
        }

        private void buttonAdaugaModel_Click(object sender, EventArgs e) {
           
                bool ok = true;
                if (ok) {
                    foreach (Masina m in listaMasina) {

                        if (m.Id == Convert.ToInt32(textBoxAMID.Text)) {
                            ok = false;
                            string[] modeleS = textBoxModele.Text.Split(',');
                            int[] modele = new int[modeleS.Length];
                            for (int i = 0; i < modeleS.Length; i++)
                                modele[i] = Convert.ToInt32(modeleS[i]);
                        }

                    }
                    if (ok == false) {
                    MsgBox msg = new MsgBox();
                    msg.textProperty = "Felicitari! Ai introdus modele cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();

                }
                        
                }
             if(ok==true) {
                MsgBox msg = new MsgBox {
                        textProperty = "Nu a fost gasit ID-ul"
                    };
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
            }
        }

        private void textBoxAMID_Click(object sender, EventArgs e) {
            textBoxAMID.Clear();
            textBoxAMID.ForeColor = Color.WhiteSmoke;
            panel10.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void textBoxModele_Click(object sender, EventArgs e) {
            textBoxModele.Clear();
            textBoxModele.ForeColor = Color.WhiteSmoke;
            panel9.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void buttonSalvareMasini_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Salveaza masinile (CTRL+S)", buttonSalvareMasini);
        }

        private void buttonCitireMasini_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Incarca masinile (CTRL+I)", buttonCitireMasini);
        }
        private void buttonAdaugareMasina_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga o masina (CTRL+N)", buttonAdaugareMasina);
        }
        private void buttonAdaugareModel_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga un model nou (CTRL+H)", buttonAdaugareModel);
        }
        private void buttonPrint_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Printeaza datele(CTRL+P)", buttonPrint);
        }
        private void FormMasina_KeyDown(object sender, KeyEventArgs e) {
            if(e.Control==true && e.KeyCode==Keys.S) {
                buttonSalvareMasini.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.I) {
               buttonCitireMasini.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.N) {
               buttonAdaugareMasina.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.H) {
                buttonAdaugareModel.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.E) {
                buttonVizualizareMasini.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.P) {
                buttonPrint.PerformClick();
            }


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            buttonAdaugareMasina.PerformClick();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            buttonAdaugareModel.PerformClick();
        }

        private void transportToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonVizualizareMasini.PerformClick();
        }

        private void rutaToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonSalvareMasini.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonCitireMasini.PerformClick();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) {
            
        }
        private void buttonEmail_Click(object sender, EventArgs e) {
            string file = "Raport Masini #" + random + " " + userName;
            string directory = Environment.CurrentDirectory;
            string filePath = Path.Combine(directory, file + ".pdf");
            PrintDocument doc = new PrintDocument() {
                PrinterSettings = new PrinterSettings() {
                    PrinterName = "Microsoft Print to PDF",
                    PrintToFile = true,
                    PrintFileName = Path.Combine(directory, file + ".pdf"),
                }

            };          
            doc.PrintPage += new PrintPageEventHandler(DVPrintDocument1_PrintPage);
            doc.Print();
            System.Threading.Thread.Sleep(1000);
            sendEmail(filePath);
            
        }

        string MakeImageSrcData(string filename) {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return "data:image/png;base64," +
              Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
        }

        private void sendEmail(string filePath) {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("onlylogistics2020@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Raport Masini #"+random+" "+userName;
            mail.IsBodyHtml = true;
            //string htmlBody = File.ReadAllText("email.html");
            string htmlTemplate = "<!DOCTYPE html> <html> <head> </head> <body style=\"margin: 0; padding: 0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"padding: 10px 0 30px 0;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\"> <tr> <td align=\"center\" bgcolor=\"#70bbd9\" style=\"padding: 40px 0 30px 0; background-color: #153643; font-size: 28px; font-weight: bold; font-family: 'Montserrat' , sans-serif;\"> <img src=\"" + MakeImageSrcData("logo2.0.png") + "\" alt=\"Creating Email Magic\" width=\"200\" height=\"200\" style=\"display: block;\" /> </td> </tr> <tr> <td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 24px;\"> <b>Draga {0}, </b> </td> </tr> <tr> <td style=\"padding: 20px 0 20px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Intrucat ai facut o cerere pentru a genera raportul #{1} pentru masini, il vei gasi atasat mai jos. <p style=\"margin-top:10px\">Pentru orice alte detalii, nu ezista sa ne contactezi</p> </td> </tr> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify\"> <b>Cu drag,<br></b><b>Echipa Only Logistics</b> </td> </tr> </table> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\"> &nbsp; </td> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify;\"> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#ee4c50\" style=\"padding: 30px 30px 30px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\"> &reg; Cezin Cupii, Bucharest 2020<br /> </td> <td align=\"right\" width=\"25%\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("tw.png") + "\" alt=\"Twitter\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("fb.png") + "\" alt=\"Facebook\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>";
            string mailBody = string.Format(htmlTemplate, userName,random);
            //mail.Body = "<body style=\"margin: 0; padding: 0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"padding: 10px 0 30px 0;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\"> <tr> <td align=\"center\" bgcolor=\"#70bbd9\" style=\"padding: 40px 0 30px 0; color: #153643; font-size: 28px; font-weight: bold; font-family: Arial, sans-serif;\"> </td> </tr> <tr> <td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 24px;\"> <b>Lorem ipsum dolor sit amet!</b> </td> </tr> <tr> <td style=\"padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> </table> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\"> &nbsp; </td> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Lorem ipsum dolor sit amet, consectetur adipiscing elit. In tempus adipiscing felis, sit amet blandit ipsum volutpat sed. Morbi porttitor, eget accumsan dictum, nisi libero ultricies ipsum, in posuere mauris neque at erat. </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#ee4c50\" style=\"padding: 30px 30px 30px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\"> &reg; Someone, somewhere 2013<br /> <a href=\"#\" style=\"color: #ffffff;\"> <font color=\"#ffffff\">Unsubscribe</font> </a> to this newsletter instantly </td> <td align=\"right\" width=\"25%\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> </a> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> </a> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> ";

            mail.Body = mailBody;

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(filePath);
            mail.Attachments.Add(attachment);


            //parolafoarteputernica
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("onlylogistics2020@gmail.com", "parolafoarteputernica");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            MsgBox msg = new MsgBox();
            msg.textProperty = "Raportul a fost trimis !";
            msg.Show();
        }

        private void buttonPrint_Click(object sender, EventArgs e) {
            DVPrintPreviewDialog1.Icon = Properties.Resources.logo2_0_Ugm_icon;
            DVPrintPreviewDialog1.Document = DVPrintDocument1;
            DVPrintPreviewDialog1.ShowDialog();
        }

        private void DVPrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            

            Bitmap bmp = Properties.Resources.logo;
            Image logo = bmp;
            e.Graphics.DrawImage(logo, (e.PageBounds.Width-logo.Width) / 2,40, 150, 150);
            e.Graphics.DrawString("User: " + userName, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new Point(50, 125));

            e.Graphics.DrawString("Print Nr: "+random+"/2020", new Font("Century Gothic", 12), Brushes.Black, new Point(50, 75));
            e.Graphics.DrawString("" + DateTime.Now, new Font("Century Gothic", 12), Brushes.Black, new Point(50, 95));
            e.Graphics.DrawString("RAPORT INTERMEDIAR", new Font("Century Gothic", 15,FontStyle.Bold), Brushes.Black, new Point((e.PageBounds.Width-200)/2, 250));
            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, 285));
            e.Graphics.DrawString("ID", new Font("Century Gothic", 12), Brushes.Black, new Point(125, 315));
            e.Graphics.DrawString("Firma", new Font("Century Gothic", 12), Brushes.Black, new Point(325, 315));
            e.Graphics.DrawString("An Fabricatie", new Font("Century Gothic", 12), Brushes.Black, new Point(525, 315));
            e.Graphics.DrawString("Pret", new Font("Century Gothic", 12), Brushes.Black, new Point(725, 315));
            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, 345));
            int y = 370;
            double pretMic = 0;
            double pretMediu = 0;
            double pretMare = 0;
            double pretTotal = 0;
            double count = 0;
            foreach (Masina m in listaMasina) {
 
                    e.Graphics.DrawString(m.Id.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(125, y));
                    e.Graphics.DrawString(m.Firma, new Font("Century Gothic", 12), Brushes.Black, new Point(325, y));
                    e.Graphics.DrawString(m.AnFabricate.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(550, y));
                    e.Graphics.DrawString(m.Pret.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(725, y));
                    y += 30;
                if (m.Pret < 2500) {
                    pretMic++;
                }
                if (m.Pret > 2500 && m.Pret < 5000) {
                    pretMediu++;
                }
                if (m.Pret > 5000) {
                    pretMare++;
                }
                pretTotal += m.Pret;
                count++;
            }
            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, y));
            e.Graphics.DrawString("GRAFIC PRETURI", new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(210, y += 50));

            //GRAFIC
            //#293541 #2B6C79 #47A896
            double calculMic, calculMediu, calculMare, calculMedie;
            calculMic = (pretMic / count) * 360;
            calculMediu = (pretMediu / count) * 360;
            calculMare = (pretMare / count) * 360;
            calculMedie = pretTotal / count;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            const int width = 200;

            Graphics gr = e.Graphics;
            Pen outline_pen = Pens.Red;

            SolidBrush smallBrush = new SolidBrush(Color.FromArgb(255, 41, 53, 65));
            SolidBrush mediumBrush = new SolidBrush(Color.FromArgb(255, 43, 108, 121));
            SolidBrush bigBrush = new SolidBrush(Color.FromArgb(255, 71, 168, 150));

            using (Pen ellipse_pen = new Pen(Color.White)) {
                Rectangle rect =
                    new Rectangle(200, y += 60, width, width);
                gr.DrawEllipse(ellipse_pen, rect);

                gr.FillPie(smallBrush, rect, 0, (float)calculMic);
                gr.FillPie(mediumBrush, rect, (float)calculMic, (float)calculMediu);
                gr.FillPie(bigBrush, rect, (float)calculMediu + (float)calculMic, (float)calculMare);

            }

            e.Graphics.DrawString("\u25C9 0-2500 € ", new Font("Century Gothic", 18), smallBrush, new Point(500, y += 30));
            e.Graphics.DrawString("\u25C9 2500-5000 €", new Font("Century Gothic", 18), mediumBrush, new Point(500, y += 30));
            e.Graphics.DrawString("\u25C9 5000+ €", new Font("Century Gothic", 18), bigBrush, new Point(500, y += 30));
            e.Graphics.DrawString("\u25C9 pret mediu: " + calculMedie + " €", new Font("Century Gothic", 18), Brushes.Black, new Point(500, y += 30));


            //Semantura
            Bitmap bmp2 = Properties.Resources.Semnatura_Cezin;
            Image semnatura = bmp2;
            e.Graphics.DrawString("Cezin Cupii", new Font("Century Gothic", 12), Brushes.Black, new Point(675, e.PageBounds.Height - 100));
            e.Graphics.DrawString("Copyright© Only Logistics RO, Inc. All rights reserved.", new Font("Century Gothic", 12), Brushes.Black, new Point(50, e.PageBounds.Height - 50));
            e.Graphics.DrawImage(semnatura, 675, e.PageBounds.Height - 90, 100, 100);


            
        }

        private void FormMasina_DragDrop(object sender, DragEventArgs e) {
            MsgBox msg = new MsgBox();
            if(e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                StreamReader sr = new StreamReader(files[0]);
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = linie.Split(',');
                        int id = Convert.ToInt32(elements[0]);
                        string firma = elements[1];
                        int anFabricatie = Convert.ToInt32(elements[2]);
                        int pret = Convert.ToInt32(elements[3]);
                        int[] modele = new int[10];
                        for (int i = 0; i < 6; i++) {
                            modele[i] = Convert.ToInt32(elements[i + 4]);
                        }
                        Masina m = new Masina(id, firma, anFabricatie, pret, modele);
                        listaMasina.Add(m);
                    }
                }
                catch (Exception ex) {
                    msg.textProperty = "Fisierul a fost citit cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();

                }

                sr.Close();
            }
        }

        private void FormMasina_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        
    }
}
