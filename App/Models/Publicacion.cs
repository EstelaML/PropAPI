
namespace WebApplication1.Models;

public partial class Publicacion
{
    public int id { get; set; }
    public int usuario { get; set; }
    public int comercio { get; set; }
    public string titulo { get; set; }
    public string descripcion { get; set; }
    public DateTime fecha { get; set; }
    public string nombreimagen { get; set; }
    public virtual Comercio comercioObject { get; set; }
    public virtual Usuario usuarioObject { get; set; }

}
