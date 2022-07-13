using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotebook.Data;
using WebNotebook.Data.Dto.User;
using WebNotebook.Data.Dto.Usuario;
using WebNotebook.Models;

namespace WebNotebook.Services
{
    public class UsuarioService
    {

        private AppDbContext _context;
        private IMapper _mapper; 
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;

        public UsuarioService(AppDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager , RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _mapper = mapper;

            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result> criaUsuarioAsync(CreateUsuarioDto dto)
        {
            
            if ( !await _roleManager.RoleExistsAsync(dto.Role))
            {
                return Result.Fail("Role incorreta");
            }

            Usuario usuario = _mapper.Map<Usuario>(dto);
            ApplicationUser criaUser = _mapper.Map<ApplicationUser>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(criaUser, dto.Password);
            var usuarioRule = _userManager.AddToRoleAsync(criaUser, dto.Role);

            if(resultadoIdentity.Result.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail(resultadoIdentity.Result.ToString());


        }

       

        public List<ReadUsuarioDto> recuperarUsuario(int skip, int take)
        {
            List<ApplicationUser> usuarioApp = _userManager.Users.Skip(skip).Take(take).ToList();
            List<ReadUsuarioDto> usuarios =  new List<ReadUsuarioDto>();

                             
            foreach (var user in usuarioApp)
            {
                Roles Addroles = new Roles();

                var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
                if (userRole != null)
                {

                
                var roles = _context.Roles.FirstOrDefault(r => r.Id == userRole.RoleId);
              


                if (roles!=null)
                {
                    Addroles.Id = roles.Id;
                    Addroles.Name = roles.Name;
                    Addroles.NormalizedName = roles.NormalizedName;
                }

                }
                usuarios.Add(new ReadUsuarioDto
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    CPF = user.CPF,
                    DataNascimento = user.DataNascimento,
                    CRM = user.CRM,
                    Email = user.Email,
                    NomeMae = user.NomeMae,
                    NomePai = user.NomePai,
                    RG = user.RG,
                    Username = user.UserName,
                    Roles = Addroles

                });

               
              
             
            }


            return usuarios;


        }

        public ReadUsuarioDto recuperaUsuarioPorId(int id)
        {
            ApplicationUser applicationUser = _userManager.Users.FirstOrDefault(usu => usu.Id == id);
            ReadUsuarioDto usuarios = new ReadUsuarioDto();

            if (applicationUser == null)
            {


                return null;

            }

            Roles Addroles = new Roles();

            var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId == applicationUser.Id);
            if (userRole != null)
            {


                var roles = _context.Roles.FirstOrDefault(r => r.Id == userRole.UserId);



                if (roles != null)
                {
                    Addroles.Id = roles.Id;
                    Addroles.Name = roles.Name;
                    Addroles.NormalizedName = roles.NormalizedName;
                }

            }

            usuarios.Id = applicationUser.Id;
            usuarios.Nome = applicationUser.Nome;
            usuarios.CPF = applicationUser.CPF;
            usuarios.DataNascimento = applicationUser.DataNascimento;
            usuarios.CRM = applicationUser.CRM;
            usuarios.Email = applicationUser.Email;
            usuarios.NomeMae = applicationUser.NomeMae;
            usuarios.NomePai = applicationUser.NomePai;
            usuarios.RG = applicationUser.RG;
            usuarios.Username = applicationUser.UserName;
            usuarios.Roles = Addroles;




            return usuarios;

        }

        public async Task<Result> atualizarUsuarioAsync(int id, UpdateUsuarioDto dto)
        {
            ApplicationUser applicationUser = _userManager.Users.FirstOrDefault(user => user.Id == id);
        
            if (applicationUser == null)
            {
                return Result.Fail("Usuario não encontrado");
            }



            applicationUser.UserName = dto.Username;
            applicationUser.Email = dto.Email;
            applicationUser.Nome = dto.Nome;
            applicationUser.DataNascimento = dto.DataNascimento;
            applicationUser.CPF = dto.CPF;
            applicationUser.RG = dto.RG;
            applicationUser.CRM = dto.CRM;
            applicationUser.NomeMae = dto.NomeMae;
            applicationUser.NomePai = dto.NomePai;
            
            IdentityResult result = await _userManager.UpdateAsync(applicationUser);
           //Task<IdentityResult> result  = identityResult;


            var usuarioRule = await _userManager.GetRolesAsync(applicationUser);

            foreach (var role in usuarioRule)
            {
                if ( role != dto.Role)
                {
                 IdentityResult resutado =   await _userManager.RemoveFromRoleAsync(applicationUser, role);
                    if(resutado.Succeeded)
                    {
                        _userManager.AddToRoleAsync(applicationUser, dto.Role);
                    }
                    
                }
            }
           if(usuarioRule.Count == 0)
            {
                _userManager.AddToRoleAsync(applicationUser, dto.Role);
            }
           

            if (result.Succeeded)
            {
                return Result.Ok();
            }

            return Result.Fail(result.ToString());


        }


        public async Task<Result> deletaUsuarui(int id)
        {
            ApplicationUser applicationUser = _userManager.Users.FirstOrDefault(u => u.Id == id);

            if (applicationUser == null)
            {
                return Result.Fail("Usuario não encontrqado");
            }

           IdentityResult identityResult = await _userManager.DeleteAsync(applicationUser);

            return identityResult.Succeeded ? Result.Ok() : Result.Fail(identityResult.Errors.ToString());
            
        }
    }
}
