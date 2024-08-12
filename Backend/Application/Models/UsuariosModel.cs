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
        public string matricula { get; set; }
    }

    public class UsuariosLightModel : BaseModel
    {
        public string nomeCompleto { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public TipoUsuario tipoUsuario { get; set; }
        public string matricula { get; set; }
    }

    public class UsuariosFullModel
    {
        public UsuariosLightModel? usuario { get; set; }
        public PreRegistro? preRegistro {  get; set; }
        public ProjetosFullModel? projetos { get; set; }
        public Orientacoes? orientacoes { get; set; }
        public Bancas? bancas { get; set; }
        public Faltas? faltas { get; set; }
        public TarefaAluno? tarefaAluno { get; set; }
        public Turmas? turmaAluno { get; set; }
    }

}
