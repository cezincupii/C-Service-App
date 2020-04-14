using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Cupii_Cezin_1048 {
    class Transporturi {
        private int idTransport;
        private string numeFirma;
        private string destinatie;
        private int[] greutateTransport;

        public Transporturi(int idTransport, string numeFirma, string destinatie, int[] greutateTransport) {
            this.IdTransport = idTransport;
            this.NumeFirma = numeFirma;
            this.Destinatie = destinatie;
            this.GreutateTransport = greutateTransport;
        }

        public int IdTransport { get => idTransport; set => idTransport = value; }
        public string NumeFirma { get => numeFirma; set => numeFirma = value; }
        public string Destinatie { get => destinatie; set => destinatie = value; }
        public int[] GreutateTransport { get => greutateTransport; set => greutateTransport = value; }

        public override string ToString() {
            string toPrint = null;
            toPrint += "Transportul cu ID-ul " + this.idTransport + " detinut de firma " + this.numeFirma + " are destinatia " + this.destinatie;
            return toPrint;
        }
    }
}
