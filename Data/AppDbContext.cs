using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebNotebook.Models;

namespace WebNotebook.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts ) : base(opts)
         {

        }

        protected  override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Telefone>()
                .HasOne(telefone => telefone.Paciente)
                .WithMany(paciente => paciente.Telefones)
                .HasForeignKey(telefone => telefone.PacienteId);

        }


        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public DbSet<Especialidade> Especialidade{ get; set; }


    }
}
