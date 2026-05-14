# 🎯 POC Alpha Demo — Plan de 2 Semanas

## Objetivo
Presentar una mini demo funcional a una tienda de juegos de mesa para convencer al dueño de colaborar en el desarrollo de la app (feedback + compromiso de uso).

## Fecha límite: 28 de mayo de 2026

---

## 📊 Progreso Actual (actualizado: 14 mayo 2026)

| Completado | Pendiente | Total |
|-----------|-----------|-------|
| 7/15 | 8/15 | 15 stories |

### Resumen rápido
- ✅ CRUD básico de torneos (crear, leer, listar) — funcionando
- ✅ CRUD básico de jugadores (crear, leer) — funcionando
- ✅ Añadir jugador a torneo — funcionando
- ❌ Modificar torneo (PUT) — pendiente
- ❌ Eliminar torneo (DELETE) — pendiente
- ❌ Eliminar jugador de torneo (DELETE) — pendiente
- ❌ CORS — pendiente
- ❌ Frontend React — no iniciado
- ❌ Seed data — no iniciado

---

## 🏗️ Alcance de la Demo

### ✅ QUÉ INCLUIR
1. **API funcional** con CRUD completo de torneos y gestión de jugadores
2. **Frontend React** con diseño atractivo y responsive (tablet/móvil)
3. **Seed data** con torneos y jugadores de ejemplo (Warhammer, Magic, genérico)
4. **CORS** configurado para desarrollo local
5. **Docker Compose** para levantar todo con un solo comando

### ❌ QUÉ NO INCLUIR (se hará después)
- Autenticación / OAuth
- Despliegue en cloud
- CI/CD
- Seguridad avanzada (headers, CodeQL, Dependabot)

---

## 📋 Stories Priorizadas (en orden de ejecución)

### Semana 1: Backend Completo + Inicializar Frontend

| # | Story | Prioridad | Estado |
|---|-------|-----------|--------|
| 1 | 002-003: Endpoint crear torneo | 🔴 Crítica | ✅ DONE |
| 2 | 002-004: Endpoint leer torneo (detalle + listado) | 🔴 Crítica | ✅ DONE |
| 3 | 002-008: Endpoint modificar torneo | 🟡 Alta | ✅ DONE |
| 4 | 002-007: Endpoint eliminar torneo | 🟡 Alta | ⬜ PENDIENTE |
| 5 | 002-001: Endpoint crear usuario/jugador | 🔴 Crítica | ✅ DONE |
| 6 | 002-002: Endpoint leer usuario/jugador | 🔴 Crítica | ✅ DONE |
| 7 | 002-005: Endpoint añadir jugador a torneo | 🔴 Crítica | ✅ DONE |
| 8 | 002-006: Endpoint eliminar jugador de torneo | 🟡 Alta | ⬜ PENDIENTE |
| 9 | 007-005: Configurar CORS (mínimo) | 🟡 Alta | ⬜ PENDIENTE |
| 10 | 004-001: Inicializar proyecto React | 🔴 Crítica | ⬜ PENDIENTE |

### Semana 2: Frontend Visual + Seed Data + Pulido

| # | Story | Prioridad | Estado |
|---|-------|-----------|--------|
| 11 | 004-004: Interfaz gestión de torneos | 🔴 Crítica | ⬜ PENDIENTE |
| 12 | 004-003: Interfaz gestión de jugadores | 🟡 Alta | ⬜ PENDIENTE |
| 13 | 004-005: Integrar llamadas API | 🔴 Crítica | ⬜ PENDIENTE |
| 14 | NUEVO: Seed data con datos de ejemplo | 🔴 Crítica | ⬜ PENDIENTE |
| 15 | NUEVO: Landing/Home page atractiva | 🟡 Alta | ⬜ PENDIENTE |

### Stories DESCARTADAS para esta fase
- 003-001, 003-002, 003-003 (Auth/OAuth) → Post-demo
- 004-002 (Componentes login) → Post-demo
- 005-xxx (Infraestructura cloud) → Post-demo
- 007-001 a 007-004 (Seguridad) → Post-demo

---

## 🎨 Visión de la Demo

### Pantallas del Frontend
1. **Landing Page**: Nombre de la app, tagline ("Organiza torneos de mesa como un pro"), CTA
2. **Lista de Torneos**: Cards con nombre, fecha, sistema de juego, nº jugadores
3. **Detalle de Torneo**: Info completa, lista de jugadores inscritos, botón "Inscribir jugador"
4. **Crear Torneo**: Formulario con nombre, fecha, sistema de juego (Warhammer AoS, 40K, Magic, etc.)

### Seed Data sugerida
- Torneo "Gran Batalla AoS - Primavera 2026" (Warhammer AoS, 8 jugadores)
- Torneo "Liga Magic: Draft Semanal" (Magic, 12 jugadores)
- Torneo "Warhammer 40K: Escalada 500pts" (Warhammer 40K, 6 jugadores)

---

## 🔧 Decisiones Técnicas para la POC

1. **Relación Tournament ↔ Player**: Crear tabla intermedia `TournamentPlayer` (many-to-many)
2. **Frontend**: React + TypeScript + Vite + TailwindCSS (rápido y visual)
3. **CORS**: Permitir `localhost:5173` (default Vite) en desarrollo
4. **Seed Data**: Clase `DbInitializer` que se ejecuta en Development

---

## 📦 Estrategia de Demo — Opciones de Entrega

### ❌ Opción descartada: Solo local
Llevar un portátil con Docker + API + frontend corriendo localmente NO es viable para una demo en tienda (depende de WiFi, Docker consumiendo recursos, poco profesional).

### ✅ Opción recomendada: Despliegue gratuito mínimo

| Componente | Plataforma | Coste | Esfuerzo |
|-----------|-----------|-------|----------|
| **Frontend** | Vercel o Netlify | Gratis | 5 min (conectar repo GitHub) |
| **API (.NET)** | Railway o Render | Gratis (free tier) | 30 min |
| **Base de datos** | Railway PostgreSQL o Render PostgreSQL | Gratis (free tier) | 15 min |

> ⚠️ Nota: Los free tiers de Railway/Render usan PostgreSQL, no SQL Server. Para la POC esto implica cambiar el provider de EF Core a `Npgsql` con config condicional (SQL Server local, PostgreSQL en producción) O usar directamente PostgreSQL también en local via Docker.

**Alternativa más simple (sin cambiar DB provider):**

| Componente | Plataforma | Coste | Esfuerzo |
|-----------|-----------|-------|----------|
| **Frontend** | Vercel | Gratis | 5 min |
| **API + SQL Server** | Azure App Service + Azure SQL | Gratis 12 meses | 1h primera vez |

### ✅ Opción intermedia: Local + Túnel (backup plan)

Si no da tiempo al despliegue, usar **Cloudflare Tunnel** o **ngrok** para exponer la app local a internet:

```bash
# Frontend desplegado en Vercel (siempre)
# API expuesta temporalmente:
ngrok http 5187
```

Esto permite abrir la demo desde cualquier dispositivo (tablet en la tienda) apuntando a una URL pública temporal.

### 🎯 Decisión final del Architect

**Plan A (ideal):** Frontend en Vercel + API en Railway (con PostgreSQL)
- Más profesional, URL permanente, funciona desde cualquier dispositivo
- Requiere: agregar soporte para PostgreSQL en EF Core (migration sencilla)
- Se muestra como una app "real" en internet

**Plan B (backup):** Frontend en Vercel + API local con ngrok
- Para el día de la demo si no da tiempo Plan A
- Funciona igual de bien pero la URL de la API es temporal

**Plan C (emergencia):** Todo local en laptop
- Solo si todo lo demás falla
- Llevar laptop con Docker listo + hotspot móvil por si WiFi falla

---

## 📋 Tareas adicionales para Demo Delivery (Plan A)

| # | Tarea | Estimación |
|---|-------|------------|
| 16 | Añadir soporte dual DB (SQL Server dev / PostgreSQL prod) | 2h |
| 17 | Configurar Railway: API + PostgreSQL | 1h |
| 18 | Configurar Vercel: Frontend | 15min |
| 19 | Seed data ejecutable en producción (primera vez) | 30min |
| 20 | Comprar dominio corto (opcional, ~10€) para URL bonita | 15min |

---

## 🗓️ Timeline Revisado

### Semana 1 (14-20 mayo) — HOY ES DÍA 1
- ~~Días 1-3: Completar endpoints API + relación Tournament-Player~~ → Parcialmente hecho (faltan: PUT, DELETE torneo, DELETE jugador de torneo)
- **Día 1-2 (14-15 mayo):** Completar los 3 endpoints pendientes del backend + CORS
- **Día 3-4 (16-17 mayo):** Inicializar frontend React + diseño base
- **Día 5 (18 mayo):** Buffer / catch-up

### Semana 2 (21-27 mayo)
- Días 1-2: Frontend completo (lista torneos, detalle, crear torneo)
- Día 3: Seed data + landing page
- Día 4: Despliegue (Railway + Vercel)
- Día 5: Testing de la demo + ensayo del pitch

### Día D: 28 de mayo
- Demo en tienda con URL real funcionando en tablet/móvil
