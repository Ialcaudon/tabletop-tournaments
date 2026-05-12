# Story 007-001: Gestión segura de secretos y contraseñas

**Descripción:** Eliminar secretos hardcodeados del código fuente y configurar una gestión segura de credenciales usando User Secrets en desarrollo y variables de entorno o un vault en producción.  

**Prioridad:** Alta  

**Estado:** DRAFT  

**Tasks:**  
- Auditar el código fuente en busca de secretos hardcodeados (connection strings, SA_PASSWORD, API keys, etc.).  
- Configurar .NET User Secrets (`dotnet user-secrets`) para el proyecto API en entorno de desarrollo.  
- Mover la connection string de SQL Server a User Secrets y/o variables de entorno.  
- Mover SA_PASSWORD del docker-compose.yml a un archivo `.env` excluido del repositorio.  
- Añadir `.env` al `.gitignore` si no está ya incluido.  
- Crear un archivo `.env.example` con las variables requeridas (sin valores reales) como referencia.  
- Documentar el proceso de configuración de secretos en el README o en docs/.  

**Requisitos previos:**  
- Ninguno.  

**Notas:**  
- Nunca commitear secretos reales al repositorio.  
- Considerar Azure Key Vault o similar para producción en el futuro.  

