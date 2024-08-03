using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UsuarioTurmaModel : BaseModel
    {
        public int idUsuario { get; set; }
        public int idTurma { get; set; }
    }
    public class UsuarioTurmaFullModel : BaseModel
    {
        public UsuariosLightModel usuario { get; set; }
        public TurmasModel turma { get; set; }
        public int quantidadeFaltas { get; set; }
        public bool faltaAula {  get; set; }
        public int? idFalta { get; set; }
    }
}
