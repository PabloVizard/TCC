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
        public UsuariosModel professorOrientador { get; set; }
        public int idAlunoOrientado { get; set; }
        public UsuariosModel alunoOrientado { get; set; }

        public int idTurma { get; set; }
        public TurmasModel turma { get; set; }
        public string statusAprovacao { get; set; }
    }
}
