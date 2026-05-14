# Story 002-002: Endpoint para leer datos de usuario

**Descripción:** Implementar endpoint GET /api/players/{id} para leer datos de un usuario.  

**Prioridad:** Alta  

**Estado:** DONE  

**Tasks:**  
- [x] Extender IPlayerService con método GetPlayerByIdAsync.  
- [x] Implementar GetPlayerByIdAsync en PlayerService.  
- [x] Extender PlayersController con método GET {id}.  
- [x] Retornar NotFound cuando el jugador no existe.  
- [x] Corregir CreatedAtAction del POST para apuntar a GetById.  
- [x] Añadir tests unitarios para GetPlayerByIdAsync.
