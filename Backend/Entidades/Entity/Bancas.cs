using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Bancas : BaseEntity
    {
        public int idProjeto { get; set; }
        public int idProfessorOrientador { get; set; }
        public int idAlunoOrientado { get; set; }
        public int idAvaliador01 { get; set; }
        public int? idAvaliador02 { get; set; }
        public int ano { get; set; }
        public int semestre { get; set; }
        public bool bancaConfirmada { get; set; }
        public DateTime? dataDefesa { get; set; }

    }
}
