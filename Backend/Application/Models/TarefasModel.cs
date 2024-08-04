using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TarefasModel : BaseModel
    {
        public string descricao { get; set; }
        public int idAluno { get; set; }
        public int idProfessor { get; set; }
        public DateTime dataLimite { get; set; }
        public string? anexo { get; set; }
    }
    public class TarefasFullModel : BaseModel
    {
        public string descricao { get; set; }
        public UsuariosLightModel aluno { get; set; }
        public UsuariosLightModel professor { get; set; }
        public DateTime dataLimite { get; set; }
        public string? anexo { get; set; }
        public bool entregue { get; set; }
        public string? anexoEntrega { get; set; }
    }
}
