﻿using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BancasModel : BaseModel
    {
        public int idProjeto { get; set; }
        public int idProfessorOrientador { get; set; }
        public int idAlunoOrientado { get; set; }
        public int idAvaliador01 { get; set; }
        public int? idAvaliador02 { get; set; }
        public int ano { get; set; }
        public int semestre { get; set; }
        public bool bancaConfirmada { get; set; }
        public bool? status {  get; set; }
        public DateTime? dataDefesa { get; set; }
        public int? numeroDefesa { get; set; }
        public string? localDefesa { get; set; }
    }
    public class BancasFullModel: BaseModel
    {
        public Projetos projeto { get; set; }
        public UsuariosLightModel professorOrientador { get; set; }
        public UsuariosLightModel alunoOrientado { get; set; }
        public UsuariosLightModel avaliador01 { get; set; }
        public UsuariosLightModel? avaliador02 { get; set; }
        public int ano { get; set; }
        public int semestre { get; set; }
        public bool bancaConfirmada { get; set; }
        public bool? status { get; set; }
        public DateTime? dataDefesa { get; set; }
        public int? numeroDefesa { get; set; }
        public string? localDefesa { get; set; }
    }
}
