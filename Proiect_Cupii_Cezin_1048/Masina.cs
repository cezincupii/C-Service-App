using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_Cupii_Cezin_1048
{
    public class Masina : ICloneable, IComparable
    {
        private int id;
        private string firma;
        private int anFabricate;
        private int pret;
        private int[] modele;

        public Masina(int id, string f, int d, int p, int[] m)
        {
            this.id = id;
            this.Firma = f;
            this.anFabricate = d;
            this.Pret = p;
            this.Modele = new int[m.Length];
            for (int i = 0; i < m.Length; i++)
            {
                this.Modele[i] = m[i];
            }
        }


        public int Id { get => id; set => id = value; }
        public string Firma { get => firma; set => firma = value; }
        public int AnFabricate { get => anFabricate; set => anFabricate = value; }
        public int Pret { get => pret; set => pret = value; }
        public int[] Modele { get => modele; set => modele = value; }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        public Object Clone()
        {

            Masina m = (Masina)this.MemberwiseClone();
            int[] modeleNoi = (int[])modele.Clone();
            m.modele = modeleNoi;
            return m;
        }

        public int CompareTo(object obj)
        {
            Masina m = (Masina)obj;
            if (this.pret < m.pret)
            {
                return -1;
            }
            if (this.pret > m.pret)
            {
                return 1;
            }
            else
            {
                return string.Compare(this.firma, m.firma);
            }
        }

        public override string ToString()
        {
            string toPrint = null;
            toPrint += this.id + " produsa de firma : " + this.firma + " are pretul de " + this.pret + " euro";
            return toPrint;
        }

        public static Masina operator +(Masina m, int n)
        {

            int[] modeleNoi = new int[m.modele.Length + 1];

            for (int i = 0; i < m.modele.Length; i++)
            {
                modeleNoi[i] = m.modele[i];
            }
            modeleNoi[modeleNoi.Length - 1] = n;
            m.modele = modeleNoi;
            return m;
        }

        public static Masina operator ++(Masina m)
        {
            m++;
            return m;
        }

        public int this[int index]
        {
            get
            {
                if(index>=0 && index <this.modele.Length)
                {
                    return this.modele[index];
                }
                else
                {
                    return -1;
                }
            }
        }





    }
}
