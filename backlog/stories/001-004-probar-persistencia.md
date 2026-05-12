# Story 001-004: Probar persistencia con integration tests

**Descripción:** Probar la persistencia ampliando los integration tests para verificar guardado y lectura contra SQL Server real (Testcontainers).  

**Prioridad:** Alta  

**Estado:** DONE  

**Tasks:**  
- [x] Añadir `GetByIdAsync(int id)` y `GetAllAsync()` a `IPlayerRepository` + `PlayerRepository`.  
- [x] Añadir `GetByIdAsync(int id)` a `ITournamentRepository` + `TournamentRepository`.  
- [x] Refactorizar `IntegrationTestFixture` para aislar tests con DbContext fresco por test.  
- [x] Ampliar `PlayerRepositoryTests`: tests de AddAsync, GetByIdAsync, GetAllAsync.  
- [x] Ampliar `TournamentRepositoryTests`: tests de AddAsync, GetByIdAsync, GetAllAsync.  
- [x] Actualizar repos InMemory para implementar nuevos métodos de interfaz.  
- [x] Ejecutar `dotnet test` para validar (16/16 tests OK).  

**Requisitos previos:**  
- Story 001-003 (Migraciones iniciales) completada.  
- Docker disponible para Testcontainers.
