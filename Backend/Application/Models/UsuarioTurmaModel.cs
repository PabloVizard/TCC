﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UsuarioTurmaModel : BaseModel
    {
        public int idUsuario { get; set; }
        public int idTurma { get; set; }
        public int tipoUsuario { get; set; }
    }
}
