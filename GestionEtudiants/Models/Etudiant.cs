using System;
using System.Collections.Generic;
using System.Text;

namespace GestionEtudiants.Models
{
    public class Etudiant
    {
        public int Identifiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public bool Genre { get; set; }
        public int Age { get; set; }
        public string Promotion { get; set; }
        public short IdPromotion { get; set; }
    }
}
