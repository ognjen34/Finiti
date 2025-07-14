# Glossary Management System

## Requirements

- Docker
- Docker Compose
- PowerShell (for running scripts)
- Angular CLI (v15+)
- .NET 8 SDK

## Getting Started

1. **Clone the repository**
    ```bash
    git clone <repository-url>
    cd <repository-folder>
    ```

2. **Run the startup script in PowerShell**
    ```powershell
    ./start.ps1
    ```
    This builds and runs backend, frontend, and database containers using Docker Compose.

## Database Initialization

The database is automatically seeded with preset glossary data on startup.

## Login or Register

- Register a new author via the frontend UI.
- Or use the pre-registered user:
  - **Username:** `john.doe`
  - **Password:** `test`

Any pre-registered author uses the format: `firstname.lastname` and password `test`.

## Features

- Full CRUD for glossary terms with domain rules enforcement
- Alphabetically sorted term listing
- Term lifecycle: Draft → Published → Archived
- User authentication for authors
