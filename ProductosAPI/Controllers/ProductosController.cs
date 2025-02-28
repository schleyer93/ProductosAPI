using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Data;
using ProductosAPI.DTOs;
using ProductosAPI.Entities;
using ProductosAPI.Mappers;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ApplicationDbContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        
        [HttpGet] // api/productos
        public async Task<ActionResult<IEnumerable<ProductoResponseDTO>>> GetProductos()
        {
            _logger.LogInformation("Obteniendo todos los productos");
            var productos = await _context.Productos.ToListAsync();
            return productos.Select(p => p.ToResponseDTO()).ToList();
        }

        
        [HttpGet("{id}")] // api/productos/{id}
        public async Task<ActionResult<ProductoResponseDTO>> GetProducto(int id)
        {
            _logger.LogInformation("Obteniendo producto con ID {Id}", id);

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                _logger.LogWarning("Producto con ID {Id} no encontrado", id);
                return NotFound();
            }

            return producto.ToResponseDTO();
        }

        
        [HttpPost] // api/productos
        public async Task<ActionResult<ProductoResponseDTO>> PostProducto(ProductoDTO productoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creando nuevo producto: {NombreProducto}", productoDTO.Nombre);

            var producto = productoDTO.ToEntity();
            producto.FechaCreacion = DateTime.Now;

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProducto),
                new { id = producto.Id },
                producto.ToResponseDTO());
        }

        
        [HttpPut("{id}")] // api/productos/{id}
        public async Task<IActionResult> PutProducto(int id, ProductoDTO productoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // para actualizar propiedades preservando la fecha de creación original
            producto.Nombre = productoDTO.Nombre;
            producto.Descripcion = productoDTO.Descripcion;
            producto.Precio = productoDTO.Precio;
            producto.Stock = productoDTO.Stock;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpDelete("{id}")] // api/productos/{id}
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}