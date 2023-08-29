using System.Runtime.Intrinsics.X86;
using APIFinal.Data;
using APIFinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using APIFinal.Comandos;
using APIFinal.Resultados.Aviones;
using APIFinal.Resultados;
using APIFinal.Comandos.Avion;

namespace APIFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvionController : ControllerBase
{
    private readonly Context _context;

    public AvionController(Context context)
    {
        _context = context;
    }

    [HttpGet("obternerAviones")]
    public async Task<ActionResult<List<Avion>>> GetAviones()
    {
        var listaAviones = new List<Avion>();

        listaAviones = await _context.Aviones.ToListAsync();

        return Ok(listaAviones);

    }

    [HttpPost("altaAvion")]
    public async Task<ActionResult<ResultadoAvion>> PostAvion([FromBody] ComandoAvion comando)
    {
        var result = new ResultadoAvion();

        if(comando.Matricula == "" || comando.Modelo == "" || comando.Fabricante == "" || comando.CantidadPasajeros <= 0
            || comando.AutonomiaKm <= 0)
        {
            result.SetError("Error al guardar avion. No puede haber campos nulos.");
            return Ok(result);
        }
        else
        {
            var avion = new Avion{
            ID = new Guid(),
            Matricula = comando.Matricula,
            Modelo = comando.Modelo,
            Fabricante = comando.Fabricante,
            CantidadPasajeros = comando.CantidadPasajeros,
            AutonomiaKm = comando.AutonomiaKm,
            FechaAlta = comando.FechaAlta
            };

            _context.Aviones.Add(avion);
            await _context.SaveChangesAsync();
            return Ok(avion);
        }              

    }

    [HttpPut("modificarAvion")]
    public async Task<ActionResult<ResultadoBase>> UpdateAvion([FromBody] ComandoUpdateAvion comando)
    {
        var result = new ResultadoBase();
        if(comando.ID.Equals(""))
        {
            result.SetError("No se puede editar. El campo id no puede ser nulo");
            result.StatusCode = 500;
        }

        var avion = await _context.Aviones.Where(c=> c.ID.Equals(comando.ID)).FirstOrDefaultAsync();
        if(avion != null)
        {
            avion.Matricula = comando.Matricula;
            avion.Modelo = comando.Modelo;
            avion.Fabricante = comando.Fabricante;
            avion.CantidadPasajeros = comando.CantidadPasajeros;
            avion.AutonomiaKm = comando.AutonomiaKm;
            avion.FechaAlta = comando.FechaAlta;

            _context.Update(avion);
            await _context.SaveChangesAsync();
        }
        else
        {
            result.SetError("Avion no encontrado");
            result.StatusCode = 500;
        }

        return result;
    }
}
