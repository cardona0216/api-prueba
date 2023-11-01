﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using ApiPruebas.Models;

using Microsoft.AspNetCore.Cors;

namespace ApiPruebas.Controllers
{
    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        public readonly DbapiContext _dbcontext;

        public ProductoController(DbapiContext _context) {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("lista")]
        public IActionResult Get() {
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbcontext.Productos.Include(c => c.oCategoria).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {
            Producto oProducto = _dbcontext.Productos.Find(idProducto);
            if (oProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oProducto = _dbcontext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();
                

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oProducto });
            }
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult Post([FromBody] Producto objeto)
        {
            try
            {
                _dbcontext.Productos.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }

        [HttpPut]
        [Route("editar")]
        public IActionResult Put([FromBody] Producto objeto)
        {
            Producto oProducto = _dbcontext.Productos.Find(objeto.IdProducto);
            if (oProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                oProducto.CodigoBarra = objeto.CodigoBarra is null ? oProducto.CodigoBarra : objeto.CodigoBarra;
                oProducto.Descripcion = objeto.Descripcion is null ? oProducto.Descripcion : objeto.Descripcion;
                oProducto.Marca = objeto.Marca is null ? oProducto.Marca : objeto.Marca;
                oProducto.IdCategoria = objeto.IdCategoria is null ? oProducto.IdCategoria : objeto.IdCategoria;
                oProducto.Precio = objeto.Precio is null ? oProducto.Precio : objeto.Precio;

                _dbcontext.Productos.Update(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }


        [HttpDelete]
        [Route("eliminar/{idProducto:int}")]
        public  IActionResult Eliminar(int idProducto)
        {
            Producto oProducto = _dbcontext.Productos.Find(idProducto);
            if (oProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                
                _dbcontext.Productos.Remove(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

    }
}