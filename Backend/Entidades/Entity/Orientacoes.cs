using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class Orientacoes : BaseEntity
    {
        [ForeignKey("Usuarios")]
        public int idProfessorOrientador { get; set; }
        public Usuarios professorOrientador { get; set; }
        [ForeignKey("Usuarios")]
        public int idAlunoOrientado { get; set; }
        public Usuarios alunoOrientado { get; set; }
        [ForeignKey("Turmas")]
        public int idTurma { get; set; }
        public Turmas turma { get; set; }
        public string statusAprovacao { get; set; }
    }
}
