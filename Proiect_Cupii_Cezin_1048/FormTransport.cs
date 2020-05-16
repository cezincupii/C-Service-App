using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_Cupii_Cezin_1048 {
    public partial class FormTransport : Form {
        List<Transporturi> listaTransport = new List<Transporturi>();
        public FormTransport() {
            InitializeComponent();
            panelLeft.Hide();
        }

        //private void FormTransport_Resize(object sender, EventArgs e) {
        //    int x = (this.Width - panelAdaugaTransport.Width) / 2;
        //    int y = (this.Width - panelAdaugaTransport.Width) / 14;
        //    panelAdaugaTransport.Location = new Point(x, y);
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

        private void tbDestinatie_Click(object sender, EventArgs e) {
            tbDestinatie.Clear();
            tbDestinatie.ForeColor = Color.WhiteSmoke;
            panel4.BackColor = Color.FromArgb(104, 142, 38);

           
        }

        private void button1_Click(object sender, EventArgs e) {
            MsgBox msg = new MsgBox();
            bool valid = true;
            if (tbID.Text == "" || tbID.Text == "ID") {
                msg.textProperty += "Introdu ID-ul";
                valid = false;
            }
            else if (tbDestinatie.Text == "" || tbDestinatie.Text == "Destinatie") {
                msg.textProperty += "Introdu Destinatia";
                valid = false;
            }
            else if (tbFirma.Text == "" || tbFirma.Text == "Firma") {
                msg.textProperty += "Introdu Firma";
                valid = false;
            }
            else if (!Regex.IsMatch(tbFirma.Text, @"^[a-zA-Z]+$")) {
                valid = false;
                msg.textProperty += "Firma trebuie sa contina doar litere";
                tbFirma.Text = "";
            }
            else if (!Regex.IsMatch(tbDestinatie.Text, @"^[a-zA-Z]+$")) {
                valid = false;
                msg.textProperty += "Destinatia trebuie sa contina doar litere";
                tbDestinatie.Text = "";
            }
            if(valid)
            try {
                int id = Convert.ToInt32(tbID.Text);
                string firma = tbFirma.Text;
                string destinatie = tbDestinatie.Text;
                int[] greutateTransport = new int[30];
                for(int i=0;i<30;i++) {
                    greutateTransport[i] = 0;
                }
                Transporturi t = new Transporturi(id, firma, destinatie, greutateTransport);
                listaTransport.Add(t);
                msg.textProperty = "Felicitari! Ai introdus un transport cu succes!";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();

            }
            catch (Exception ex) {
                    msg.textProperty = "EROARE! Ia legatura cu administratorul aplicatiei";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }
            finally {
                tbID.Text = "ID";
                tbFirma.Text = "Firma";
                tbDestinatie.Text = "Destinatie";
            }
            else {
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
        }

        private void FormTransport_Load(object sender, EventArgs e) {
            panelAdaugaGreutate.Hide();
            panelAfisareTransporturi.Hide();
            labelDashed.Hide();
            textBoxGreutati.Text = "Greutate";
        }

        private void buttonAdaugareTransport_Click(object sender, EventArgs e) {
            panelAfisareTransporturi.Hide();
            panelAdaugaGreutate.Hide();
            panelAdaugaTransport.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareTransport.Height;
            panelLeft.Top = buttonAdaugareTransport.Top;
        }

        private void buttonAdaugareGreutate_Click(object sender, EventArgs e) {
            panelAfisareTransporturi.Hide();
            panelAdaugaTransport.Hide();
            panelAdaugaGreutate.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareGreutate.Height;
            panelLeft.Top = buttonAdaugareGreutate.Top;

        }

        private void textBoxAMID_Click(object sender, EventArgs e) {
            textBoxAMID.Clear();
            textBoxAMID.ForeColor = Color.WhiteSmoke;
            panel10.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void textBoxGreutati_Click(object sender, EventArgs e) {
            textBoxGreutati.Clear();
            textBoxGreutati.ForeColor = Color.WhiteSmoke;
            panel9.BackColor = Color.FromArgb(104, 142, 38);
        }

        private void buttonAdaugaGreutate_Click(object sender, EventArgs e) {
            bool ok = true;
            MsgBox msg = new MsgBox();
            if (ok) {
                foreach (Transporturi t in listaTransport) {
                    if (t.IdTransport == Convert.ToInt32(textBoxAMID.Text)) {
                        ok = false;
                        string[] greutatiS = textBoxGreutati.Text.Split(',');
                        int[] modele = new int[greutatiS.Length];
                        for (int i = 0; i < greutatiS.Length; i++)
                            modele[i] = Convert.ToInt32(greutatiS[i]);
                    }

                }
                if (ok == false) {
                    msg.textProperty = "Felicitari! Ai introdus greutate cu succes!";
                    msg.StartPosition = FormStartPosition.CenterScreen;
                    msg.Show();
                }
                    
            }
            if (ok == true) {

                msg.textProperty = "Felicitari! Ai introdus greutate cu succes!";
                msg.StartPosition = FormStartPosition.CenterScreen;
                msg.Show();
            }
        }

        private void buttonVizualizareTransporturi_Click(object sender, EventArgs e) {
            panelLeft.Show();
            panelLeft.Height = buttonVizualizareTransporturi.Height;
            panelLeft.Top = buttonVizualizareTransporturi.Top;
            panelAdaugaTransport.Hide();
            panelAdaugaGreutate.Hide();
            panelAfisareTransporturi.Show();
            foreach (Transporturi t in listaTransport) {
                ListViewItem itm = new ListViewItem(t.IdTransport.ToString());
                itm.SubItems.Add(t.NumeFirma);
                itm.SubItems.Add(t.Destinatie);
                listView1.Items.Add(itm);
            }
        }

        private void buttonSalvareTransporturi_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (Transporturi t in listaTransport) {
                    sw.WriteLine(t.ToString());
                }

                sw.Close();

            }
        }

        private void buttonCitireTransporturi_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = sr.ReadLine().Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string firma = elements[1];
                        string destinatie = elements[2];
                        
                        int[] greutate = new int[10];
                        for (int i = 0; i < 10; i++) {
                            greutate[i] = Convert.ToInt32(elements[i + 3]);
                        }
                        Transporturi t = new Transporturi(id, firma, destinatie, greutate);
                        listaTransport.Add(t);
                        
                    }
                    MsgBox msg = new MsgBox {
                        textProperty = "Fisier citit cu succes!",
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    msg.Show();
                }
                catch (Exception ex) {
                    MsgBox msg = new MsgBox {
                        textProperty = ex.Message,
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    msg.Show();
                }

                sr.Close();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            buttonAdaugareTransport.PerformClick();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            buttonAdaugareGreutate.PerformClick();
        }

        private void transportToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonVizualizareTransporturi.PerformClick();
        }

        private void rutaToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonSalvareTransporturi.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonCitireTransporturi.PerformClick();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void buttonAdaugareTransport_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga un transport(CTRL+N)", buttonAdaugareTransport);

        }

        private void buttonAdaugareGreutate_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Adauga Greutate(CTRL+H)", buttonAdaugareGreutate);

        }

        private void buttonVizualizareTransporturi_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Vizualizare transporturi(CTRL+E)", buttonVizualizareTransporturi);

        }

        private void buttonSalvareTransporturi_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Salveaza transporturile(CTRL+S)", buttonSalvareTransporturi);

        }

        private void buttonCitireTransporturi_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Incarca transporturile(CTRL+I)", buttonCitireTransporturi);

        }
        private void buttonPrint_MouseHover(object sender, EventArgs e) {
            toolTip1.Show("Salveaza datele(CTRL+P)", buttonPrint);

        }

        private void FormTransport_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control == true && e.KeyCode == Keys.S) {
                buttonSalvareTransporturi.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.I) {
                buttonCitireTransporturi.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.N) {
                buttonAdaugareTransport.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.H) {
                buttonAdaugareGreutate.PerformClick();
            }
            if (e.Control == true && e.KeyCode == Keys.E) {
                buttonVizualizareTransporturi.PerformClick();
            }
            if(e.Control==true && e.KeyCode==Keys.P) {
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
            e.Graphics.DrawString("Firma", new Font("Century Gothic", 12), Brushes.Black, new Point(400, 315));
            e.Graphics.DrawString("Destinatie", new Font("Century Gothic", 12), Brushes.Black, new Point(675, 315));
            e.Graphics.DrawString(labelDashed.Text, new Font("Century Gothic", 12), Brushes.Black, new Point(95, 345));
            int y = 370;

            foreach (Transporturi t in listaTransport) {
                e.Graphics.DrawString(t.IdTransport.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(125, y));
                e.Graphics.DrawString(t.NumeFirma.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(400, y));
                e.Graphics.DrawString(t.Destinatie.ToString(), new Font("Century Gothic", 12), Brushes.Black, new Point(675, y));

                y += 30;

            }
            //Semantura
            Bitmap bmp2 = Properties.Resources.Semnatura_Cezin;
            Image semnatura = bmp2;
            e.Graphics.DrawString("Cezin Cupii", new Font("Century Gothic", 12), Brushes.Black, new Point(675, e.PageBounds.Height - 100));
            e.Graphics.DrawString("Copyright© Only Logistics RO, Inc. All rights reserved.", new Font("Century Gothic", 12), Brushes.Black, new Point(50, e.PageBounds.Height-50));
            e.Graphics.DrawImage(semnatura, 675, e.PageBounds.Height - 90, 100, 100);
        }

        private void FormTransport_DragDrop(object sender, DragEventArgs e) {
            MsgBox msg = new MsgBox();
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                StreamReader sr = new StreamReader(files[0]);
                string linie = null;
                try {
                    while ((linie = sr.ReadLine()) != null) {
                        string[] elements = sr.ReadLine().Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string firma = elements[1];
                        string destinatie = elements[2];

                        int[] greutate = new int[10];
                        for (int i = 0; i < 10; i++) {
                            greutate[i] = Convert.ToInt32(elements[i + 3]);
                        }
                        Transporturi t = new Transporturi(id, firma, destinatie, greutate);
                        listaTransport.Add(t);

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

        private void FormTransport_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        
    }
}
