# Story 001-001: Configurar SQL Server en contenedor Docker

**Descripción:** Configurar SQL Server en un contenedor Docker para el servicio de base de datos.  

**Prioridad:** Alta  

**Estado:** DONE  

**Tasks:**  
- Elegir imagen de Docker: Usar mcr.microsoft.com/mssql/server:latest para SQL Server.  
- Crear docker-compose.yml: Definir servicio para SQL Server con imagen, puertos (1433:1433), volúmenes para persistencia, y variables de entorno (ACCEPT_EULA=Y, SA_PASSWORD=<strong_password>, MSSQL_PID=Express).  
- Opcional: Crear Dockerfile si se necesita customización adicional.  
- Configurar variables de entorno para SQL Server (e.g., SA_PASSWORD).  
- Probar el contenedor: Ejecutar docker-compose up y verificar conexión con sqlcmd o un cliente SQL.  
- Documentar comandos para iniciar/detener el DB.  

**Requisitos previos:**  
- Instalar Docker y Docker Compose.  
- Asegurar que el puerto 1433 esté disponible.  

**Notas:**  
- Usar una contraseña fuerte para SA_PASSWORD.  
- Para desarrollo, usar SQL Server Express (MSSQL_PID=Express) para evitar licencias.