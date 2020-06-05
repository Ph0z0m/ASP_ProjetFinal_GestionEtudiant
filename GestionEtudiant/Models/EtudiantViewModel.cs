using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionEtudiant.Models
{
    public class EtudiantViewModel
    {
        [HiddenInput]
        public int? Identifiant { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est requis")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le genre de l'étudiant est demandé.")]
        public bool Genre { get; set; }

        [Display(Name = "Quel âge à l'étudiant?")]
        public int Age { get; set; }

        [Display(Name = "Dans quelle promotion l'étudiant est inscrit?")]
        

        public short IdPromotion { get; set; }

        public List<SelectListItem> Promotions { get; set; }
    }
}
