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
        public bool coordenador { get; set; }
        public bool professor { get; set; }
        public bool orientador { get; set; }
        public bool aluno { get; set; }
        public bool cadastrado { get; set; }
        public int idTurma { get; set; }
    }
}
