using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRMFacilitoInicial.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("NombreCompleto", this.NombreCompleto));


            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Telefono>() //El telefono es el que se va a modificar
                .HasRequired(x => x.Cliente).WithMany(x => x.Telefonos).WillCascadeOnDelete(true);  //Un cliente puede tener varios telefonos por eso es la relación de uno a muchos


            modelBuilder.Entity<Email>()
                .HasRequired(x => x.Cliente).WithMany(x => x.Correos).WillCascadeOnDelete(true);

            modelBuilder.Entity<Direccion>()
             .HasRequired(x => x.Cliente).WithMany(x => x.Direcciones).WillCascadeOnDelete(true);



            //como se cambio las clases del modelo hay que actualizar la bd y borrar los registros de bd para q no de error de nulos
            //ya no se van a permitir nulos

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<TipoActividad> TipoActividades { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<Campania> Campanias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }


    }
}