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
                MessageBox.Show("Felicitari! Ai adaugat o ruta cu succes!");
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                tbID.Text = "ID";
                tbPlecare.Text = "Localitate Plecare";
                tbSosire.Text = "Localitate Sosire";
                tbNrKilometri.Text = "Nr Kilometri";

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
                    MessageBox.Show("Felicitari! Ai introdus modele cu succes!");
            }
            if (ok == true) {
                MessageBox.Show("Nu s-a gasit ID-ul!");
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
            
            openFileDialog1.Filter = "(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                try {
                    while (sr.ReadLine() != null) {
                        string[] elements = sr.ReadLine().Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string plecare = elements[1];
                        string sosire = elements[1];
                        int nrKm = Convert.ToInt32(elements[2]);
                        string[] opriri = new string[10];
                        for (int i = 0; i < 10; i++) {
                            opriri[i] =elements[i + 4];
                        }
                        Rute r = new Rute(id, plecare, sosire, nrKm, opriri);
                        listaRute.Add(r);
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
        }
    }
}
