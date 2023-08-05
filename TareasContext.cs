using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options): base(options){
        
    }

    protected override  void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriaInit = new List<Categoria>();
        categoriaInit.Add(new Categoria() { CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"), Nombre = "Pending activities", Peso = 20});
        categoriaInit.Add(new Categoria() { CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"), Nombre = "Personal activities", Peso = 50});

        modelBuilder.Entity<Categoria>(categoria =>{
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion).IsRequired(false);
            categoria.Property(p => p.Peso);
            categoria.HasData(categoriaInit);
            
        });

        List<Tarea> tareaInit = new List<Tarea>();
        tareaInit.Add(new Tarea() {TareaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c100"), 
                                        CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c657"),
                                        PrioridadTarea = Prioridad.Media,
                                        Titulo = "Payment of public services",
                                        FechaCreacion= DateTime.Now});

        tareaInit.Add(new Tarea() {TareaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c101"), 
                                        CategoriaId = Guid.Parse("c4e0d0e7-5f06-48c7-9246-11fe12f2c602"),
                                        PrioridadTarea = Prioridad.Baja,
                                        Titulo = "Finish watching movie",
                                        FechaCreacion = DateTime.Now});  

            modelBuilder.Entity<Tarea>(tarea =>{
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            tarea.HasOne(p=>p.Categoria).WithMany(p=> p.Tareas).HasForeignKey(p=> p.CategoriaId);
            tarea.Property(p=> p.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(p=> p.Descripcion).IsRequired(false);
            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.FechaCreacion);
            tarea.Ignore(p => p.Resumen);
            tarea.HasData(tareaInit);
        });
    }

}