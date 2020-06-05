using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionEtudiant.Models;
using GestionEtudiants;
using GestionEtudiants.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace GestionEtudiant.Controllers
{
    public class EtudiantController : Controller
    {
        private string connectionString;

        public EtudiantController(IConfiguration configRoot)
        {
            connectionString = configRoot["ConnectionStrings:DefaultConnection"];
        }


        public IActionResult Index()
        {
            EtudiantContext etudiantContext = new EtudiantContext(connectionString);
            List<Etudiant> etudiants = etudiantContext.GetAll();

            EtudiantsViewModel model = new EtudiantsViewModel();
            model.Etudiants = etudiants;

            return View(model);
        }


        public IActionResult Delete(int id)
        {
            EtudiantContext etudiantContext = new EtudiantContext(connectionString);
            bool isOK = etudiantContext.Delete(id);

            DeleteEtudiantViewModel model = new DeleteEtudiantViewModel();
            model.IsDeleted = isOK;

            return View(model);
        }

        private List<SelectListItem> populatesPromotion()
        {
            PromotionContext promotionContext = new PromotionContext(connectionString);

            List<Promotion> promotions = promotionContext.GetAll();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (Promotion promotion in promotions)
            {
                selectListItem.Add(new SelectListItem(promotion.Label, promotion.Identifiant.ToString()));
            }

            return selectListItem;
        }

        public IActionResult Create()
        {

            EtudiantViewModel model = new EtudiantViewModel();
            model.Promotions = populatesPromotion();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EtudiantViewModel etudiantModel)
        {
            EtudiantContext etudiantContext = new EtudiantContext(connectionString);

            etudiantModel.Promotions = populatesPromotion();


            //Rajouter des contrôles dynamiques

            if (etudiantModel.IdPromotion == 2)
            {
                ModelState.AddModelError("IdentifiantPromotion", "Ne peut être égal à 2");
            }

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Etudiant etudiant = new Etudiant();

                etudiant.Nom = etudiantModel.Nom;
                etudiant.Prenom = etudiantModel.Prenom;
                etudiant.IdPromotion = etudiantModel.IdPromotion;
                etudiant.Age = etudiantModel.Age;
                etudiant.Genre = etudiantModel.Genre;

                bool isOK = etudiantContext.Insert(etudiant);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(etudiantModel);
            }

            return retour;
        }



        public IActionResult Edit(int id)
        {
            EtudiantContext etudiantContext = new EtudiantContext(connectionString);
            Etudiant etudiant = etudiantContext.Get(id);
            EtudiantViewModel etudiantModel = new EtudiantViewModel();

            etudiantModel.Identifiant = etudiant.Identifiant;
            etudiantModel.Nom = etudiant.Nom;
            etudiantModel.Prenom = etudiant.Prenom;
            etudiantModel.IdPromotion = etudiant.IdPromotion;
            etudiantModel.Genre = etudiant.Genre;
            etudiantModel.Age = etudiant.Age;

            etudiantModel.Promotions = populatesPromotion();

            return View(etudiantModel);
        }

        [HttpPost]
        public IActionResult Edit(EtudiantViewModel etudiantModel)
        {
            EtudiantContext etudiantContext = new EtudiantContext(connectionString);
            etudiantModel.Promotions = populatesPromotion();
            //Rajouter des contrôles dynamiques

            //if(etudiantModel.IdentifiantPromotion == 2)
            //{
            //    ModelState.AddModelError("IdentifiantPromotion", "Ne peut être égal à 2");
            //}

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Etudiant etudiant = new Etudiant();

                etudiant.Identifiant = (int)etudiantModel.Identifiant;
                etudiant.Nom = etudiantModel.Nom;
                etudiant.Prenom = etudiantModel.Prenom;
                etudiant.IdPromotion = etudiantModel.IdPromotion;
                etudiant.Age = etudiantModel.Age;
                etudiant.Genre = etudiantModel.Genre;

                bool isOK = etudiantContext.Update(etudiant);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(etudiantModel);
            }

            return retour;
        }
    }
}
