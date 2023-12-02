namespace WebApplication1.Models;

public class Lista
{
    public int id { get; set; }
    public int idusuario { get; set; }
    public string nombre { get; set; }
    public string? zona { get; set; }
    public int? duracion { get; set; }
    public string? descripcion { get; set; }
    public virtual Usuario usuario { get; set; }
    public virtual ICollection<Comercio> Comercio { get; set; } = new List<Comercio>();

    public virtual ICollection<Usuario> usuarioSeguidos { get; set; } = new List<Usuario>();
}

