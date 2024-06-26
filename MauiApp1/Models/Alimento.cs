using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Alimento
    {
        public int IdAlimento { get; set; }
        public string NomeAlimento { get; set; }
        public double Carboidratos { get; set; }
        public double Gorduras { get; set; }
        public double Proteinas { get; set; }
        public int Calorias { get; set; }
        public double Fibras { get; set; }
        public int Sodio { get; set; }
    }
}
