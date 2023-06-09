using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Turmas: BaseEntity
    {
        public string? descricao { get; set; }
        public int ano { get; set; }
        public int semestre { get; set; }
        public int nPoc { get; set; }

    }
}
