namespace proyectoef.Models;
S
public class Categoria
{
    public Guid CategoriaId {get;set;}
    public string Nombre { get; set; }
    public string Descripcion { get; set; } 
    public virtual ICollection<Tareas> Tareas { get; set; }

}