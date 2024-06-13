using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models.Enuns;

namespace MauiApp1.Models
{
    public class Refeicao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Carboidratos { get; set; }
        public int Gorduras { get; set; }
        public int Proteinas { get; set; }
        public int Calorias { get; set; }
        public int Fibras { get; set; }
        public int Sodio { get; set; }
        public TipoRefeicaoEnum Classe { get; set; }
    }
}
