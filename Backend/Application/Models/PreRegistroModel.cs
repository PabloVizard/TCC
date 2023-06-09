using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PreRegistroModel : BaseModel
    {
        public string cpf { get; set; }
        public bool coordenador { get; set; }
        public bool professor { get; set; }
        public bool orientador { get; set; }
        public bool aluno { get; set; }
        public bool cadastrado { get; set; }
        public int idTurma { get; set; }

    }
}
