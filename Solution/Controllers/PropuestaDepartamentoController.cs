using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data = Solution.DO.Objects;
using datamodels = Solution.API.DataModels;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaDepartamentoController : ControllerBase
    {
    }
}
