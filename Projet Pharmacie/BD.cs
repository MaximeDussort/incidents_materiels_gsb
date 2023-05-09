using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace Projet_Pharmacie
{
    class BD
    {
        string connString = "Server=127.0.0.1;Database=projet_pharmacie;Uid=root;Password=;";
        MySqlConnection cnn;

        
        public bool isMatricule(string matricule)
        {
            string tempMatricule;
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand getDbMatricule = cnn.CreateCommand();
            getDbMatricule.CommandText = "SELECT `matricule` FROM `personnel`";
            MySqlDataReader dataReader = getDbMatricule.ExecuteReader();

            while (dataReader.Read())
            {
                tempMatricule = Convert.ToString(dataReader["matricule"]);
                if (tempMatricule == matricule)
                {
                    cnn.Close();
                    return true;
                }
            }
            cnn.Close();
            return false;
        }
        public bool isPasswordOkay(string matriculeConcerne, string testPassword)
        {
            string tempPassword;
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(testPassword);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string finalTestPassword = ByteArrayToString(tmpHash);
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand getDbPassword = cnn.CreateCommand();
            getDbPassword.CommandText = "SELECT mdp FROM personnel WHERE matricule = '" + matriculeConcerne +"'";
            tempPassword = getDbPassword.ExecuteScalar().ToString();
            
            if (finalTestPassword == tempPassword)
            {
                cnn.Close();
                return true;
            }
            cnn.Close();
            return false;
        }
        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public string verifQualification(string matriculeValide)
        {
            string reponseDB = "Personnel";
            cnn = new MySqlConnection(connString);
            cnn.Open();
            //Vérification de si l'utilisateur authentifié est technicien
            MySqlCommand isTechnicien = cnn.CreateCommand();
            isTechnicien.CommandText = "SELECT Id_technicien FROM technicien WHERE Id_personnel = (SELECT Id_personnel FROM personnel WHERE matricule = '" + matriculeValide + "')";
            bool reponseTechnicien = Convert.ToBoolean(isTechnicien.ExecuteScalar());
            if (reponseTechnicien)
            {
                reponseDB = "Technicien";
            }
            //Vérification de si l'utilisateur authentifié est responsable
            MySqlCommand isResponsable = cnn.CreateCommand();
            isResponsable.CommandText = "SELECT Id_Responsable FROM responsable WHERE Id_personnel = (SELECT Id_personnel FROM personnel WHERE matricule = '" + matriculeValide + "')";
            bool reponseResponsable = Convert.ToBoolean(isResponsable.ExecuteScalar());
            if (reponseResponsable)
            {
                reponseDB = "Responsable";
            }
            return reponseDB;
        }
        public MySqlDataAdapter initContenuTabIncidents()
        {
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlDataAdapter getDbTickets = new MySqlDataAdapter();
            getDbTickets.SelectCommand = new MySqlCommand("SELECT ticket_incident.Id_ticket_incident, ticket_incident.objet, ticket_incident.avancement, ticket_incident.Id_Materiel, materiel.affectation FROM ticket_incident, materiel WHERE ticket_incident.Id_Materiel = materiel.Id_Materiel", cnn);
            cnn.Close();
            return getDbTickets;
        }

        public List<Materiel> getListeMateriel()
        {
            List<Materiel> toutLeMateriel = new List<Materiel>();
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand getDbMateriel = cnn.CreateCommand();
            getDbMateriel.CommandText = "SELECT * FROM materiel";
            MySqlDataReader dataReader = getDbMateriel.ExecuteReader();
            while (dataReader.Read())
            {
                Materiel unMateriel = new Materiel(Convert.ToInt32(dataReader[0]), dataReader[1].ToString(), Convert.ToInt32(dataReader[2]), Convert.ToInt32(dataReader[3]), dataReader[4].ToString(), dataReader[5].ToString());
                toutLeMateriel.Add(unMateriel);
            }
            cnn.Close();
            return toutLeMateriel;
        }
        public List<Personnel> getListePersonnel()
        {
            List<Personnel> toutLePersonnel = new List<Personnel>();
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand getDbPersonnel = cnn.CreateCommand();
            getDbPersonnel.CommandText = "SELECT * FROM personnel";
            MySqlDataReader dataReader = getDbPersonnel.ExecuteReader();
            while (dataReader.Read())
            {
                Personnel unPersonnel = new Personnel(Convert.ToInt32(dataReader[0]), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(), dataReader[4].ToString(), dataReader[5].ToString(), dataReader[6].ToString(), dataReader[7].ToString());
                toutLePersonnel.Add(unPersonnel);
            }
            cnn.Close();
            return toutLePersonnel;
        }

        public void createTicket(string horodate1, string horodate2, int gravite, string objet, string avancement, int materielConcerne, int affectation)
        {
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand createDbTickets = cnn.CreateCommand();
            createDbTickets.CommandText = "INSERT INTO ticket_incident VALUES (0,  \"" + horodate1 + "\", \"" + horodate2 + "\", " + gravite + ",\"" + objet + "\", \"" + avancement + "\", " + materielConcerne + ", " + affectation + ")";
            createDbTickets.ExecuteNonQuery();
            cnn.Close();
        }
        public void addMateriel(string processeur, int ram, int espace_disque, string logiciels_installés, string affectation)
        {
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand createNewMateriel = cnn.CreateCommand();
            createNewMateriel.CommandText = "INSERT INTO materiel VALUES (0, \"" + processeur + "\", \"" + ram + "\", \"" + espace_disque + "\", \"" + logiciels_installés + "\", \"" + affectation + "\")";
            createNewMateriel.ExecuteNonQuery();
            cnn.Close();
        }
        public void supprMateriel(int idSuppr)
        {
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand supprMateriel = cnn.CreateCommand();
            supprMateriel.CommandText = "DELETE FROM materiel WHERE id_materiel = "+idSuppr;
            supprMateriel.ExecuteNonQuery();
            cnn.Close();
        }
        public MySqlDataAdapter initContenuTabMateriel()
        {
            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlDataAdapter getDbMateriel = new MySqlDataAdapter();
            getDbMateriel.SelectCommand = new MySqlCommand("SELECT affectation, processeur, ram, espace_disque, Id_Materiel FROM materiel", cnn);
            cnn.Close();
            return getDbMateriel;
        }

        public void updateAvancement(int id, string avancement)
        {

            cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand updateAvancementTicket = cnn.CreateCommand();
            updateAvancementTicket.CommandText = "UPDATE `ticket_incident` SET avancement = \"" + avancement + "\" WHERE Id_ticket_incident = " + id;
            updateAvancementTicket.ExecuteNonQuery();
            cnn.Close();
        }

        public void addPersonnel(string matricule, string mdp, string nom, string prenom, string adresse, string embauche, string region, bool isTechnicien, int niveau_intervention, string formation, string competence)
        {
            cnn = new MySqlConnection(connString);
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(mdp);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string finalMdp = ByteArrayToString(tmpHash);
            cnn.Open();
            MySqlCommand createNewPersonnel = cnn.CreateCommand();
            createNewPersonnel.CommandText = "INSERT INTO personnel VALUES (0, \"" + matricule + "\", \"" + finalMdp + "\", \"" + nom + "\", \"" + prenom + "\", \"" + adresse + "\", \"" + embauche + "\", \"" + region + "\")";
            createNewPersonnel.ExecuteNonQuery();
            if (isTechnicien == true)
            {
                MySqlCommand getIdPersonnel = cnn.CreateCommand();
                getIdPersonnel.CommandText = "SELECT Id_personnel FROM personnel WHERE matricule = '" + matricule + "'";
                int idPersonnel = Convert.ToInt32(getIdPersonnel.ExecuteScalar());
                MySqlCommand createNewTechnicien = cnn.CreateCommand();
                createNewTechnicien.CommandText = "INSERT INTO technicien VALUES (0, \"" + niveau_intervention + "\", \"" + formation + "\", \"" + competence + "\", \"" + idPersonnel + "\")";
                createNewTechnicien.ExecuteNonQuery();
            }
            cnn.Close();
        }
    }
}
