using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_Pharmacie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BD BDConnect;
        private void Form1_Load(object sender, EventArgs e)
        {
            BDConnect = new BD();
        }
        private void refreshListeTickets()
        {
            DataSet ds = new DataSet();
            //Initialisation de la DataGridView
            BDConnect.initContenuTabIncidents().Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            //Vidage des champs du formulaire
            comboBox1.Text = "";
            textBox1.Text = "";
            numericUpDown1.Text = "0";
            //Initialisation de la liste de Materiel sur l'interface des tickets
            List<Materiel> collecMateriel = BDConnect.getListeMateriel();
            comboBox1.Items.Clear();
            foreach (Materiel unMateriel in collecMateriel)
            {
                comboBox1.Items.Add(unMateriel.Id_materiel);
            }
        }

        private void refreshListeMateriel()
        {
            DataSet ds = new DataSet();
            //Initialisation de la DataGridView
            BDConnect.initContenuTabMateriel().Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            //Vidage des champs du formulaire
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            numericUpDown2.Text = "0";
            numericUpDown3.Text = "0";
            //Initialisation de la liste de Materiel sur l'interface des techniciens
            List<Materiel> collecMateriel = BDConnect.getListeMateriel();
            comboBox2.Items.Clear();
            foreach (Materiel unMateriel in collecMateriel)
            {
                comboBox2.Items.Add(unMateriel.Id_materiel);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BD BDConnect = new BD();
            BDConnect.addMateriel(textBox5.Text, Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown3.Value), textBox6.Text, textBox4.Text);
            refreshListeTickets();
            refreshListeMateriel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BDConnect.isMatricule(textBox2.Text))
            {
                if (BDConnect.isPasswordOkay(textBox2.Text, textBox3.Text))
                {
                    MessageBox.Show("Mot de passe correct.", "Résultat");
                    groupBox1.Enabled = false;

                    dataGridView1.Refresh();
                    string qualification = BDConnect.verifQualification(textBox2.Text);
                    if (qualification == "Responsable")
                    {
                        groupBox1.Enabled = true;
                        groupBox4.Enabled = true;
                        refreshListeTickets();
                        refreshListeMateriel();
                    }
                    else if (qualification == "Technicien")
                    {
                        dataGridView2.Refresh();
                        groupBox1.Enabled = true;
                        groupBox3.Enabled = true;
                        groupBox7.Enabled = true;
                        refreshListeTickets();
                        refreshListeMateriel();
                    }
                    else
                    {
                        refreshListeTickets();
                        refreshListeMateriel();
                    }
                }
                else
                {
                    MessageBox.Show("Mot de passe incorrect.", "Résultat");
                }
            }
            else
            {
                MessageBox.Show("Matricule incorrect.", "Résultat");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BD BDConnect = new BD();
            BDConnect.supprMateriel(Convert.ToInt32(Convert.ToInt32(comboBox2.Text)));
            comboBox2.Text = "";
            refreshListeTickets();
            refreshListeMateriel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BD BDConnect = new BD();
            Materiel materielConcerne = new Materiel(-1, "-1", -1, -1, "-1", "-1");
            Personnel personnelAffecté = new Personnel(-1, "-1", "-1", "-1", "-1", "-1", "-1", "-1");
            List<Materiel> listeMateriel = BDConnect.getListeMateriel();
            List<Personnel> listePersonnel = BDConnect.getListePersonnel();
            foreach (Materiel unMateriel in listeMateriel)
            {
                if (unMateriel.Id_materiel.ToString() == comboBox1.SelectedItem.ToString())
                {
                    materielConcerne = unMateriel;
                    foreach (Personnel unPersonnel in listePersonnel)
                    {
                        if (materielConcerne.Affectation == unPersonnel.Matricule)
                        {
                            personnelAffecté = unPersonnel;
                            break;
                        }
                    }
                }
            }
            if (materielConcerne.Id_materiel != -1 && personnelAffecté.Id != -1)
            {
                BDConnect.createTicket(DateTime.Now.ToString(), "00/00/0000 00:00:00", Convert.ToInt32(numericUpDown1.Value), textBox1.Text, "Enregistrée", materielConcerne.Id_materiel, personnelAffecté.Id);
                refreshListeTickets();
                refreshListeMateriel();
            }
            else
            {
                MessageBox.Show("Erreur Systeme", "Résultat");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            BDConnect.updateAvancement(Convert.ToInt32(textBox7.Text), comboBox3.Text);
            textBox7.Text = "";
            comboBox3.Text = "";
            refreshListeTickets();
            refreshListeMateriel();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool isTechnicien = false;            
            if (checkBox1.Checked)
            {
                isTechnicien = true;
                Technicien nouveauTechnicien = new Technicien(textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox13.Text, textBox14.Text, textBox12.Text, Convert.ToInt32(numericUpDown4.Value), textBox16.Text, textBox15.Text);
                BDConnect.addPersonnel(nouveauTechnicien.Matricule, nouveauTechnicien.Mdp, nouveauTechnicien.Nom, nouveauTechnicien.Prenom, nouveauTechnicien.Adresse, nouveauTechnicien.Embauche, nouveauTechnicien.Region, isTechnicien, nouveauTechnicien.Niveau_intervention, nouveauTechnicien.Formation, nouveauTechnicien.Compétences);
            }
            else
            {
                Personnel nouveauPersonnel = new Personnel(textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox13.Text, textBox14.Text, textBox12.Text);
                BDConnect.addPersonnel(nouveauPersonnel.Matricule, nouveauPersonnel.Mdp, nouveauPersonnel.Nom, nouveauPersonnel.Prenom, nouveauPersonnel.Adresse, nouveauPersonnel.Embauche, nouveauPersonnel.Region, isTechnicien, 0, "", "");
            }

            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            numericUpDown4.Value = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (groupBox9.Enabled)
            {
                groupBox9.Enabled = false;
            }
            else
            {
                groupBox9.Enabled = true;
            }
        }
    }
}
