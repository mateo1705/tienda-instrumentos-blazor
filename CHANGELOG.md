# Changelog

Todos los cambios notables de este proyecto se documentan en este archivo.
El formato está basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.1.0/)
y el proyecto sigue [Versionado Semántico](https://semver.org/lang/es/).

## [Unreleased]

### Added
- README profesional con diagrama de arquitectura
- Plantillas de issues y pull requests
- Comentarios XML en `IProductoService`

## [0.3.0] - AAAA-MM-DD

### Added
- Persistencia con Entity Framework Core y SQLite (`IDbContextFactory`)
- Entidades `ProductoEntity` y `Categoria` con relación uno-a-muchos
- Migración inicial y datos de ejemplo (5 categorías y 10 productos)
- `IProductoService` con CRUD, búsqueda y paginación
- Componentes reutilizables `AlertError` y `Paginacion`

### Changed
- Catálogo, detalle, administración y encabezado leen de la base de datos
- Estadísticas del inventario calculadas desde la base

### Removed
- `ProductoState` (lista en memoria)

## [0.2.0] - AAAA-MM-DD

### Added
- HTML5 semántico y enlace "Saltar al contenido"
- Variables CSS globales y CSS Isolation en `ProductoCard`, `ConfirmarModal` y `ProductoDetalle`
- Modo oscuro con persistencia en `localStorage` mediante JS Interop
- Botón "Copiar enlace" y notificación del navegador por stock bajo
- Página de detalle de producto

## [0.1.0] - AAAA-MM-DD

### Added
- Catálogo de productos con componente `ProductoCard`
- Carrito de compras con `CarritoState`
- Formularios de alta y edición con validación (Data Annotations)
- Modal de confirmación para eliminar productos
- Navegación con rutas y parámetros
