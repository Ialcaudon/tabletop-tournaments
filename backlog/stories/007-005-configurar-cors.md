# Story 007-005: Configurar CORS de forma restrictiva

**Descripción:** Revisar y ajustar la configuración de CORS (Cross-Origin Resource Sharing) en la API para permitir solo los orígenes autorizados, evitando configuraciones permisivas como `AllowAnyOrigin` en producción.  

**Prioridad:** Media  

**Estado:** DONE  

**Tasks:**  
- [x] Auditar la configuración CORS actual en `Startup.cs` o `Program.cs`.  
- [x] Definir una política CORS con orígenes específicos permitidos (configurable por entorno).  
- [x] Mover los orígenes permitidos a la configuración (`appsettings.json` / variables de entorno).  
- [x] Permitir `AllowAnyOrigin` solo en desarrollo si es necesario.  
- [ ] Añadir tests para verificar que CORS rechaza orígenes no autorizados.  

**Requisitos previos:**  
- Conocer el dominio del frontend cuando se despliegue (épica 004).  

**Notas:**  
- En desarrollo permite `http://localhost:5173` (Vite default).
- En producción se configuran orígenes vía `Cors:AllowedOrigins` en appsettings o variables de entorno.
- Tests de CORS se pospondrán a post-demo (requieren integration tests con servidor real).  

