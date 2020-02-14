# [Montr](https://montr.net/) &middot; [![GitHub Actions status](https://github.com/montr/montr/workflows/build/badge.svg)](https://github.com/montr/montr) [![GitHub license](https://img.shields.io/badge/license-GPL3.0-blue.svg)](https://github.com/montr/montr/blob/master/LICENSE)

R&D sample of B2B applications

* SSO
* MDM
* more to come...

## Table of contents

- 🚀[Getting Started](#getting-started)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

* [.NET Core 3.1](https://dotnet.microsoft.com/download)
* [Node.js LTS](https://nodejs.org/en/download/)
* [PostgreSQL 12](https://www.postgresql.org/download/)

### Installation

1. Clone repository from `git@github.com:montr/montr.git`
2. Create database `montr` (or choose your database name) in PostgreSQL.
3. Copy sample `secrets.json` from `templates/secrets.json` to `Microsoft/UserSecrets/1f5f8818-a536-4818-b963-2d3ef5dcef03` directory.
   * Specify choosen database name and other connection string parameters in `Default` and `Migration` connections of `ConnectionStrings` section in `secrets.json`.
   * Specify default administrator email and password in `Montr.Idx.IdxOptions` section.
4. Run dotnet to watch backend sources changes in `./src/Host`. During first startup database structure (tables etc.) and default data (users etc.) will be created.
```bash
dotnet watch run
```
5. Install node packages in `./src/ui`.
```bash
npm install
```
6. Run webpack to watch frontend sources changes in `./src/ui`. Compiled assets will be copied to `./src/Host/wwwroot/assets` and served from these location.
```bash
npm start
```
7. Open http://127.0.0.1:5000 or https://127.0.0.1:5001 in browser.

### License

Montr is [GPL 3.0 licensed](./LICENSE).
