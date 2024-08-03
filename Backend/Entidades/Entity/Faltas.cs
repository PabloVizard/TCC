using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Faltas : BaseEntity
    {
        public int idTurma { get; set; }
        public int idAluno { get; set; }
        public int idAula { get; set; }

    }
}
