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

            builder.Entity<EspecialidadeDR>()
                .HasKey(x => new { x.ApplicationUserId, x.EspecialidadeId });

            builder.Entity<EspecialidadeDR>(  )
                .HasOne(especialidadeDr => especialidadeDr.ApplicationUser)
                .WithMany(usuario => usuario.EspecialidadeDRs)
                .HasForeignKey(especialidadeDr => especialidadeDr.ApplicationUserId);

            builder.Entity<EspecialidadeDR>()
               .HasOne(especialidadeDr => especialidadeDr.Especialidade)
               .WithMany(especialidade => especialidade.EspecialidadeDRs)
               .HasForeignKey(especialidadeDr => especialidadeDr.EspecialidadeId);

        }


        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public DbSet<Especialidade> Especialidade{ get; set; }

        public DbSet<EspecialidadeDR> EspecialidadeDRs { get; set; }


    }
}
