using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TarefaAlunoModel : BaseModel
    {
        public int idAluno { get; set; }
        public int idTarefa { get; set; }
        public string anexo { get; set; }
        public DateTime dataEntrega { get; set; }
        public DateTime dataLimite { get; set; }
    }
    public class TarefaAlunoFullModel : BaseModel
    {
        public UsuariosLightModel aluno { get; set; }
        public Tarefas Tarefas { get; set; }
        public string anexo { get; set; }
        public DateTime dataEntrega { get; set; }
        public DateTime dataLimite { get; set; }
    }
}
