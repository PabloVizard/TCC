using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Tarefas : BaseEntity
    {
        public string descricao { get; set; }
        public int idTurma { get; set; }
        public int idProfessor { get; set; }
        public DateTime dataLimite { get; set; }
        public string? anexo { get; set; }

    }
}
