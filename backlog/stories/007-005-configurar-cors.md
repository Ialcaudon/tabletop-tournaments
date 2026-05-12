# Story 007-005: Configurar CORS de forma restrictiva

**Descripción:** Revisar y ajustar la configuración de CORS (Cross-Origin Resource Sharing) en la API para permitir solo los orígenes autorizados, evitando configuraciones permisivas como `AllowAnyOrigin` en producción.  

**Prioridad:** Media  

**Estado:** DRAFT  

**Tasks:**  
- Auditar la configuración CORS actual en `Startup.cs` o `Program.cs`.  
- Definir una política CORS con orígenes específicos permitidos (configurable por entorno).  
- Mover los orígenes permitidos a la configuración (`appsettings.json` / variables de entorno).  
- Permitir `AllowAnyOrigin` solo en desarrollo si es necesario.  
- Añadir tests para verificar que CORS rechaza orígenes no autorizados.  

**Requisitos previos:**  
- Conocer el dominio del frontend cuando se despliegue (épica 004).  

**Notas:**  
- En desarrollo puede ser más permisivo; en producción debe ser estricto.  
- Coordinar con la épica de frontend (004) para definir los orígenes válidos.  

