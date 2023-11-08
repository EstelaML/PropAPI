namespace WebApplication1.Models;

public class Lista
{
    public int id { get; set; }
    public Usuario usuario { get; set; }
    public int idusuario { get; set; }
    public virtual ICollection<Comercio> Comercio { get; set; } = new List<Comercio>();
}

