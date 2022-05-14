using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto.Roles;
using WebNotebook.Models;

namespace WebNotebook.Services
{
    public class RolesService
    {
        private RoleManager<IdentityRole<int>> _roleManager;

        private AppDbContext _context;
        private IMapper _mapper;
       
        public RolesService(AppDbContext appDbContext , IMapper mapper , RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
            _context = appDbContext;
            _mapper = mapper;
           
        }
        
        public async Task<Result> AdicionarRoles(CreateRolesDto dto)
        {
            Roles roles = _mapper.Map<Roles>(dto);
            IdentityRole<int> identityRole = _mapper.Map<IdentityRole<int>>(roles);
            var resultado = await _roleManager.CreateAsync(identityRole);

            if (resultado.Succeeded)
            {
                return Result.Ok();
            }

            return  Result.Fail(resultado.ToString());
            
        }

        public ReadRolesDto recuperarRolesPorId(int id)
        {
            IdentityRole<int> roles = _roleManager.Roles.FirstOrDefault(r=> r.Id == id);
            Roles roles1 = _mapper.Map<Roles>(roles);
            ReadRolesDto read = _mapper.Map<ReadRolesDto>(roles1);
            return read;
            
        }

        public Result deletarRoles(int id)
        {
            IdentityRole<int> role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
            {
                return Result.Fail("Role não encontrada");
            }

            _roleManager.DeleteAsync(role);
            _context.SaveChanges();
            return Result.Ok();
        }

        public List<ReadRolesDto> recuperarRoles()
        {
            List<IdentityRole<int>> identityRole = _roleManager.Roles.ToList();
            List<Roles> roles = _mapper.Map<List<Roles>>(identityRole);
            return _mapper.Map<List<ReadRolesDto>>(roles);

        }
    }
}
