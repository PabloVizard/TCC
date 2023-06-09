using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UsuariosModel : BaseModel
    {
        public string nomeCompleto { get; set; }

        public string cpf { get; set; }

        public string email { get; set; }
        public string senha { get; set; }
        public bool coordenador { get; set; }
        public bool professor { get; set; }
        public bool orientador { get; set; }
        public bool aluno { get; set; }
        public int? idTurma { get; set; }
        public int? idProfessorOrientador { get; set; }
        public int? idAlunoOrientado { get; set; }
    }
}
