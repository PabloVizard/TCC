﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Usuarios
    {
        [Key]
        public int idUsuarios { get; set; }
        public string nomeCompleto { get; set; }

        public string cpf { get; set; }

        [ForeignKey("cpf")]
        [NotMapped]
        public virtual PreRegistro cpfPreRegistro { get; set; }

        public string email { get; set; }
        public string senha { get; set; }
        public bool coordenador { get; set; }
        public bool professor { get; set; }
        public bool orientador { get; set; }
        public bool aluno { get; set; }
        public int? ano { get; set; }
        public int? semestre { get; set; }

    }
}
