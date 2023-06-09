using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AuthModel
    {
        public new int id { get; set; }
        public string nomeCompleto { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string ip { get; set; }
        public bool coordenador { get; set; }
        public bool professor { get; set; }
        public bool orientador { get; set; }
        public bool aluno { get; set; }
        public int? idTurma { get; set; }
        public int? idProfessorOrientador { get; set; }
        public int? idAlunoOrientado { get; set; }

    }
}
