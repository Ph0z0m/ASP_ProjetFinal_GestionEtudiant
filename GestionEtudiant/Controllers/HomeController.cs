using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GestionEtudiant;
using GestionEtudiant.Models;
using GestionEtudiants;
using GestionEtudiants.Models;
using Microsoft.Extensions.Configuration;

namespace EtudiantTrackerFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configRoot)
        {
            _logger = logger;
            connectionString = configRoot["ConnectionStrings:DefaultConnection"];
        }


        public IActionResult Index()
        {
            EtudiantContext requetesEtudiants = new EtudiantContext(connectionString);
            List<Etudiant> Etudiants = requetesEtudiants.GetAll();

            AcceuilViewModel model = new AcceuilViewModel();
            model.Etudiants = Etudiants;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
