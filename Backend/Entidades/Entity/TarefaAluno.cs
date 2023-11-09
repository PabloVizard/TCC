using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class TarefaAluno : BaseEntity
    {
        public int idAluno { get; set; }
        public int idTarefa { get; set; }
        public string anexo { get; set; }
        public DateTime dataEntrega { get; set; }
        public DateTime dataLimite { get; set; }
    }
}
