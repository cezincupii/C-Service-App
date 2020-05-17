using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class FormSofer : Form {
        string connString;

        string userName = null;
        string email = null;
        string password = null;
        int random;
        List<Sofer> listaSofer = new List<Sofer>();
        public FormSofer() {
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
        

        //private void FormSofer_Resize(object sender, EventArgs e) {
        //    int x = (this.Width - panelAdaugaSofer.Width) / 2;
        //    int y = (this.Width - panelAdaugaSofer.Width) / 20;
        //    panelAdaugaSofer.Location = new Point(x, y);
        //}

        private void tbID_Click(object sender, EventArgs e) {
            tbID.Clear();
            tbID.ForeColor = Color.WhiteSmoke;
            panel2.BackColor = Color.FromArgb(104, 142, 38);

            
        }

        private void tbNume_Click(object sender, EventArgs e) {
            tbNume.Clear();
            tbNume.ForeColor = Color.WhiteSmoke;
            panel1.BackColor = Color.FromArgb(104, 142, 38);

            
        }

        private void tbPrenume_Click(object sender, EventArgs e) {
            tbPrenume.Clear();
            tbPrenume.ForeColor = Color.WhiteSmoke;
            panel4.BackColor = Color.FromArgb(104, 142, 38);

           
        }

        private void tbDataNasterii_Click(object sender, EventArgs e) {
            tbDataNasterii.Clear();
            tbDataNasterii.ForeColor = Color.WhiteSmoke;
            panel3.BackColor = Color.FromArgb(104, 142, 38);

            
        }

        private void tbSalariu_Click(object sender, EventArgs e) {
            tbSalariu.Clear();
            tbSalariu.ForeColor = Color.WhiteSmoke;
            panel7.BackColor = Color.FromArgb(104, 142, 38);

           
        }

        private void tbTipCarnet_Click(object sender, EventArgs e) {
            
            tbTipCarnet.Clear();
            tbTipCarnet.ForeColor = Color.WhiteSmoke;
            panel6.BackColor = Color.FromArgb(104, 142, 38);

           
        }

        private void button1_Click(object sender, EventArgs e) {
            
            MsgBox msg = new MsgBox();
            bool valid = true;
            if (tbID.Text == "" || tbID.Text == "ID") {
                msg.textProperty += "Introdu ID-ul";
                valid = false;
            }
            else if (tbNume.Text == "" || tbNume.Text == "Nume") {
                msg.textProperty += "Introdu numele";
                valid = false;
            }
            else if (tbPrenume.Text == "" || tbPrenume.Text == "Prenume") {
                valid = false;
                msg.textProperty += "Introdu prenumele";
            }
            else if (tbDataNasterii.Text == "" || tbDataNasterii.Text == "Data Nasterii") {
                valid = false;
                msg.textProperty += "Introdu data nasterii";
            }
            else if (tbSalariu.Text == "" || tbSalariu.Text == "Salariu") {
                valid = false;
                msg.textProperty += "Introdu salariul";
            }
            else if (tbTipCarnet.Text == "" || tbTipCarnet.Text == "Tip Carnet") {
                valid = false;
                msg.textProperty += "Introdu tipul carnet";
            }
            else if (!Regex.IsMatch(tbNume.Text, @"^[a-zA-Z]+$")) {
                valid = false;
                msg.textProperty += "Numele trebuie sa contina doar litere";
                tbNume.Text = "";
            }
            else if (!Regex.IsMatch(tbPrenume.Text, @"^[a-zA-Z]+$")) {
                valid = false;
                msg.textProperty += "Preumele trebuie sa contina doar litere";
                tbPrenume.Text = "";
            }
            else if (!Regex.IsMatch(tbDataNasterii.Text, "^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$")) {
                valid = false;
                msg.textProperty += "Data nasterii trebuie sa fie in formatul DD/MM/YYYY";
                tbDataNasterii.Text = "";
            }
            else if (!Regex.IsMatch(tbSalariu.Text, "^[0-9]*$")) {
                valid = false;
                msg.textProperty = "Salariul trebuie sa fie alcatuit din cifre";
                tbSalariu.Text = "";
            }
            else if (Convert.ToInt32(tbSalariu.Text) < 0) {
                valid = false;
                msg.textProperty += "Introdu o valoare reala a salariului!";
                tbSalariu.Text = "";
            }
            else if (tbTipCarnet.Text.Length > 1 && (!Regex.IsMatch(tbTipCarnet.Text, @"^[a-zA-Z]+$"))) {
                valid = false;
                msg.textProperty += "Introdu un tip real de carnet!";
                tbTipCarnet.Text = "";
            }
            
            if (valid)
            try {
                int id = Convert.ToInt32(tbID.Text);
                string nume = tbNume.Text;
                string prenume = tbPrenume.Text;
                string dataNasterii = tbDataNasterii.Text;
                int salariu = Convert.ToInt32(tbSalariu.Text);
                string tipCarnet = tbTipCarnet.Text;
                int[] kmLunari = new int[12];
                for(int i=0;i<12;i++) {
                    kmLunari[i] = 0;
                
                }
                Sofer s = new Sofer(id, nume, prenume, dataNasterii, salariu, tipCarnet, kmLunari);
                listaSofer.Add(s);
                    msg.textProperty = "Felicitari! Ai introdus un sofer cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }
                catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                tbID.Text = "ID";
                tbNume.Text = "Nume";
                tbPrenume.Text = "Prenume";
                tbDataNasterii.Text = "Data Nasterii";
                tbSalariu.Text = "Salariu";
                tbTipCarnet.Text = "Tip Carnet";
            }
            else {
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
        }

        private void FormSofer_Load(object sender, EventArgs e) {
            labelDashed.Hide();
            panelAdaugaKM.Hide();
            panelAfisareSoferi.Hide();

        }

        private void textBoxAMID_Click(object sender, EventArgs e) {
            textBoxAMID.Clear();
            textBoxAMID.ForeColor = Color.WhiteSmoke;
            panel10.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void textBoxKM_Click(object sender, EventArgs e) {
            textBoxKM.Clear();
            textBoxKM.ForeColor = Color.WhiteSmoke;
            panel9.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void buttonAdaugareKm_Click(object sender, EventArgs e) {
            panelAfisareSoferi.Hide();
            panelAdaugaSofer.Hide();
            panelAdaugaKM.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareKm.Height;
            panelLeft.Top = buttonAdaugareKm.Top;
        }

        private void buttonAdaugareSofer_Click(object sender, EventArgs e) {
            panelAfisareSoferi.Hide();
            panelAdaugaKM.Hide();
            panelAdaugaSofer.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareSofer.Height;
            panelLeft.Top = buttonAdaugareSofer.Top;

            
        }

        private void buttonAdaugaModel_Click(object sender, EventArgs e) {
            bool ok = true;
            if (ok) {
                foreach (Sofer s in listaSofer) {

                    if (s.Id == Convert.ToInt32(textBoxAMID.Text)) {
                        ok = false;
                        string[] kmS = textBoxKM.Text.Split(',');
                        int[] km = new int[kmS.Length];
                        for (int i = 0; i < kmS.Length; i++)
                            km[i] = Convert.ToInt32(kmS[i]);
                    }

                }
                if (ok == false) {
                    MsgBox msg = new MsgBox();
                    msg.textProperty = "Felicitari! Ai introdus modele cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();

                }
                    
            }
            if (ok == true) {
                MsgBox msg = new MsgBox();
                msg.textProperty = "Nu s-a gasit ID-ul";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
        }

        private void buttonVizualizareSoferi_Click(object sender, EventArgs e) {
            panelAfisareSoferi.Show();
            panelAdaugaKM.Hide();
            panelAdaugaSofer.Hide();
            panelLeft.Show();
            panelLeft.Height = buttonVizualizareSoferi.Height;
            panelLeft.Top = buttonVizualizareSoferi.Top;
            foreach (Sofer s in listaSofer) {
                ListViewItem itm = new ListViewItem(s.Id.ToString());
                itm.SubItems.Add(s.Nume);
                itm.SubItems.Add(s.Prenume);
                itm.SubItems.Add(s.DataNasterii);
                itm.SubItems.Add(s.Salariu.ToString());
                itm.SubItems.Add(s.TipCarnet);
                listView1.Items.Add(itm);

            }
        }

        private void buttonSalvareSoferi_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (Sofer s in listaSofer) {
                    sw.WriteLine(s.ToString());
                }

                sw.Close();

            }
        }

        private void buttonCitireSoferi_Click(object sender, EventArgs e) {
            MsgBox msg = new MsgBox();
            openFileDialog1.Filter = "(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = linie.Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string nume = elements[1];
                        string prenume = elements[2];
                        string dataNasterii = elements[3];
                        int salariu = Convert.ToInt32(elements[4]);
                        string tipCarnet = elements[5];
                        int[] km = new int[12];
                        for (int i = 0; i < 12; i++) {
                            km[i] = Convert.ToInt32(elements[i + 6]);
                        }
                        Sofer s = new Sofer(id, nume, prenume, dataNasterii, salariu, tipCarnet, km);
                        listaSofer.Add(s);
                        msg.textProperty = "Fisierul a fost citit cu succes!";
                        msg.StartPosition = FormStartPosition.CenterScreen;
                        msg.Show();
                    }
                }
                catch (Exception ex) {
                    msg.textProperty = ex.Message;
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }

                sr.Close();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            buttonAdaugareSofer.PerformClick();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            buttonAdaugareKm.PerformClick();
        }

        private void transportToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonVizualizareSoferi.PerformClick();
        }

        private void rutaToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonSalvareSoferi.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonCitireSoferi.PerformClick();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void buttonAdaugareSofer_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga un sofer(CTRL+N)", buttonAdaugareSofer);
        }

        private void buttonAdaugareKm_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga KM(CTRL+H)", buttonAdaugareKm);

        }

        private void buttonVizualizareSoferi_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Vizualizare soferi(CTRL+E)", buttonVizualizareSoferi);

        }

        private void buttonSalvareSoferi_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Salveaza soferii(CTRL+S)", buttonSalvareSoferi);

        }

        private void buttonCitireSoferi_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Incarca soferii(CTRL+I)", buttonCitireSoferi);

        }
        private void buttonPrint_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Printeaza datele(CTRL+P)", buttonPrint);

        }

        private void FormSofer_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.S) {
                buttonSalvareSoferi.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.I) {
                buttonCitireSoferi.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.N) {
                buttonAdaugareSofer.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.H) {
                buttonAdaugareKm.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.E) {
                buttonVizualizareSoferi.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.P) {
                buttonPrint.PerformClick();
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e) {
            DVPrintPreviewDialog1.Document = DVPrintDocument1;
            DVPrintPreviewDialog1.ShowDialog();
        }

        private void DVPrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            
            OleDbConnection conexiune = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Users.accdb ");
            conexiune.Open();
            OleDbCommand comanda = new OleDbCommand("SELECT username from users where isLogged=1");
            comanda.Connection = conexiune;
            OleDbDataReader reader = comanda.ExecuteReader();
            while (reader.Read()) {
                userName = reader["username"].ToString();
                e.Graphics.DrawString("User: " + userName, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Black, new Point(50, 125));

            }
            reader.Close();
            conexiune.Close();

            Bitmap bmp = Properties.Resources.logo;
            Image logo = bmp;
            Random rnd = new Random();
            e.Graphics.DrawImage(logo, (e.PageBounds.Width - logo.Width) / 2, 40, 150, 150);

            e.Graphics.DrawString("Print Nr: " + rnd.Next(1, 100) + "/2020", new Font("Century Gothic", 12), Brushes.Black, new Point(50, 75));
            e.Graphics.DrawString("" + DateTime.Now, new Font("Century Gothic", 12), Brushes.Black, new Point(50, 95));
            e.Graphics.DrawString("RAPORT INTERMEDIAR", new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Black, new Point((e.PageBounds.Width - 200) / 2, 250));
            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, 285));
            e.Graphics.DrawString("ID", new Font("Century Gothic", 12), Brushes.Black, new Point(125, 315));
            e.Graphics.DrawString("Nume", new Font("Century Gothic", 12), Brushes.Black, new Point(225, 315));
            e.Graphics.DrawString("Prenume", new Font("Century Gothic", 12), Brushes.Black, new Point(325, 315));
            e.Graphics.DrawString("Data Nasterii", new Font("Century Gothic", 12), Brushes.Black, new Point(425, 315));
            e.Graphics.DrawString("Salariu", new Font("Century Gothic", 12), Brushes.Black, new Point(575, 315));
            e.Graphics.DrawString("Tip Carnet", new Font("Century Gothic", 12), Brushes.Black, new Point(675, 315));
            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, 345));
            int y = 370;
            double salariuMic=0;
            double salariuMediu = 0;
            double salariuMare=0;
            double salariuTotal = 0;
            double count = 0;
            foreach (Sofer s in listaSofer) {
                e.Graphics.DrawString(s.Id.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(125, y));
                e.Graphics.DrawString(s.Nume, new Font("Century Gothic", 12), Brushes.Black, new Point(225, y));
                e.Graphics.DrawString(s.Prenume, new Font("Century Gothic", 12), Brushes.Black, new Point(325, y));
                e.Graphics.DrawString(s.DataNasterii, new Font("Century Gothic", 12), Brushes.Black, new Point(425, y));
                e.Graphics.DrawString(s.Salariu.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(575, y));
                e.Graphics.DrawString(s.TipCarnet, new Font("Century Gothic", 12), Brushes.Black, new Point(725, y));
                y += 30;
                //MessageBox.Show(s.Salariu.ToString());
                if (s.Salariu < 2500) {
                    salariuMic++;
                }
                if (s.Salariu > 2500 && s.Salariu < 5000) {
                    salariuMediu++;
                }
                if (s.Salariu > 5000) {
                    salariuMare++;
                }
                salariuTotal += s.Salariu;
                count++;
            }
            
            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, y));
            e.Graphics.DrawString("GRAFIC SALARII", new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(210, y+=50));

            //GRAFIC
            //#293541 #2B6C79 #47A896
            double calculMic,calculMediu,calculMare,calculMedie;
            calculMic = (salariuMic / count)*360;
            calculMediu = (salariuMediu / count)*360;
            calculMare = (salariuMare / count) * 360;
            calculMedie = salariuTotal / count;
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
                gr.FillPie(bigBrush, rect, (float)calculMediu+(float)calculMic, (float)calculMare);

            }

            e.Graphics.DrawString("\u25C9 0-2500 lei", new Font("Century Gothic", 18), smallBrush, new Point(500, y+=30 ));
            e.Graphics.DrawString("\u25C9 2500-5000 lei", new Font("Century Gothic", 18), mediumBrush, new Point(500, y+=30 ));
            e.Graphics.DrawString("\u25C9 5000+ lei", new Font("Century Gothic", 18), bigBrush, new Point(500, y+=30 ));
            e.Graphics.DrawString("\u25C9 Salariu mediu: " + calculMedie+" lei", new Font("Century Gothic", 18), Brushes.Black, new Point(500, y+=30 ));


            //Semantura
            Bitmap bmp2 = Properties.Resources.Semnatura_Cezin;
            Image semnatura = bmp2;
            e.Graphics.DrawString("Cezin Cupii", new Font("Century Gothic", 12), Brushes.Black, new Point(675, e.PageBounds.Height-100));
            e.Graphics.DrawString("Copyright© Only Logistics RO, Inc. All rights reserved.", new Font("Century Gothic", 12), Brushes.Black, new Point(50, e.PageBounds.Height-50));
            e.Graphics.DrawImage(semnatura, 675, e.PageBounds.Height-90, 100, 100);

        }

        private void FormSofer_DragDrop(object sender, DragEventArgs e) {
            MsgBox msg = new MsgBox();
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                StreamReader sr = new StreamReader(files[0]);
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = linie.Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string nume = elements[1];
                        string prenume = elements[2];
                        string dataNasterii = elements[3];
                        int salariu = Convert.ToInt32(elements[4]);
                        string tipCarnet = elements[5];
                        int[] km = new int[12];
                        for (int i = 0; i < 12; i++) {
                            km[i] = Convert.ToInt32(elements[i + 6]);
                        }
                        Sofer s = new Sofer(id, nume, prenume, dataNasterii, salariu, tipCarnet, km);
                        listaSofer.Add(s);
                        msg.textProperty = "Fisierul a fost citit cu succes!";
                        msg.StartPosition = FormStartPosition.CenterScreen;
                        msg.Show();
                    }
                }
                catch (Exception ex) {
                    msg.textProperty = ex.Message;
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }

                sr.Close();

            }
        }

        private void FormSofer_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void buttonEmail_Click(object sender, EventArgs e) {
            string file = "Raport Soferi #" + random + " " + userName;
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
            mail.Subject = "Raport Sofer #" + random + " " + userName;
            mail.IsBodyHtml = true;
            //string htmlBody = File.ReadAllText("email.html");
            string htmlTemplate = "<!DOCTYPE html> <html> <head> </head> <body style=\"margin: 0; padding: 0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"padding: 10px 0 30px 0;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\"> <tr> <td align=\"center\" bgcolor=\"#70bbd9\" style=\"padding: 40px 0 30px 0; background-color: #153643; font-size: 28px; font-weight: bold; font-family: 'Montserrat' , sans-serif;\"> <img src=\"" + MakeImageSrcData("logo2.0.png") + "\" alt=\"Creating Email Magic\" width=\"200\" height=\"200\" style=\"display: block;\" /> </td> </tr> <tr> <td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 24px;\"> <b>Draga {0}, </b> </td> </tr> <tr> <td style=\"padding: 20px 0 20px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;\"> Intrucat ai facut o cerere pentru a genera raportul #{1} pentru soferi, il vei gasi atasat mai jos. <p style=\"margin-top:10px\">Pentru orice alte detalii, nu ezista sa ne contactezi</p> </td> </tr> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify\"> <b>Cu drag,<br></b><b>Echipa Only Logistics</b> </td> </tr> </table> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\"> &nbsp; </td> <td width=\"260\" valign=\"top\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td> </td> </tr> <tr> <td style=\"padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px; text-align: justify;\"> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td bgcolor=\"#ee4c50\" style=\"padding: 30px 30px 30px 30px;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr> <td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 14px;\" width=\"75%\"> &reg; Cezin Cupii, Bucharest 2020<br /> </td> <td align=\"right\" width=\"25%\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tr> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("tw.png") + "\" alt=\"Twitter\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> <td style=\"font-size: 0; line-height: 0;\" width=\"20\">&nbsp;</td> <td style=\"font-family: Arial, sans-serif; font-size: 12px; font-weight: bold;\"> <a href=\"http://www.twitter.com/\" style=\"color: #ffffff;\"> <img src=\"" + MakeImageSrcData("fb.png") + "\" alt=\"Facebook\" width=\"38\" height=\"38\" style=\"display: block;\" border=\"0\" /> </a> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </body> </html>";
            string mailBody = string.Format(htmlTemplate, userName, random);
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
    }
}
