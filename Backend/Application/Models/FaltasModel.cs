using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class FaltasModel : BaseModel
    {
        public int idTurma { get; set; }
        public int idAluno { get; set; }
        public int idAula { get; set; }

    }
    public class FaltasFullModel : BaseModel
    {
        public TurmasModel turma { get; set; }
        public UsuariosLightModel aluno { get; set; }
        public Aulas aula { get; set; }

    }
}
