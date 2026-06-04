using Microsoft.EntityFrameworkCore;
using Entidades.Models;

namespace Datos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // =========================
    // DB SETS
    // =========================
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Subcategoria> Subcategorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<DetallePedido> DetallesPedido { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Envio> Envios { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<ProductoProveedor> ProductosProveedor { get; set; }
    public DbSet<Reporte> Reportes { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<MedioContacto> MediosContacto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =====================================================
        //  PRECISION (DINERO)
        // =====================================================
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Pedido>()
            .Property(p => p.Total)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Pago>()
            .Property(p => p.Monto)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Factura>()
            .Property(f => f.Total)
            .HasPrecision(18, 2);

        modelBuilder.Entity<DetallePedido>()
            .Property(d => d.PrecioUnitario)
            .HasPrecision(18, 2);

        modelBuilder.Entity<DetallePedido>()
            .Property(d => d.Subtotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ProductoProveedor>()
            .Property(pp => pp.PrecioCompra)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Envio>()
            .Property(e => e.Costo)
            .HasPrecision(18, 2);

        // =====================================================
        //  EMPRESA
        // =====================================================
        modelBuilder.Entity<Empresa>()
            .HasMany(e => e.Productos)
            .WithOne(p => p.Empresa)
            .HasForeignKey(p => p.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Empresa>()
            .HasMany(e => e.Categorias)
            .WithOne(c => c.Empresa)
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Empresa>()
            .HasMany(e => e.Sucursales)
            .WithOne(s => s.Empresa)
            .HasForeignKey(s => s.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Empresa>()
            .HasMany(e => e.Proveedores)
            .WithOne()
            .HasForeignKey("EmpresaId")
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  CATEGORIA
        // =====================================================
        modelBuilder.Entity<Categoria>()
            .HasOne(c => c.Empresa)
            .WithMany(e => e.Categorias)
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Subcategorias)
            .WithOne(s => s.Categoria)
            .HasForeignKey(s => s.CategoriaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Productos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  SUBCATEGORIA
        // =====================================================
        modelBuilder.Entity<Subcategoria>()
            .HasOne(s => s.Categoria)
            .WithMany(c => c.Subcategorias)
            .HasForeignKey(s => s.CategoriaId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  PRODUCTO
        // =====================================================
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Empresa)
            .WithMany(e => e.Productos)
            .HasForeignKey(p => p.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Producto>()
            .HasMany(p => p.DetallesPedido)
            .WithOne(d => d.Producto)
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  PEDIDO
        // =====================================================
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Pedidos)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Pedido>()
            .HasMany(p => p.DetallesPedido)
            .WithOne(d => d.Pedido)
            .HasForeignKey(d => d.PedidoId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Pagos)
            .WithOne(p => p.Pedido)
            .HasForeignKey(p => p.PedidoId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  PAGO
        // =====================================================
        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Pedido)
            .WithMany(p => p.Pagos)
            .HasForeignKey(p => p.PedidoId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  FACTURA
        // =====================================================
        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Pedido)
            .WithOne()
            .HasForeignKey<Factura>(f => f.PedidoId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  ENVIO
        // =====================================================
        modelBuilder.Entity<Envio>()
            .HasOne(e => e.Pedido)
            .WithOne()
            .HasForeignKey<Envio>(e => e.PedidoId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  STOCK
        // =====================================================
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Producto)
            .WithMany(p => p.Stocks)
            .HasForeignKey(s => s.ProductoId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Sucursal)
            .WithMany(su => su.Stocks)
            .HasForeignKey(s => s.SucursalId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  USUARIO
        // =====================================================
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Empresa)
            .WithMany(e => e.Usuarios)
            .HasForeignKey(u => u.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Reportes)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Notificaciones)
            .WithOne()
            .HasForeignKey("UsuarioId")
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  REPORTE
        // =====================================================
        modelBuilder.Entity<Reporte>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Reportes)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.NoAction);

        // =====================================================
        //  MEDIO CONTACTO
        // =====================================================
        modelBuilder.Entity<MedioContacto>()
            .HasOne(m => m.Empresa)
            .WithMany(e => e.MediosContacto)
            .HasForeignKey(m => m.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}