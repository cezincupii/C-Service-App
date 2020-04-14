using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
                MessageBox.Show("Felicitari, ai adaugat un transport cu succes!");

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                tbID.Text = "ID";
                tbFirma.Text = "Firma";
                tbDestinatie.Text = "Destinatie";
            }
        }

        private void FormTransport_Load(object sender, EventArgs e) {
            panelAdaugaGreutate.Hide();
            panelAfisareTransporturi.Hide();
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
                if (ok == false)
                    MessageBox.Show("Felicitari! Ai introdus modele cu succes!");
            }
            if (ok == true) {
                MessageBox.Show("Nu s-a gasit ID-ul!");
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
                try {
                    while (sr.ReadLine() != null) {
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
                    MessageBox.Show("Fisier citit cu succes!");
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
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
        }
    }
}
