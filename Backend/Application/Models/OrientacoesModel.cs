using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrientacoesModel : BaseModel
    {
        public int idProfessorOrientador { get; set; }
        public int idAlunoOrientado { get; set; }
        public int idTurma { get; set; }
        public string statusAprovacao { get; set; }
    }
}
