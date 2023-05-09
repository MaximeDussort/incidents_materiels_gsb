using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Pharmacie
{
    class Materiel
    {
        int id_materiel;
        string processeur;
        int ram;
        int espace_disque;
        string logiciels;
        string affectation;

        public Materiel(int id_materiel, string processeur, int ram, int espace_disque, string logiciels, string affectation)
        {
            this.id_materiel = id_materiel;
            this.processeur = processeur;
            this.ram = ram;
            this.espace_disque = espace_disque;
            this.logiciels = logiciels;
            this.affectation = affectation;
        }

        public int Id_materiel { get => id_materiel; }
        public string Processeur { get => processeur; set => processeur = value; }
        public int Ram { get => ram; set => ram = value; }
        public int Espace_disque { get => espace_disque; set => espace_disque = value; }
        public string Logiciels { get => logiciels; set => logiciels = value; }
        public string Affectation { get => affectation; set => affectation = value; }
    }
}
