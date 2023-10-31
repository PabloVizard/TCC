using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Usuarios : BaseEntity
    {
        public string nomeCompleto { get; set; }

        public string cpf { get; set; }

        public string email { get; set; }
        public string senha { get; set; }
        public TipoUsuario tipoUsuario { get; set; }

    }
}
