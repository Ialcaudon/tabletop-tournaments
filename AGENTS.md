# Instrucciones para agentes

## Contexto del proyecto

Tabletop Tournaments es un proyecto personal mantenido por un único
desarrollador. Utiliza .NET 9, ASP.NET Core, Blazor, Entity Framework Core y una
arquitectura de dominio simplificada.

Antes de realizar cambios, consulta:

- `docs/architecture.md` para entender la arquitectura vigente y la objetivo.
- `docs/adr/` para conocer las decisiones arquitectónicas aceptadas.
- `backlog/stories/` para localizar la historia relacionada con la tarea.

## Idioma

- Escribe en español la documentación, el backlog, los ADR, los mensajes para el
  usuario y los textos funcionales de la interfaz.
- Mantén en inglés el código, los identificadores, nombres de tipos y métodos,
  nombres de archivos de código y mensajes de commit.
- Conserva en su idioma original los nombres oficiales de productos, paquetes,
  protocolos y comandos cuando traducirlos reduzca la precisión.

## Flujo de trabajo Git

El proyecto utiliza desarrollo basado en tronco y `main` es la rama de trabajo.

- Trabaja directamente sobre `main`.
- No crees ramas, pull requests ni worktrees adicionales salvo que el usuario lo
  solicite expresamente o exista una restricción técnica que lo haga obligatorio.
- Si el entorno ya proporciona una rama o un worktree aislado, úsalo sin crear
  otro y comunica esa limitación al usuario.
- Divide el trabajo en commits pequeños, coherentes, compilables y fáciles de
  revertir.
- No mezcles cambios funcionales, refactors y limpieza no relacionada en el mismo
  commit.
- No hagas push, merges, rebases ni cambios de historial sin una petición
  explícita del usuario.
- No propongas una pull request como paso normal de entrega.

## Estrategia de entrega

- Protege funcionalidades incompletas o despliegues graduales mediante feature
  toggles en lugar de ramas de larga duración.
- Centraliza los toggles en configuración tipada; no repartas comprobaciones de
  cadenas literales por el código.
- Los toggles nuevos deben tener un valor predeterminado seguro, normalmente
  desactivado.
- Prueba el comportamiento relevante con el toggle activado y desactivado.
- Documenta el propósito, propietario y condición de retirada de cada toggle.
- Elimina toggles y rutas antiguas cuando finalice su despliegue; no deben
  convertirse en configuración permanente por accidente.

## Calidad y pruebas

- Añade o actualiza pruebas para cualquier cambio de comportamiento.
- Ejecuta primero las pruebas más cercanas al cambio y después la validación más
  amplia que resulte segura y relevante.
- Antes de dar por terminado un cambio de código, ejecuta como mínimo
  `dotnet build` y las suites afectadas. Si no es posible, explica el motivo.
- No ocultes pruebas fallidas ni rebajes aserciones sólo para hacerlas pasar.
- Las pruebas de persistencia deben aplicar las migraciones versionadas reales;
  no deben sustituirlas por `EnsureCreated`.
- Mantén los commits integrables en `main`: no dejes deliberadamente el proyecto
  sin compilar.

## Arquitectura y código

- Respeta las dependencias `Core <- Application` y `Core <- Infrastructure`; la
  API actúa como raíz de composición y Blazor consume la API por HTTP.
- Mantén las interfaces de repositorio en Core y sus implementaciones en
  Infrastructure.
- Usa nombres descriptivos y convenciones estándar de C#.
- Añade comentarios sólo cuando expliquen una decisión que el código no pueda
  expresar por sí mismo.
- Evita abstracciones, infraestructura o patrones pensados para una escala que el
  proyecto todavía no necesita.

## Base de datos

- Sigue el ADR vigente para la transición de SQL Server a Supabase PostgreSQL.
- Mantén una sola fuente de verdad para el esquema en `supabase/migrations`.
- No guardes credenciales, cadenas de conexión reales ni secretos en el
  repositorio.
- Verifica las migraciones en una base vacía antes de considerarlas terminadas.

## Backlog y documentación

- Actualiza la historia correspondiente cuando completes una tarea.
- Marca con `[x]` únicamente las tareas realmente terminadas.
- Cambia el estado de una historia a `DONE` sólo cuando no quede trabajo requerido.
- Actualiza la documentación en el mismo commit cuando un cambio altere comandos,
  configuración o arquitectura.
