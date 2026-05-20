# EcoSystem Connect

Proyecto desarrollado en .NET 8 utilizando arquitectura N-Capas.

## Arquitectura del Proyecto

La solución está dividida en capas independientes para mantener el código organizado, escalable y mantenible.

### Capas

- **EcoSystem.API**
  - Capa de presentación
  - Expone endpoints HTTP mediante ASP.NET Core Web API
  - Gestiona solicitudes y respuestas JSON

- **EcoSystem.Data**
  - Capa de acceso a datos
  - Contiene entidades y configuración de persistencia

## Tecnologías Utilizadas

- .NET 8
- ASP.NET Core Web API
- C#
- Swagger / OpenAPI

## Estructura de la Solución

```text
EcoSystem.sln
│
├── EcoSystem.API
└── EcoSystem.Data
```

## Objetivo

Construir la base arquitectónica del proyecto final utilizando el patrón N-Capas.

## Autor

Luis Cobos