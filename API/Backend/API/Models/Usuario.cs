namespace APIFinal.Models;

public class Usuario
{
    public Guid Id {get; set;}
    public string NombreUsuario {get; set;}
    public string Password {get; set;}
    public bool Activo {get; set;}
    public DateTime FechaALta {get; set;}

}
