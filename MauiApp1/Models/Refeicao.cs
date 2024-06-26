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
        public int IdRefeicao { get; set; }
        public int IdUsuario { get; set; }
        public int IdAlimento { get; set; }
        public TipoRefeicaoEnum TpRefeicao { get; set; }

    }

}
