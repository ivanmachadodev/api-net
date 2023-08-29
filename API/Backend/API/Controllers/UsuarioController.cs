using APIFinal.Comandos;
using APIFinal.Data;
using APIFinal.Resultados.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly Context _context;

    public UsuarioController(Context context)
    {
        _context = context;
    }

    [HttpGet("obternerUsuario")]
    public async Task<ActionResult<ResultadoUsuario>> GetUsuario()
    {
        var result = new ResultadoUsuario();
        var usuarioAux = await _context.Usuarios.FirstOrDefaultAsync();
        
        if(usuarioAux != null)
        {
            result.NombreUsuario = usuarioAux.NombreUsuario;
            result.StatusCode = 200;
            return Ok(result);
        }
        else
        {
            result.SetError("No se encontro usuario");
            return Ok(result);
        }

    }

    [HttpPost("login")]
    public async Task<ActionResult<ResultadoUsuario>> Login([FromBody] ComandoUsuario comando)
    {
        try
        {
            var result = new ResultadoUsuario();
            var usuario = await _context.Usuarios.Where(c=> c.Activo && c.NombreUsuario.Equals(comando.usuario) && c.Password.Equals(comando.password)).FirstOrDefaultAsync();
            if(usuario != null)
            {
                result.NombreUsuario = usuario.NombreUsuario;
                result.StatusCode = 200;
                return Ok(result);
            }
            else
            {
                result.SetError("Usuario y/o contraseña incorrecta");
                result.StatusCode = 500;
                return Ok(result);
            }
        }
        catch(Exception ex)
        {
            return BadRequest("Error al obtener datos de la base");
        }
    }
}
