using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Pharmacie
{
    class Responsable : Personnel
    {
        int id_resp;
        public Responsable(int id, string matricule, string mdp, string nom, string prenom, string adresse, string embauche, string region)
            : base(id, matricule, mdp, nom, prenom, adresse, embauche, region)
        {

        }

        public int Id_resp { get => id_resp; }
    }
}
