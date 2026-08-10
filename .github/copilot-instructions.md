# Instrucciones de desarrollo para Tabletop Tournaments

La fuente principal de instrucciones para agentes y asistentes es `AGENTS.md`.
Lee también `docs/architecture.md`, los ADR aplicables y la historia relevante
del backlog antes de implementar cambios.

## Convenciones principales

- Documentación, backlog y textos funcionales en español; código e identificadores
  en inglés.
- Nullable e implicit usings habilitados.
- Entidades con setters privados, constructor protegido para EF Core y validación
  en constructores o métodos de dominio.
- Interfaces de repositorio en Core e implementaciones en Infrastructure.
- Servicios de aplicación registrados según su ciclo de vida y repositorios como
  scoped.
- Comentarios sólo cuando aporten contexto que el código no pueda expresar.
- Pruebas unitarias con xUnit, Moq y FluentAssertions.
- Pruebas de integración contra el motor real y aplicando migraciones versionadas.

## Entrega

- Desarrollo basado en tronco sobre `main`.
- Sin ramas ni pull requests por defecto.
- Commits pequeños, coherentes e integrables.
- Pruebas y feature toggles para controlar el riesgo de cambios graduales.
- Actualización del backlog y la documentación como parte del cambio que los
  afecte.
