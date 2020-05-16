using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class FormRuta : Form {

        List<Rute> listaRute = new List<Rute>();

        public FormRuta() {
            InitializeComponent();
            panelLeft.Hide();
        }

        //private void FormRuta_Resize(object sender, EventArgs e) {
        //    int x = (this.Width - panelAdaugaRuta.Width) / 2;
        //    int y = (this.Width - panelAdaugaRuta.Width) / 14;
        //    panelAdaugaRuta.Location = new Point(x, y);
        //}

        private void tbID_Click(object sender, EventArgs e) {
            tbID.Clear();
            tbID.ForeColor = Color.WhiteSmoke;
            panel2.BackColor = Color.FromArgb(104, 142, 38);

            
        }

        private void tbPlecare_Click(object sender, EventArgs e) {
            tbPlecare.Clear();
            tbPlecare.ForeColor = Color.WhiteSmoke;
            panel1.BackColor = Color.FromArgb(104, 142, 38);

           
        }

        private void tbSosire_Click(object sender, EventArgs e) {
            tbSosire.Clear();
            tbSosire.ForeColor = Color.WhiteSmoke;
            panel4.BackColor = Color.FromArgb(104, 142, 38);

           
        }

        private void tbNrKilometri_Click(object sender, EventArgs e) {
            tbNrKilometri.Clear();
            tbNrKilometri.ForeColor = Color.WhiteSmoke;
            panel3.BackColor = Color.FromArgb(104, 142, 38);

            
        }

        private void button1_Click(object sender, EventArgs e) {
            MsgBox msg = new MsgBox();
            bool valid = true;
            if (tbID.Text == "" || tbID.Text == "ID") {
                msg.textProperty += "Introdu ID-ul";
                valid = false;
            }
            else if (tbPlecare.Text == "" || tbPlecare.Text == "Localitate Plecare") {
                msg.textProperty += "Introdu Localitatea de plecare";
                valid = false;
            }
            else if (tbSosire.Text == "" || tbSosire.Text == "Localitate Sosire") {
                msg.textProperty += "Introdu Localitatea de sosire";
                valid = false;
            }
            else if (tbNrKilometri.Text=="" || tbNrKilometri.Text== "Nr Kilometri") {
                msg.textProperty = "Introdu km";
                valid = false;
            }
            else if (!Regex.IsMatch(tbPlecare.Text, @"^[a-zA-Z]+$")) {
                valid = false;
                msg.textProperty += "Localitatea de plecare trebuie sa contina doar litere";
                tbPlecare.Text = "";
            }
            else if (!Regex.IsMatch(tbSosire.Text, @"^[a-zA-Z]+$")) {
                valid = false;
                msg.textProperty += "Localitatea de sosire trebuie sa contina doar litere";
                tbSosire.Text = "";
            }
            else if (!Regex.IsMatch(tbNrKilometri.Text, "^[0-9]*$")) {
                valid = false;
                msg.textProperty = "Salariul trebuie sa fie alcatuit din cifre";
                tbNrKilometri.Text = "";
            }
            if (valid) 
            try {
                int id = Convert.ToInt32(tbID.Text);
                string plecare = tbPlecare.Text;
                string sosire = tbSosire.Text;
                int nrKilometri = Convert.ToInt32(tbNrKilometri.Text);
                string[] opriri = new string[10];
                for(int i=0;i<10;i++) {
                    opriri[i] = "";
                }
                Rute r = new Rute(id, plecare, sosire, nrKilometri, opriri);
                listaRute.Add(r);
                    msg.textProperty = "Felicitari! Ai adaugat o ruta cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }
            catch(Exception ex) {
                    msg.textProperty = ex.Message;
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                   
            }
            finally {
                tbID.Text = "ID";
                tbPlecare.Text = "Localitate Plecare";
                tbSosire.Text = "Localitate Sosire";
                tbNrKilometri.Text = "Nr Kilometri";

            }
            else {
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
        }

        private void FormRuta_Load(object sender, EventArgs e) {
            panelAdaugaOprire.Hide();
            panelAfisareRute.Hide();
        }

        private void buttonAdaugareRuta_Click(object sender, EventArgs e) {
            panelAdaugaOprire.Hide();
            panelAfisareRute.Hide();
            panelAdaugaRuta.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareRuta.Height;
            panelLeft.Top = buttonAdaugareRuta.Top;
        }

        private void buttonAdaugareOprire_Click(object sender, EventArgs e) {
            panelAdaugaRuta.Hide();
            panelAfisareRute.Hide();
            panelAdaugaOprire.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareOprire.Height;
            panelLeft.Top = buttonAdaugareOprire.Top;
        }

        private void buttonAdaugaOprire_Click(object sender, EventArgs e) {
            bool ok = true;
            MsgBox msg = new MsgBox();
            if (ok) {
                foreach (Rute r in listaRute) {

                    if (r.IdRuta == Convert.ToInt32(textBoxAMID.Text)) {
                        ok = false;
                        string[] opririS = textBoxOpriri.Text.Split(',');
                        string[] opriri = new string[opririS.Length];
                        for (int i = 0; i < opririS.Length; i++)
                            opriri[i] = opririS[i];
                    }

                }
                if (ok == false)
                    msg.textProperty = "Succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
            }
            if (ok == true) {
                msg.textProperty = "Nu s-a gasit ID-ul";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
        }

        private void textBoxAMID_Click(object sender, EventArgs e) {
            textBoxAMID.Clear();
            textBoxAMID.ForeColor = Color.WhiteSmoke;
            panel10.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void textBoxOpriri_Click(object sender, EventArgs e) {
            textBoxOpriri.Clear();
            textBoxOpriri.ForeColor = Color.WhiteSmoke;
            panel9.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void buttonVizualizareMasini_Click(object sender, EventArgs e) {
            panelAdaugaRuta.Hide();
            panelAfisareRute.Show();
            panelAdaugaOprire.Hide();
            panelLeft.Show();
            panelLeft.Height = buttonVizualizareMasini.Height;
            panelLeft.Top = buttonVizualizareMasini.Top;
            foreach(Rute r in listaRute) {
                ListViewItem itm = new ListViewItem(r.IdRuta.ToString());
                itm.SubItems.Add(r.LocalitatePlecare);
                itm.SubItems.Add(r.LocalitateSosire);
                itm.SubItems.Add(r.NrKilometri.ToString());
                listView1.Items.Add(itm);
            }
        }

        private void buttonSalvareMasini_Click(object sender, EventArgs e) {
          
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (Rute r in listaRute) {
                    sw.WriteLine(r.ToString());
                }

                sw.Close();
            }
        }

        private void buttonCitireMasini_Click(object sender, EventArgs e) {
            MsgBox msg = new MsgBox();
            openFileDialog1.Filter = "(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = linie.Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string plecare = elements[1];
                        string sosire = elements[2];
                        int nrKm = Convert.ToInt32(elements[3]);
                        string[] opriri = new string[10];
                        for (int i = 0; i < 10; i++) {
                            opriri[i] =elements[i + 4];
                        }
                        Rute r = new Rute(id, plecare, sosire, nrKm, opriri);
                        listaRute.Add(r);
                    }
                    msg.textProperty = "Fisier citit cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
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
            buttonAdaugareRuta.PerformClick();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            buttonAdaugaOprire.PerformClick();
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

        private void buttonAdaugareRuta_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga o ruta(CTRL+N)", buttonAdaugareRuta);

        }

        private void buttonAdaugareOprire_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga oprire(CTRL+H)", buttonAdaugareOprire);

        }

        private void buttonVizualizareMasini_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Vizualizare rute(CTRL+E)", buttonVizualizareMasini);

        }

        private void buttonSalvareMasini_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Salveaza rutele(CTRL+S)", buttonSalvareMasini);

        }

        private void buttonCitireMasini_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Incarca rutele(CTRL+I)", buttonCitireMasini);

        }
        private void buttonPrint_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Printeaza datele(CTRL+P)", buttonPrint);
        }

        private void FormRuta_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.S) {
                buttonSalvareMasini.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.I) {
                buttonCitireMasini.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.N) {
                buttonAdaugareRuta.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.H) {
                buttonAdaugareOprire.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.E) {
                buttonVizualizareMasini.PerformClick();
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
            string userName;
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
            e.Graphics.DrawString("Plecare", new Font("Century Gothic", 12), Brushes.Black, new Point(275, 315));
            e.Graphics.DrawString("Sosire", new Font("Century Gothic", 12), Brushes.Black, new Point(500, 315));
            e.Graphics.DrawString("Nr. Km", new Font("Century Gothic", 12), Brushes.Black, new Point(700, 315));

            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, 345));
            int y = 370;
            double kmMic = 0;
            double kmMediu = 0;
            double kmMare = 0;
            double kmTotal = 0;
            double count = 0;
            foreach (Rute r in listaRute) {
                e.Graphics.DrawString(r.IdRuta.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(125, y));
                e.Graphics.DrawString(r.LocalitatePlecare, new Font("Century Gothic", 12), Brushes.Black, new Point(275, y));
                e.Graphics.DrawString(r.LocalitateSosire, new Font("Century Gothic", 12), Brushes.Black, new Point(500, y));
                e.Graphics.DrawString(r.NrKilometri.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(700, y));
                
                y += 30;
                //MessageBox.Show(s.km.ToString());
                if (r.NrKilometri < 250) {
                    kmMic++;
                }
                if (r.NrKilometri > 250 && r.NrKilometri < 500) {
                    kmMediu++;
                }
                if (r.NrKilometri > 500) {
                    kmMare++;
                }
                kmTotal += r.NrKilometri;
                count++;
            }

            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, y));
            e.Graphics.DrawString("GRAFIC SALARII", new Font("Century Gothic", 18, FontStyle.Bold), Brushes.Black, new Point(210, y += 50));

            //GRAFIC
            //#293541 #2B6C79 #47A896
            double calculMic, calculMediu, calculMare, calculMedie;
            calculMic = (kmMic / count) * 360;
            calculMediu = (kmMediu / count) * 360;
            calculMare = (kmMare / count) * 360;
            calculMedie = kmTotal / count;
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

            e.Graphics.DrawString("\u25C9 0-250 km", new Font("Century Gothic", 18), smallBrush, new Point(500, y += 30));
            e.Graphics.DrawString("\u25C9 250-500 km", new Font("Century Gothic", 18), mediumBrush, new Point(500, y += 30));
            e.Graphics.DrawString("\u25C9 500+ km", new Font("Century Gothic", 18), bigBrush, new Point(500, y += 30));
            e.Graphics.DrawString("\u25C9 Nr mediu KM: " + calculMedie, new Font("Century Gothic", 18), Brushes.Black, new Point(500, y += 30));


            //Semantura
            Bitmap bmp2 = Properties.Resources.Semnatura_Cezin;
            Image semnatura = bmp2;
            e.Graphics.DrawString("Copyright© Only Logistics RO, Inc. All rights reserved.", new Font("Century Gothic", 12), Brushes.Black, new Point(50, e.PageBounds.Height-50));
            e.Graphics.DrawString("Cezin Cupii", new Font("Century Gothic", 12), Brushes.Black, new Point(675, e.PageBounds.Height - 100));
            e.Graphics.DrawImage(semnatura, 675, e.PageBounds.Height - 90, 100, 100);

        }

        private void FormRuta_DragDrop(object sender, DragEventArgs e) {
            MsgBox msg = new MsgBox();
            if(e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                StreamReader sr = new StreamReader(files[0]);
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = linie.Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string plecare = elements[1];
                        string sosire = elements[2];
                        int nrKm = Convert.ToInt32(elements[3]);
                        string[] opriri = new string[10];
                        for (int i = 0; i < 10; i++) {
                            opriri[i] = elements[i + 4];
                        }
                        Rute r = new Rute(id, plecare, sosire, nrKm, opriri);
                        listaRute.Add(r);
                    }
                    msg.textProperty = "Fisier citit cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }
                catch (Exception ex) {
                    msg.textProperty = ex.Message;
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }

                sr.Close();
            }
        }

        private void FormRuta_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        
    }
}
