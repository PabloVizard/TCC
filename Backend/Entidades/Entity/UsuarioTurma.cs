using Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class UsuarioTurma : BaseEntity 
    {
        public int idUsuario { get; set; }
        public int idTurma { get; set; }
        public TipoUsuario tipoUsuario { get; set; }

    }
}
