using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Cupii_Cezin_1048
{
    class Sofer: ICloneable, IComparable,Interfata
    {
        private int id;
        private string nume;
        private string prenume;
        private string dataNasterii;
        private int salariu;
        private string tipCarnet;
        private int[] kmLunari;

        public Sofer(int id, string nume, string prenume, string dataNasterii, int salariu, string tipCarnet, int[] kmLunari)
        {
            this.Id = id;
            this.nume = nume;
            this.Prenume = prenume;
            this.DataNasterii = dataNasterii;
            this.Salariu = salariu;
            this.TipCarnet = tipCarnet;
            this.KmLunari = new int[kmLunari.Length];
            for(int i=0; i<kmLunari.Length;i++)
            {
                this.kmLunari[i] = kmLunari[i];
            }
        }

        public int Id { get => id; set => id = value; }
        public string Nume { get => nume; set => nume = value; }
        public string Prenume { get => prenume; set => prenume = value; }
        public string DataNasterii { get => dataNasterii; set => dataNasterii = value; }
        public int Salariu { get => salariu; set => salariu = value; }
        public string TipCarnet { get => tipCarnet; set => tipCarnet = value; }
        public int[] KmLunari { get => kmLunari; set => kmLunari = value; }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public object Clone()
        {
            Sofer s = (Sofer)this.MemberwiseClone();
            int[] kmLunariCopie = (int[])this.KmLunari.Clone();
            s.KmLunari = kmLunariCopie;
            return s;
        }

        public int CompareTo(object obj)
        {
            Sofer s = (Sofer)obj;
            if(this.salariu<s.salariu) {
                return -1;
            }
            else if (this.salariu>s.salariu) {
                return 1;
            }
            else {
                return string.Compare(this.nume, s.nume);
            }
        }


        public override string ToString()
        {
             string toPrint = null;
            toPrint += "Soferul :" + this.nume + " " + this.prenume + " nascut la data de " + this.dataNasterii + " conduce o masina de tip " + this.tipCarnet + " si are un salariu de " + this.salariu + " lei.";
            return toPrint;
        }

        public float calculKmMedii() {
            float medie = 0;
            for(int i=0;i<this.kmLunari.Length;i++) {
                medie += this.kmLunari[i];
            }
            medie /= this.kmLunari.Length;
            return medie;
           
        }

        public static explicit operator float(Sofer v) {
            throw new NotImplementedException();
        }
    }
}
