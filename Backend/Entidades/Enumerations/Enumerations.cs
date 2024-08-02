using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enumerations
{
    public enum TipoUsuario
    {
        Aluno = 1,
        Professor,
        Orientador,
        ProfessorOrientador,
        Coordenador
    }

    public enum StatusAprovacao
    {
        Iniciado = 1,
        PodeDefender,
        Concluido,
        Adiado,
        Reprovado
    }
}
