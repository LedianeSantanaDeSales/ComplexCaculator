using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calculatrice_Ezo.Models
{
    public class CalculadoraViewModel
    {
        [Display(Name = "Entrée les numéros à calculer" )]
        public string Input { get; set; }

        [Display(Name = "Resultat")]
        public string ResultEntry { get; set; }
    }
}
