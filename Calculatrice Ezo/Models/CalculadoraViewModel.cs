using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calculatrice_Ezo.Models
{
    public class CalculadoraViewModel
    {
        [Display(Name = "Entrée votre formule" )]
        public string Input { get; set; }

        public string ResultEntry { get; set; }
    }
}
