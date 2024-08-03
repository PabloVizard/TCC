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
        public int idTurma { get; set; }
        public string? anexo { get; set; }
        public DateTime? dataEntrega { get; set; }
    }
    public class TarefaAlunoTurmaFullModel : BaseModel
    {
        public UsuariosLightModel aluno { get; set; }
        public Tarefas tarefa { get; set; }
        public TurmasModel turma { get; set; }
        public string? anexo { get; set; }
        public DateTime? dataEntrega { get; set; }
    }
}
