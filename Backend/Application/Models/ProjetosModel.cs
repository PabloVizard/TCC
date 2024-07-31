using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProjetosModel : BaseModel
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public int idProfessorResponsavel { get; set; }
        public int? idAlunoResponsavel { get; set; }
        public string area { get; set; }

    }

    public class ProjetosFullModel : BaseModel
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public string area { get; set; }
        public UsuariosLightModel? ProfessorResponsavel { get; set; }
        public UsuariosLightModel? AlunoResponsavel { get; set; }
    }
}
