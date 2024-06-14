using MauiApp1.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class TipoMetas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Idade { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
        public TipoMetasEnum Classe { get; set; }
    }
}
