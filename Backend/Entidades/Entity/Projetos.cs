using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Projetos : BaseEntity
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public int idProfessorResponsavel { get; set; }
        public int? idAlunoResponsavel { get; set; }
        public string area { get; set; }
    }
}
