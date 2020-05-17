using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Proiect_Cupii_Cezin_1048
{
    public partial class Form1 : Form
    {
        Thread th;
        
        
        FormMasina frmMasina = new FormMasina() { Dock = DockStyle.Fill, TopLevel = false };
        FormSofer frmSofer = new FormSofer() { Dock = DockStyle.Fill, TopLevel = false };
        FormTransport frmTransport = new FormTransport() { Dock = DockStyle.Fill, TopLevel = false };
        FormRuta frmRuta = new FormRuta() { Dock = DockStyle.Fill, TopLevel = false };
        HomePage homepage = new HomePage() { Dock = DockStyle.Fill, TopLevel = false };

        public Form1()
        {
            
            
            InitializeComponent();
            panelLeft.Height = buttonAdaugareSofer.Height;
            panelLeft.Top = buttonAdaugareSofer.Top;

            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.panelLeft.BringToFront();
            //this.WindowState = FormWindowState.Maximized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]


        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Form1_Load(object sender, EventArgs e) {
            panelLeft.Hide();
            this.panelDesktop.Controls.Add(homepage);
            homepage.Show();
        }

       

        private void panel2_Paint(object sender, PaintEventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {
            frmRuta.Hide();
            frmMasina.Hide();
            frmSofer.Hide();
            frmTransport.Hide();
            this.panelDesktop.Controls.Add(homepage);
            homepage.Show();
        }

       

        private void buttonAdaugareMasina_Click(object sender, EventArgs e) {
            homepage.Hide();
            frmSofer.Hide();
            frmTransport.Hide();
            frmRuta.Hide();
            this.panelDesktop.Controls.Add(frmMasina);
            frmMasina.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareMasina.Height;
            panelLeft.Top = buttonAdaugareMasina.Top;
            
            

        }
        private void button2_Click(object sender, EventArgs e) {
            homepage.Hide();
            frmMasina.Hide();
            frmTransport.Hide();
            frmRuta.Hide();
            this.panelDesktop.Controls.Add(frmSofer);
            frmSofer.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareSofer.Height;
            panelLeft.Top = buttonAdaugareSofer.Top;
           
        }

        private void buttonAdaugareTransport_Click(object sender, EventArgs e) {
            homepage.Hide();
            frmMasina.Hide();
            frmSofer.Hide();
            frmRuta.Hide();
            this.panelDesktop.Controls.Add(frmTransport);
            frmTransport.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareTransport.Height;
            panelLeft.Top = buttonAdaugareTransport.Top;
          


        }

        private void buttonAdaugareRuta_Click(object sender, EventArgs e) {
            homepage.Hide();
            frmMasina.Hide();
            frmSofer.Hide();
            frmTransport.Hide();
            this.panelDesktop.Controls.Add(frmRuta);
            frmRuta.Show();
            panelLeft.Show();
            panelLeft.Height = buttonAdaugareRuta.Height;
            panelLeft.Top = buttonAdaugareRuta.Top;
            

        }
        private void buttonAfisare_Click(object sender, EventArgs e) {
            homepage.Hide();
            frmMasina.Hide();
            frmSofer.Hide();
            frmTransport.Hide();
            frmRuta.Hide();
            //this.panelDesktop.Controls.Add(afisaremasini);
            //afisaremasini.Show();

        }
        private void panel2_Click(object sender, EventArgs e) {
            frmRuta.Hide();
            frmMasina.Hide();
            frmSofer.Hide();
            frmTransport.Hide();
            this.panelDesktop.Controls.Add(homepage);
            homepage.Show();
        }

        private void closeLabel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e) {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        

        private void buttonMinimize_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void labelLogout_Click(object sender, EventArgs e) {
            this.Close();
            th = new Thread(openLogin);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openLogin() {
            Application.Run(new Login());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {

        }

        private void masinaToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonAdaugareMasina.PerformClick();
        }

        private void soferToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonAdaugareSofer.PerformClick();
        }

        private void transportToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonAdaugareTransport.PerformClick();
        }

        private void rutaToolStripMenuItem_Click(object sender, EventArgs e) {
            buttonAdaugareRuta.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            frmRuta.Hide();
            frmMasina.Hide();
            frmSofer.Hide();
            frmTransport.Hide();
            this.panelDesktop.Controls.Add(homepage);
            homepage.Show();
        }
    }
}
