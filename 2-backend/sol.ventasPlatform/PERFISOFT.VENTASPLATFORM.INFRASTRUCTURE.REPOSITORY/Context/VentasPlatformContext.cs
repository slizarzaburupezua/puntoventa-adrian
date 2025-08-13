using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Entities;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context
{
    public class VentasPlatformContext : DbContext
    {
        public VentasPlatformContext(DbContextOptions<VentasPlatformContext> options) : base(options)
        {
        }

        #region DBSET

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<UsuarioId> UsuarioId { get; set; }

        public DbSet<UsuarioRestableceContrasenia> UsuarioRestableceContrasenia { get; set; }

        public DbSet<UsuarioContrasena> UsuarioContrasena { get; set; }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<Medida> Medida { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<Moneda> Moneda { get; set; }

        public DbSet<TipoDocumento> TipoDocumento { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Negocio> Negocio { get; set; }

        public DbSet<Rol> Rol { get; set; }

        public DbSet<Marca> Marca { get; set; }

        public DbSet<Venta> Venta { get; set; }

        public DbSet<MenuRol> MenuRol { get; set; }

        public DbSet<Genero> Genero { get; set; }

        public DbSet<VentaNumeroCorrelativo> VentaNumeroCorrelativo { get; set; }

        public DbSet<DetalleVenta> DetalleVenta { get; set; }

        public DbSet<Parametro> Parametro { get; set; }

        public DbSet<ParametroDetalle> ParametroDetalle { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region TABLES

            builder.Entity<Usuario>()
               .HasKey(o => new { o.ID });

            builder.Entity<UsuarioId>()
                .HasKey(o => new { o.ID });

            builder.Entity<UsuarioRestableceContrasenia>()
                .HasKey(o => new { o.ID });

            builder.Entity<UsuarioContrasena>()
                .HasKey(o => new { o.ID });

            builder.Entity<Categoria>()
                .HasKey(o => new { o.ID });

            builder.Entity<Producto>()
                .HasKey(o => new { o.ID });

            builder.Entity<Medida>()
                .HasKey(o => new { o.ID });

            builder.Entity<Menu>()
                .HasKey(o => new { o.ID });

            builder.Entity<Genero>()
                .HasKey(o => new { o.ID });

            builder.Entity<Moneda>()
                .HasKey(o => new { o.ID });

            builder.Entity<TipoDocumento>()
                .HasKey(o => new { o.ID });

            builder.Entity<Negocio>()
                .HasKey(o => new { o.ID });

            builder.Entity<Rol>()
                .HasKey(o => new { o.ID });

            builder.Entity<MenuRol>()
                .HasKey(o => new { o.ID });

            builder.Entity<Cliente>()
                .HasKey(o => new { o.ID });

            builder.Entity<Marca>()
                .HasKey(o => new { o.ID });

            builder.Entity<Venta>()
                .HasKey(o => new { o.ID });

            builder.Entity<VentaNumeroCorrelativo>()
                .HasKey(o => new { o.ID });

            builder.Entity<DetalleVenta>()
                .HasKey(o => new { o.ID });

            builder.Entity<Parametro>()
                .HasKey(o => new { o.ID });

            builder.Entity<ParametroDetalle>()
                .HasKey(o => new { o.ID });

            #endregion

            #region RELACION USUARIO

            builder.Entity<Usuario>()
            .HasOne(u => u.ROL)
            .WithMany(g => g.USUARIOS)
            .HasForeignKey(u => u.ID_ROL)
            .IsRequired();

            builder.Entity<Usuario>()
            .HasOne(u => u.GENERO)
            .WithMany(g => g.USUARIOS)
            .HasForeignKey(u => u.ID_GENERO)
            .IsRequired();

            builder.Entity<Usuario>()
            .HasOne(d => d.TIPODOCUMENTO)
            .WithMany(g => g.USUARIOS)
            .HasForeignKey(d => d.ID_TIPO_DOCUMENTO)
            .IsRequired();

            builder.Entity<Usuario>()
            .HasOne(u => u.USUARIOID)
            .WithOne(ifr => ifr.USUARIO)
            .HasForeignKey<UsuarioId>(c => c.ID_USUARIO)
            .IsRequired();

            builder.Entity<Usuario>()
            .HasOne(u => u.USUARIOCONTRASENA)
            .WithOne(ifr => ifr.USUARIO)
            .HasForeignKey<UsuarioContrasena>(c => c.ID_USUARIO)
            .IsRequired();

            #endregion

            #region RELACION CATEGORIA

            builder.Entity<Categoria>()
           .HasOne(i => i.MEDIDA)
           .WithMany(ic => ic.CATEGORIAS)
           .HasForeignKey(i => i.ID_MEDIDA)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .IsRequired();

            #endregion

            #region RELACION PRODUCTO

            builder.Entity<Producto>()
           .HasOne(i => i.CATEGORIA)
           .WithMany(ic => ic.PRODUCTOS)
           .HasForeignKey(i => i.ID_CATEGORIA)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .IsRequired();

            builder.Entity<Producto>()
           .HasOne(i => i.MARCA)
           .WithMany(ic => ic.PRODUCTOS)
           .HasForeignKey(i => i.ID_MARCA)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .IsRequired();

            #endregion

            #region RELACION NEGOCIO

            builder.Entity<Negocio>()
            .HasOne(n => n.MONEDA)
            .WithOne(m => m.NEGOCIO)
            .HasForeignKey<Negocio>(n => n.ID_MONEDA)
            .IsRequired();

            #endregion

            #region RELACION CLIENTE

            builder.Entity<Cliente>()
           .HasOne(i => i.TIPODOCUMENTO)
           .WithMany(ic => ic.CLIENTES)
           .HasForeignKey(i => i.ID_TIPO_DOCUMENTO)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .IsRequired();

            builder.Entity<Cliente>()
           .HasOne(i => i.GENERO)
           .WithMany(ic => ic.CLIENTES)
           .HasForeignKey(i => i.ID_GENERO)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .IsRequired();

            #endregion

            #region RELACION VENTA

            builder.Entity<Venta>()
           .HasOne(i => i.USUARIO)
           .WithMany(ic => ic.VENTAS)
           .HasForeignKey(i => i.ID_USUARIO)
           .OnDelete(DeleteBehavior.ClientSetNull)
           .IsRequired();

            builder.Entity<Venta>()
           .HasOne(i => i.CLIENTE)
           .WithMany(ic => ic.VENTAS)
           .HasForeignKey(i => i.ID_CLIENTE)
           .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #region RELACION DETALLE VENTA

            builder.Entity<DetalleVenta>()
            .HasOne(dv => dv.VENTA)
            .WithMany(v => v.DETALLEVENTA)
            .HasForeignKey(dv => dv.ID_VENTA)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();

            builder.Entity<DetalleVenta>()
            .HasOne(dv => dv.PRODUCTO)
            .WithMany(p => p.DETALLEVENTAS)
            .HasForeignKey(dv => dv.ID_PRODUCTO)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired();
            #endregion

            DbSeed(builder);

            base.OnModelCreating(builder);
        }

        protected virtual void DbSeed(ModelBuilder builder) { }
    }


}
