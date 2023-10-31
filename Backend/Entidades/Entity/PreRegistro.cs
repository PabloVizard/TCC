using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class PreRegistro : BaseEntity
    {
        public string cpf { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public bool cadastrado { get; set; }
        public int idTurma { get; set; }
    }
}
