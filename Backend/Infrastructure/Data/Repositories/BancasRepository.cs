﻿using Domain.Repositories.Interfaces;
using Entities.Entity;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BancasRepository : BaseRepository<Bancas>, IBancasRepository
    {
        public BancasRepository(DataContext dataContext) : base(dataContext) { }
    }
}
