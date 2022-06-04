# PolytechWebThings

Web interface for smart house environments. Integrates with [Mozilla WebThings](https://github.com/WebThingsIO/gateway) as a smart house network gateway.

## Use cases

PolytechWebThings is designed to be a high-level interface for Mozilla WebThings subnetworks. Each of those subnetworks represents a local network of a single smart house.
Planned features:
- User account
- Connection to a smart house network. Ability to see and modify state of devices there
- Ability to connect to multiple smart house networks from the same application
- Automation rules for smart house devices

## Architecture
- Single Asp.Net Core server, which serves static assets of frontend application and exposes public API
- Monolythic application based on [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture)
- CQRS with `MediatR`
- JWT-based bearer Authorization
- Frontend is built as a SPA using `Knockout.js` and `TypeScript`. It is placed in the `Web.csproj` project.
- Integration tests at API level using `WebApplicationFactory`

## Used technologies
- `Net6`, `Asp.Net Core 6`
- `Knockout.js`, `TypeScript`, `SCSS`, `Webpack`
- `MS SQL Server` + `EF Core`

## PS
Project is done as a part of my bachelor`s diploma work in Kyiv Polytechnic Institute.

Project is totally open source. Feel free to use or contribute :)
