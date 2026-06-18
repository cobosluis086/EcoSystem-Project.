# EcoSystem Connect

Proyecto desarrollado en .NET 8 utilizando arquitectura N-Capas.

## Arquitectura del Proyecto

La solución está dividida en capas independientes para mantener el código organizado, escalable y mantenible.

## Capas

### EcoSystem.API

* Capa de presentación.
* Expone endpoints HTTP mediante ASP.NET Core Web API.
* Gestiona solicitudes y respuestas en formato JSON.
* Integra Swagger / OpenAPI para documentar y probar los endpoints.

### EcoSystem.Business

* Capa de lógica de negocio.
* Contiene servicios e interfaces del sistema.
* Se encarga de procesar reglas antes de acceder a los datos.
* Ayuda a separar la lógica del controlador y mantener el proyecto ordenado.

### EcoSystem.Data

* Capa de acceso a datos.
* Contiene entidades y configuración de persistencia.
* Administra la conexión con la base de datos.
* Organiza los modelos utilizados por el sistema.

## Tecnologías Utilizadas

* .NET 8
* ASP.NET Core Web API
* C#
* Swagger / OpenAPI
* Git y GitHub

## Estructura de la Solución

```text
EcoSystem.sln
│
├── EcoSystem.API
├── EcoSystem.Business
└── EcoSystem.Data
```

## Objetivo

Construir la base arquitectónica del proyecto final utilizando el patrón N-Capas, separando responsabilidades entre presentación, lógica de negocio y acceso a datos.

## Autor

Luis Cobos

## Repositorio

EcoSystem-Project.

## Estado del Proyecto

Proyecto en desarrollo como parte de la materia Programación III.

## Notas

La estructura del proyecto permite mantener el código organizado y facilita futuras mejoras.

## Próximos Pasos

- Implementar más servicios en la capa Business.
- Conectar controladores con la lógica de negocio.
- Mejorar la documentación de endpoints.

