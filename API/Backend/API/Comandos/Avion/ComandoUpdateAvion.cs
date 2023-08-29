namespace APIFinal.Comandos.Avion;

public class ComandoUpdateAvion
{
    public Guid ID {get; set;}
    public string Matricula {get; set;}
    public string Modelo {get; set;}
    public string Fabricante {get; set;}
    public int CantidadPasajeros {get; set;}
    public int AutonomiaKm {get; set;}
    public DateTime FechaAlta {get; set;}
}
