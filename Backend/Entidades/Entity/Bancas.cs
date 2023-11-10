using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Bancas : BaseEntity
    {
        public string professorOrientador { get; set; }
        public string nomeProjeto { get; set; }
        public string Avaliador01 { get; set; }
        public string Avaliador02 { get; set; }
        public int? idAlunoOrientado { get; set; }
        public string nomeAlunoOrientado { get; set; }
        public int ano { get; set; }
        public int semestre { get; set; }

    }
}
