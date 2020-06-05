using GestionEtudiants.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionEtudiants
{
    public class PromotionContext
    {
        private String connectionString;
        public PromotionContext(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<Promotion> GetAll()
        {
            List<Promotion> promotions = new List<Promotion>();
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "SELECT identifiant, label FROM promotion ORDER BY identifiant";

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Promotion p = new Promotion();

                    p.Identifiant = reader.GetInt16("identifiant");
                    p.Label = reader.GetString("label");


                    promotions.Add(p);
                }
                return promotions;
            }
        }

        public bool Insert(Promotion promotion)
        {
            return true;
        }

        public bool Update(Promotion promotion)
        {
            return true;
        }

        public bool Delete(int id)
        {
            return true;
        }
    }
}
