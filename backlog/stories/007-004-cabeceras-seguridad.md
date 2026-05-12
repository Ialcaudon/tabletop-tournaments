# Story 007-004: Añadir cabeceras de seguridad HTTP a la API

**Descripción:** Configurar cabeceras HTTP de seguridad en la API (HSTS, X-Content-Type-Options, X-Frame-Options, etc.) para proteger contra ataques comunes como clickjacking, MIME sniffing y XSS.  

**Prioridad:** Media  

**Estado:** DRAFT  

**Tasks:**  
- Añadir middleware o configuración para las siguientes cabeceras:  
  - `Strict-Transport-Security` (HSTS).  
  - `X-Content-Type-Options: nosniff`.  
  - `X-Frame-Options: DENY`.  
  - `Referrer-Policy: strict-origin-when-cross-origin`.  
  - `Content-Security-Policy` (si aplica).  
- Verificar que las cabeceras se devuelven correctamente en las respuestas HTTP.  
- Añadir tests para validar la presencia de las cabeceras.  

**Requisitos previos:**  
- Ninguno.  

**Notas:**  
- ASP.NET Core ya incluye HSTS por defecto en producción; verificar que esté activo.  
- Ajustar las políticas según las necesidades del frontend cuando se integre.  

