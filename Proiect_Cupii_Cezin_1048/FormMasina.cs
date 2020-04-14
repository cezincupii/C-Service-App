using System;
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

namespace Proiect_Cupii_Cezin_1048 {
    public partial class FormMasina : Form {

        List<Masina> listaMasina = new List<Masina>();
        public FormMasina() {
            InitializeComponent();
            panelLeft.Hide();
            
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
           
    
            try {
                if (tbID.Text == "") {
                    MessageBox.Show("Introduceti ID-ul", "EROARE!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
          if (tbFirma.Text == "") {
                    MessageBox.Show("Introduceti firma", "EROARE!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                
          if (tbAnFabricatie.Text == "") {
                    MessageBox.Show("Introduceti Anul fabricatiei", "EROARE!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            
          if (tbPret.Text == "") {
                    MessageBox.Show("Introduceti pretul", "EROARE!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                int id = Convert.ToInt32(tbID.Text);
                string firma = tbFirma.Text;
                int anFabricatie = Convert.ToInt32(tbAnFabricatie.Text);
                float pret = (float)Convert.ToInt32(tbPret.Text);
                int[] modele = new int[10];
                for (int i = 0; i < 10; i++)
                    modele[i] = 0;
                
                Masina m = new Masina(id, firma, anFabricatie, pret, modele);
                listaMasina.Add(m);
                MessageBox.Show("Masina adaugata!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                tbID.Text = "ID";
                tbFirma.Text = "Firma";
                tbAnFabricatie.Text = "An Fabricatie";
                tbPret.Text = "Pret";
            }

        }

        private void FormMasina_Load(object sender, EventArgs e) {
            
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
                try {
                    while (sr.ReadLine() != null) {
                        string[] elements = sr.ReadLine().Split(',');

                        int id = Convert.ToInt32(elements[0]);
                        string firma = elements[1];
                        int anFabricatie = Convert.ToInt32(elements[2]);
                        float pret = (float)Convert.ToInt32(elements[3]);
                        int[] modele = new int[10];
                        for (int i = 0; i < 6; i++) {
                            modele[i] = Convert.ToInt32(elements[i + 4]);
                        }
                        Masina m = new Masina(id, firma, anFabricatie, pret, modele);
                        listaMasina.Add(m);
                    }
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message);
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
                    if (ok == false)
                        MessageBox.Show("Felicitari! Ai introdus modele cu succes!");
                }
             if(ok==true) {
                MessageBox.Show("Nu s-a gasit ID-ul!");
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
    }
}
