using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class BancasApp : BaseApp<Bancas, BancasModel>, IBancasApp
    {
        protected readonly IBancasService _bancasService;
        protected readonly IMapper _mapper;

        public BancasApp(IBancasService bancasService, IMapper mapper) : base(bancasService, mapper)
        {
            _bancasService = bancasService;
            _mapper = mapper;
        }
    }
}
