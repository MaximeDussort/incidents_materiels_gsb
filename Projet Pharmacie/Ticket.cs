using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Pharmacie
{
    class Ticket
    {
        int id_ticket;
        string horodate_debut;
        string horodate_fin;
        int gravite;
        string objet;
        string avancement;
        Materiel materielConcerne;

        public Ticket(int id_ticket, string horodate_debut, string horodate_fin, int gravite, string objet, string avancement, Materiel materielConcerne)
        {
            this.id_ticket = id_ticket;
            this.horodate_debut = horodate_debut;
            this.horodate_fin = horodate_fin;
            this.gravite = gravite;
            this.objet = objet;
            this.avancement = avancement;
            this.materielConcerne = materielConcerne;
        }
        public Ticket(string horodate_debut, string horodate_fin, int gravite, string objet, string avancement, Materiel materielConcerne)
        {
            this.horodate_debut = horodate_debut;
            this.horodate_fin = horodate_fin;
            this.gravite = gravite;
            this.objet = objet;
            this.avancement = avancement;
            this.materielConcerne = materielConcerne;
        }

        public int Id_ticket { get => id_ticket; }
        public string Horodate_debut { get => horodate_debut; set => horodate_debut = value; }
        public string Horodate_fin { get => horodate_fin; set => horodate_fin = value; }
        public int Gravite { get => gravite; set => gravite = value; }
        public string Objet { get => objet; set => objet = value; }
        public string Avancement { get => avancement; set => avancement = value; }
        public Materiel MaterielConcerne { get => materielConcerne; set => materielConcerne = value; }
    }
}
