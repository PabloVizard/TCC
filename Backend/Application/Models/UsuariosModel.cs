using Entities.Entity;
using Entities.Enumerations;
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
        public TipoUsuario tipoUsuario { get; set; }
    }
}
