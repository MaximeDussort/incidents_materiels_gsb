using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Pharmacie
{
    class Technicien : Personnel
    {
        int id_tech;
        int niveau_intervention;
        string formation;
        string compétences;

        public Technicien(int id, string matricule, string mdp, string nom, string prenom, string adresse, string embauche, string region, int id_tech, int niveau_intervention, string formation, string compétences)
            : base(id, matricule, mdp, nom, prenom, adresse, embauche, region)
        {

        }
        public Technicien(string matricule, string mdp, string nom, string prenom, string adresse, string embauche, string region, int niveau_intervention, string formation, string compétences)
            : base(matricule, mdp, nom, prenom, adresse, embauche, region)
        {

        }

        public int Id_tech { get => id_tech;}
        public int Niveau_intervention { get => niveau_intervention; set => niveau_intervention = value; }
        public string Formation { get => formation; set => formation = value; }
        public string Compétences { get => compétences; set => compétences = value; }
    }
}
