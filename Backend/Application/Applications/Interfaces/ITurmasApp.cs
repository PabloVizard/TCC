﻿using Application.Models;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications.Interfaces
{
    public interface ITurmasApp : IBaseApp<Turmas, TurmasModel>
    {
        TurmasModel ObterTurmaPorId(int idTurma);
    }
    
}
