# MAT — Intranet agencia de viajes

ASP.NET MVC 4 / .NET 4.8 · SQL Server · DBHelper + SPs (`MAT.DB`).

## Por dónde empezar

1. **[`docs/HANDOFF.md`](docs/HANDOFF.md)** — estado, roles y forma de trabajo
2. **[`docs/PENDIENTES.md`](docs/PENDIENTES.md)** — checklist vivo (sin Issues)
3. **[`docs/DECISIONES_ABIERTAS.md`](docs/DECISIONES_ABIERTAS.md)** — decisiones de negocio pendientes
4. **[`CLAUDE.md`](CLAUDE.md)** — stack, arquitectura y comandos
5. **[`DOCUMENTACION/GUIA_SISTEMA_MAT.md`](DOCUMENTACION/GUIA_SISTEMA_MAT.md)** — guía técnica

Docs históricos cerrados: [`DOCUMENTACION/Archive/`](DOCUMENTACION/Archive/README.md).

## Compilar

```bash
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MAT.MVC\MAT.MVC.csproj /t:Build /p:Configuration=Debug
```
