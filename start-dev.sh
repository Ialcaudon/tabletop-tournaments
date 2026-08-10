#!/bin/zsh

ROOT_DIR="$(cd "$(dirname "$0")" && pwd)"

if [[ -f "$ROOT_DIR/.env" ]]; then
  set -a
  source "$ROOT_DIR/.env"
  set +a
fi

if [[ -z "${ConnectionStrings__DefaultConnection:-}" ]]; then
  echo "❌ Falta ConnectionStrings__DefaultConnection. Copia .env.example como .env y configura PostgreSQL."
  exit 1
fi

API_PROJECT="$ROOT_DIR/src/TabletopTournaments.API/TabletopTournaments.API.csproj"
WEB_PROJECT="$ROOT_DIR/src/TabletopTournaments.Web/TabletopTournaments.Web.csproj"

cleanup() {
  echo "\n🛑 Deteniendo servicios..."
  kill "$API_PID" "$WEB_PID" 2>/dev/null
  exit 0
}

trap cleanup INT TERM

echo "🚀 Arrancando API en http://localhost:5102 ..."
dotnet run --project "$API_PROJECT" &
API_PID=$!

echo "🌐 Arrancando Web en http://localhost:5067 ..."
dotnet run --project "$WEB_PROJECT" &
WEB_PID=$!

echo "\n✅ Servicios en marcha. Pulsa Ctrl+C para detenerlos.\n"
echo "  API  → http://localhost:5102/swagger"
echo "  Web  → http://localhost:5067"
echo ""

wait "$API_PID" "$WEB_PID"
