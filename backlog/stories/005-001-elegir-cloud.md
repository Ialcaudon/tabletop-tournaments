# Story 005-001: Elegir proveedores de alojamiento

**Descripción:** Elegir el alojamiento de cada componente de la aplicación de
acuerdo con sus requisitos de ejecución, coste, operación e infraestructura como
código.

**Prioridad:** Alta

**Estado:** IN_PROGRESS

## Situación actual

- La API es una aplicación ASP.NET Core sobre .NET 9 y necesita un proveedor que
  ejecute .NET o contenedores como un servicio web persistente.
- El frontend actual es una aplicación Blazor Web App con interactividad de
  servidor. También necesita un proceso ASP.NET Core persistente y no es un sitio
  estático desplegable directamente en Vercel.
- Supabase PostgreSQL es la plataforma de base de datos aceptada en el
  [ADR 0001](../../docs/adr/0001-usar-supabase-postgresql.md). Su elección no
  determina dónde se alojan la API y el frontend.
- Vercel es la opción preferida para el frontend si este pasa a ser una aplicación
  estática compatible, como Blazor WebAssembly o React. Vercel no proporciona un
  runtime nativo para alojar las aplicaciones .NET actuales.

## Alternativas de despliegue

### Mantener Blazor Interactive Server

Alojar la API y el frontend en uno o dos servicios compatibles con .NET o con
contenedores. Esta alternativa conserva la implementación actual, pero no utiliza
Vercel para ejecutar el frontend.

### Utilizar Vercel para el frontend

Convertir o sustituir el frontend por una aplicación estática y alojarla en
Vercel. La API continuaría en un proveedor compatible con .NET y accedería a
Supabase PostgreSQL. Esta alternativa mantiene Vercel como plataforma de frontend,
pero requiere un cambio de arquitectura y trabajo funcional adicional.

## Criterios de evaluación

- Compatibilidad con .NET 9, contenedores y procesos persistentes.
- Coste total y comportamiento de los planes de entrada.
- Región disponible y latencia respecto a Supabase y los usuarios.
- Conectividad IPv4 e IPv6 hacia Supabase.
- Gestión de secretos, dominios, observabilidad y copias de seguridad.
- Soporte para infraestructura como código y despliegues automatizados desde
  `main`.
- Facilidad de operación para un único mantenedor.

## Tareas

- [x] Inventariar los requisitos de ejecución de API, frontend y base de datos.
- [x] Comprobar la compatibilidad del frontend y la API actuales con Vercel.
- [ ] Decidir si se mantiene Blazor Interactive Server o se adopta un frontend
  estático para utilizar Vercel.
- [ ] Comparar proveedores compatibles con .NET para la API usando los criterios
  definidos.
- [ ] Estimar el coste de la alternativa seleccionada.
- [ ] Documentar la decisión de alojamiento en un ADR independiente.
- [ ] Actualizar las historias 005-002, 005-003 y 005-004 con los proveedores y
  flujos seleccionados.

## Referencias

- [Vercel: runtimes de Functions](https://vercel.com/docs/functions/runtimes)
- [Supabase: conexión a PostgreSQL](https://supabase.com/docs/guides/database/connecting-to-postgres)
