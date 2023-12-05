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

    public override bool Equals(object? obj)
    {
        bool r = obj is Lista
            && ((Lista)obj).idusuario == this.idusuario
            && ((Lista)obj).nombre.Equals(this.nombre)
            && (this?.zona == null || ((Lista)obj).zona != null && ((Lista)obj).zona.Equals(this.zona))
            && (this?.duracion == null || ((Lista)obj).duracion == this.duracion)
            && (this?.descripcion == null || ((Lista)obj).descripcion != null && ((Lista)obj).descripcion.Equals(this.descripcion));

        return r;

    }
}

