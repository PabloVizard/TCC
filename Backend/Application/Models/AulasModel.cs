using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AulasModel : BaseModel
    {
        public string descricao { get; set; }
        public int idProfessor { get; set; }
        public int idTurma { get; set; }
        public DateTime dataAula { get; set; }
        public string? local { get; set; }
        public string? link { get; set; }
    }
    public class AulasFullModel : BaseModel
    {
        public string descricao { get; set; }
        public UsuariosLightModel professor { get; set; }
        public TurmasModel turma { get; set; }
        public DateTime dataAula { get; set; }
        public string? local { get; set; }
        public string? link { get; set; }
    }
}
