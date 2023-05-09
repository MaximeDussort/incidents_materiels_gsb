using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Pharmacie
{
    public class Personnel
    {
        int id;
        string matricule;
        string mdp;
        string nom;
        string prenom;
        string adresse;
        string embauche;
        string region;

        public Personnel(int id, string matricule, string mdp, string nom, string prenom, string adresse, string embauche, string region)
        {
            this.id = id;
            this.matricule = matricule;
            this.mdp = mdp;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.embauche = embauche;
            this.region = region;
        }
        public Personnel(string matricule, string mdp, string nom, string prenom, string adresse, string embauche, string region)
        {
            this.matricule = matricule;
            this.mdp = mdp;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.embauche = embauche;
            this.region = region;
        }

        public int Id { get => id;}
        public string Matricule { get => matricule; set => matricule = value; }
        public string Mdp { get => mdp; set => mdp = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Embauche { get => embauche; set => embauche = value; }
        public string Region { get => region; set => region = value; }
    }
}
