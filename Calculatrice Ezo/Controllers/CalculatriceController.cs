using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calculatrice_Ezo.Implementation;
using Calculatrice_Ezo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calculatrice_Ezo.Controllers
{
    public class CalculatriceController : Controller
    {
        // GET: Calculatrice

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CalculadoraViewModel calculadoraViewModel)
        {
            var operators = new Operators();

            calculadoraViewModel.ResultEntry =  operators.Treat(calculadoraViewModel.Input);

            return View(calculadoraViewModel);
        }
    }
}