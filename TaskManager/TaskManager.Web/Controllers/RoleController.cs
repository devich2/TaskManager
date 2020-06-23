using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Result;
using TaskManager.Models.Role;

namespace TaskManager.Web.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<DataResult<List<RoleModel>>> Get()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return new DataResult<List<RoleModel>>()
            {
                ResponseStatusType = roles.Any() ? ResponseStatusType.Succeed : ResponseStatusType.Warning,
                Data = roles.Select(_mapper.Map<RoleModel>).ToList()
            };
        }
    }
}