using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TarefasModel : BaseModel
    {
        public string descricao { get; set; }
        public int idTurma { get; set; }
        public DateTime dataLimite { get; set; }
        public string? anexo { get; set; }
    }
}
