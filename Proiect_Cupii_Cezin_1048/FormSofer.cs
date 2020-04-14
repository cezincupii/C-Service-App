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
    public partial class FormSofer : Form {
        public FormSofer() {
            InitializeComponent();
            panelLeft.Hide();
        }

        List<Sofer> listaSofer = new List<Sofer>();

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
                MessageBox.Show(s.ToString());
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                tbID.Text = "ID";
                tbNume.Text = "Nume";
                tbPrenume.Text = "Prenume";
                tbDataNasterii.Text = "Data Nasterii";
                tbSalariu.Text = "Salariu:";
                tbTipCarnet.Text = "Tip Carnet";
            }
        }

        private void FormSofer_Load(object sender, EventArgs e) {
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
                if (ok == false)
                    MessageBox.Show("Felicitari! Ai introdus modele cu succes!");
            }
            if (ok == true) {
                MessageBox.Show("Nu s-a gasit ID-ul!");
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
            openFileDialog1.Filter = "(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                try {
                    while (sr.ReadLine() != null) {
                        string[] elements = sr.ReadLine().Split(',');

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
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
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
        }
    }
}
