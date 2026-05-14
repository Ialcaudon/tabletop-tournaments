# Story 002-004: Endpoint para leer datos del torneo

**Descripción:** Implementar endpoint GET /api/tournaments/{id} para leer datos de un torneo.  

**Prioridad:** Alta  

**Estado:** DONE  

**Tasks:**  
- [x] Extender ITournamentService con método GetTournamentByIdAsync.  
- [x] Implementar GetTournamentByIdAsync en TournamentService.  
- [x] Extender TournamentsController con método GET {id}.  
- [x] Retornar NotFound cuando el torneo no existe.  
- [x] Corregir CreatedAtAction del POST para apuntar a GetById.  
- [x] Añadir tests unitarios para GetTournamentByIdAsync.
