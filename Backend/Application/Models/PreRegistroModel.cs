using Entities.Enumerations;
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
        public TipoUsuario tipoUsuario { get; set; }
        public bool cadastrado { get; set; }
        public int idTurma { get; set; }

    }
}
