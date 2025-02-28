using ProductosAPI.DTOs;
using ProductosAPI.Entities;

namespace ProductosAPI.Mappers
{
    public static class ProductoMapper
    {
        public static Producto ToEntity(this ProductoDTO dto)
        {
            return new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock
            };
        }

        public static Producto ToEntity(this ProductoUpdateDTO dto)
        {
            return new Producto
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock
            };
        }

        public static ProductoResponseDTO ToResponseDTO(this Producto entity)
        {
            return new ProductoResponseDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Precio = entity.Precio,
                Stock = entity.Stock,
                FechaCreacion = entity.FechaCreacion
            };
        }
    }
}