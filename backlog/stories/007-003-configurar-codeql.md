# Story 007-003: Configurar análisis de seguridad en CI (CodeQL / Security Scanning)

**Descripción:** Integrar un análisis estático de seguridad (SAST) en el pipeline de CI mediante GitHub CodeQL u otra herramienta equivalente para detectar vulnerabilidades en el código fuente.  

**Prioridad:** Media  

**Estado:** DRAFT  

**Tasks:**  
- Crear un workflow de GitHub Actions para CodeQL (`.github/workflows/codeql.yml`).  
- Configurar el análisis para el lenguaje `csharp`.  
- Programar ejecución en cada push a `main` y en pull requests.  
- Programar un escaneo semanal programado como red de seguridad.  
- Verificar que los resultados aparezcan en la pestaña Security > Code scanning del repositorio.  

**Requisitos previos:**  
- Repositorio alojado en GitHub con Actions habilitadas.  

**Notas:**  
- CodeQL es gratuito para repositorios públicos.  
- Revisar y triagear los hallazgos para evitar falsos positivos.  

