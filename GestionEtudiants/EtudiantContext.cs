using GestionEtudiants.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionEtudiants
{
    public class EtudiantContext
    {
        private String connectionString;
        public EtudiantContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Etudiant> GetAll()
        {
            {
                List<Etudiant> etudiants = new List<Etudiant>();
                using MySqlConnection c = new MySqlConnection(connectionString);
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "SELECT identifiant, nom, prenom, genre, age, promotion, identifiantPromotion FROM etudiants ORDER BY identifiant";

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Etudiant etudiant = new Etudiant
                    {
                        Identifiant = reader.GetInt32("identifiant"),
                        Nom = reader.GetString("nom"),
                        Prenom = reader.GetString("prenom"),
                        Genre = reader.GetBoolean("genre"),
                        Age = reader.GetInt16("age"),
                        Promotion = reader.GetString("promotion"),
                        IdPromotion = reader.GetInt16("identifiantPromotion")
                    };

                    etudiants.Add(etudiant);
                }
                return etudiants;
            }
        }

        public Etudiant Get(int id)
        {
            Etudiant etudiant = null;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"SELECT identifiant, nom, prenom, genre, age, promotion, identifiantPromotion  FROM etudiants
                  WHERE identifiant = @identifiant";

                command.Parameters.AddWithValue("identifiant", id);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    etudiant = new Etudiant();
                    etudiant.Identifiant = reader.GetInt32("identifiant");
                    etudiant.Nom = reader.GetString("nom");
                    etudiant.Prenom = reader.GetString("prenom");
                    etudiant.Genre = reader.GetBoolean("genre");
                    etudiant.Age = reader.GetInt16("age");
                    etudiant.Promotion = reader.GetString("promotion");
                    etudiant.IdPromotion = reader.GetInt16("identifiantPromotion");
                }
            }
            return etudiant;
        }

        /***Ajout d'un étudiant***/
        public bool Insert(Etudiant etudiant)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "INSERT INTO etudiants(promotion, nom, prenom, genre, age, promotion, identifiantPromotion) VALUE(@promotion, @nom, @prenom, @genre, @promotion, @identifiantPromotion)";

                command.Parameters.AddWithValue("nom", etudiant.Nom);
                command.Parameters.AddWithValue("prenom", etudiant.Prenom);
                command.Parameters.AddWithValue("genre", etudiant.Genre);
                command.Parameters.AddWithValue("age", etudiant.Age);
                command.Parameters.AddWithValue("promotion", etudiant.Promotion);
                command.Parameters.AddWithValue("identifiantPromotion", etudiant.IdPromotion);


                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        public bool Update(Etudiant etudiant)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"
                        UPDATE etudiants SET nom = @nom, prenom = @prenom, genre = @genre, age = @age, promotion = @promotion 
                        WHERE identifiant = @identifiant
                    ";

                command.Parameters.AddWithValue("identifiant", etudiant.Identifiant);
                command.Parameters.AddWithValue("nom", etudiant.Nom);
                command.Parameters.AddWithValue("prenom", etudiant.Prenom);
                command.Parameters.AddWithValue("genre", etudiant.Genre);
                command.Parameters.AddWithValue("age", etudiant.Age);
                command.Parameters.AddWithValue("promotion", etudiant.Promotion);
                command.Parameters.AddWithValue("identifiantPromotion", etudiant.IdPromotion);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        public bool Delete(int id)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "DELETE FROM etudiants WHERE identifiant = @id";

                command.Parameters.AddWithValue("id", id);
                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

    }
}
