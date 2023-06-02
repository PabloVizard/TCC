using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TurmasModel
    {
        public int idTurmas { get; set; }
        public string descricao { get; set; }
        public int ano { get; set; }
        public int semestre { get; set; }
        public int flagPoc { get; set; }
    }
}
