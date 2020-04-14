using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Cupii_Cezin_1048 {
    class Rute {
        private int idRuta;
        private string localitatePlecare;
        private string localitateSosire;
        private int nrKilometri;
        private string[] opriri;

        public Rute(int idRuta, string localitatePlecare, string localitateSosire, int nrKilometri, string[] opriri) {
            this.IdRuta = idRuta;
            this.LocalitatePlecare = localitatePlecare;
            this.LocalitateSosire = localitateSosire;
            this.NrKilometri = nrKilometri;
            this.Opriri = opriri;
        }

        public int IdRuta { get => idRuta; set => idRuta = value; }
        public string LocalitatePlecare { get => localitatePlecare; set => localitatePlecare = value; }
        public string LocalitateSosire { get => localitateSosire; set => localitateSosire = value; }
        public int NrKilometri { get => nrKilometri; set => nrKilometri = value; }
        public string[] Opriri { get => opriri; set => opriri = value; }

        public override string ToString() {
            string toPrint = null;
            toPrint+= "Ruta cu ID-ul :" + this.idRuta + " porneste din localitatea " + this.localitatePlecare + " si ajunge in localitatea " + this.localitateSosire + ", parcurgand " + this.nrKilometri + " km";
            return toPrint;
        }
    }
}
