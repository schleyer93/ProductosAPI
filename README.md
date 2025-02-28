# Productos API

Esta es una API RESTful para gestionar productos. Proporciona operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para productos, con validaciones de datos y documentación mediante Swagger.

## Tabla de Contenidos

1. [Requisitos](#requisitos)
2. [Configuración](#configuración)
3. [Ejecución](#ejecución)
4. [Endpoints](#endpoints)
5. [Ejemplos de Uso](#ejemplos-de-uso)
6. [Validaciones](#validaciones)
7. [Documentación con Swagger](#documentación-con-swagger)
8. [Estructura del Proyecto](#estructura-del-proyecto)
9. [Contribución](#contribución)
10. [Licencia](#licencia)

---

## Requisitos

- .NET 8.0 SDK
- SQL Server (o SQL Server LocalDB)
- Visual Studio 2022 (opcional, pero recomendado)

## Configuración

Clona el repositorio:

```bash
git clone https://github.com/tu-usuario/productos-api.git
cd productos-api
```

Configura la base de datos:

1. Asegúrate de tener SQL Server instalado y en ejecución.
2. Actualiza la cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(local);Database=WebAPIProductosDB;Integrated Security=True;TrustServerCertificate=True"
}
```

Ejecuta las migraciones para crear la base de datos:

```bash
dotnet ef database update
```

## Ejecución

Ejecuta la aplicación:

```bash
dotnet run
```

La API estará disponible en los puertos predeterminados ASP.NET Core:

```
https://localhost:5001 (o http://localhost:5000) 
```

Accede a la documentación de la API en:

```
https://localhost:5001/swagger
```

## Endpoints

La API proporciona los siguientes endpoints:

| Método | Ruta                | Descripción                      |
| ------ | ------------------- | -------------------------------- |
| GET    | /api/productos      | Obtiene todos los productos.     |
| GET    | /api/productos/{id} | Obtiene un producto por su ID.   |
| POST   | /api/productos      | Crea un nuevo producto.          |
| PUT    | /api/productos/{id} | Actualiza un producto existente. |
| DELETE | /api/productos/{id} | Elimina un producto.             |

## Ejemplos de Uso

### 1. Obtener todos los productos

#### Request:

```bash
GET /api/productos
```

#### Response:

```json
[
  {
    "id": 1,
    "nombre": "Laptop",
    "descripcion": "Laptop de última generación",
    "precio": 1200.00,
    "stock": 10,
    "fechaCreacion": "2023-10-01T12:00:00Z"
  }
]
```

### 2. Crear un nuevo producto

#### Request:

```bash
POST /api/productos
```

#### JSON:

```json
{
  "nombre": "Smartphone",
  "descripcion": "Teléfono inteligente",
  "precio": 500.00,
  "stock": 20
}
```

#### Response:

```json
{
  "id": 2,
  "nombre": "Smartphone",
  "descripcion": "Teléfono inteligente",
  "precio": 500.00,
  "stock": 20,
  "fechaCreacion": "2023-10-01T12:05:00Z"
}
```

### 3. Actualizar un producto

#### Request:

```bash
PUT /api/productos/2
```

#### JSON:

```json
{
  "nombre": "Smartphone Pro",
  "descripcion": "Teléfono inteligente avanzado",
  "precio": 600.00,
  "stock": 15
}
```

#### Response:

```http
HTTP 204 No Content
```

### 4. Eliminar un producto

#### Request:

```bash
DELETE /api/productos/2
```

#### Response:

```http
HTTP 204 No Content
```

## Validaciones

La API valida los datos de entrada utilizando atributos de validación de ASP.NET Core. Algunas validaciones incluyen:

- **Nombre**: Requerido, mínimo 3 caracteres, máximo 100 caracteres.
- **Precio**: Requerido, debe ser mayor a 0.
- **Stock**: Requerido, no puede ser negativo.

Si la validación falla, la API devuelve un error `400 Bad Request` con detalles.

## Documentación con Swagger

La API está documentada utilizando Swagger. Puedes acceder a la documentación en:

```
https://localhost:5001/swagger
```

## Estructura del Proyecto

```bash
ProductosAPI/
├── Controllers/           # Controladores de la API
├── Data/                  # Configuración de la base de datos (DbContext)
├── DTOs/                  # Objetos de transferencia de datos (DTOs)
├── Entities/              # Entidades de la base de datos
├── Mappers/               # Mapeadores entre entidades y DTOs
├── appsettings.json       # Configuración de la aplicación
├── Program.cs             # Punto de entrada de la aplicación
└── README.md              # Este archivo
```

##
