﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication1.Models;

public partial class Anuncio
{
    public int id { get; set; }
    public int idcomercio { get; set; }
    public DateTime fecha { get; set; }
    public string titulo { get; set; }
    public string descripcion { get; set; }
    public string imagenes { get; set; }
    public string tipo { get; set; }
    public DateTime? fechaIni { get; set; }
    public DateTime?     fechaFin {  get; set; }
    public Comercio? Comercio { get; set; }
}