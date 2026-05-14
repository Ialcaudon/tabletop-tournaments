# Story 002-008: Endpoint para modificar torneo

**Descripción:** Implementar endpoint PUT /api/tournaments/{id} para modificar un torneo.  

**Prioridad:** Alta  

**Estado:** DONE  

**Tasks:**  
- [x] Añadir método Update en la entidad Tournament.  
- [x] Extender ITournamentService con método UpdateTournamentAsync.  
- [x] Implementar UpdateTournamentAsync en TournamentService.  
- [x] Extender TournamentsController con método PUT.  
- [x] Validar entrada y retornar NotFound/BadRequest según corresponda.  
- [x] Añadir tests unitarios para UpdateTournamentAsync.
