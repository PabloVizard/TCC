﻿using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TurmasController : BaseController<Turmas, TurmasModel>
    {
        private readonly ITurmasApp _turmasApp;
        public TurmasController(ITurmasApp turmasApp) : base(turmasApp)
        {
            _turmasApp = turmasApp;
        }
    }
}
