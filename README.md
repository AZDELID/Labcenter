# LabCenter

> Desktop application for computer laboratory management, user authentication, session control, and workstation monitoring.

---

## 🌍 Languages

- 🇺🇸 English
- 🇪🇸 Español

---

# 🇺🇸 English

## Overview

LabCenter is a Windows desktop application developed in C# and Windows Forms for managing computer laboratory workstations.

The application authenticates users, manages login and logout sessions, displays an independent session timer, integrates with the Windows system tray, and prevents unauthorized workstation access while a session is active.

The project follows an object-oriented architecture that separates the user interface, services, infrastructure, and data models, making the application easier to maintain and extend.

---

## Current Features

- User authentication
- Offline authentication mode
- REST API authentication
- Session management
- Login and logout registration
- Floating session timer
- Windows system tray integration
- Workstation identification
- Full-screen kiosk mode
- Unauthorized close protection
- Administrator emergency access
- Password visibility toggle

---

## Solution Structure

```text
labcenter
│
├── Infrastructure
│   └── TrayIconManager.cs
│
├── Models
│   ├── AuthenticatedUser.cs
│   └── LabCenterConfiguration.cs
│
├── Services
│   ├── AdminAuthenticationService.cs
│   ├── ApiAuthenticationService.cs
│   ├── AuthenticationBridge.cs
│   ├── IAdminAuthenticationService.cs
│   ├── IConfigurationService.cs
│   ├── ILaboratoryCatalog.cs
│   ├── ISessionLifecycleService.cs
│   ├── IUserAuthenticationService.cs
│   ├── LaboratoryCatalog.cs
│   ├── NullSessionLifecycleService.cs
│   ├── OfflineAuthenticationService.cs
│   ├── SessionTimer.cs
│   └── SettingsConfigurationService.cs
│
├── Views
│   ├── MovableWindowBehavior.cs
│   └── SessionTimerForm.cs
│
├── Form1.cs
├── Program.cs
└── App.config
```

---

## Main Components

### Authentication

Provides multiple authentication mechanisms including offline credentials, administrator authentication, and REST API authentication through a common abstraction layer.

### Session Lifecycle

Controls the complete user session lifecycle, including login, logout, session validation, and workstation state.

### Configuration

Centralizes workstation settings and application configuration.

### Infrastructure

Contains platform-specific components such as the Windows notification tray manager.

### User Interface

Implements the login window and the floating session timer using Windows Forms.

### Session Timer

Tracks the elapsed session time independently from the user interface and updates subscribed components.

---

## Technologies

- C#
- .NET Framework
- Windows Forms
- Object-Oriented Programming (OOP)
- REST API
- HttpClient
- Newtonsoft.Json

---

# 🇪🇸 Español

## Descripción

LabCenter es una aplicación de escritorio desarrollada en C# y Windows Forms para la administración de estaciones de trabajo en laboratorios de informática.

La aplicación autentica usuarios, administra el inicio y cierre de sesión, muestra un temporizador independiente de la sesión, se integra con la bandeja del sistema de Windows y protege la estación de trabajo mientras una sesión permanece activa.

El proyecto sigue una arquitectura orientada a objetos que separa la interfaz de usuario, los servicios, la infraestructura y los modelos de datos, facilitando el mantenimiento y la futura expansión del sistema.

---

## Funcionalidades actuales

- Autenticación de usuarios
- Modo de autenticación offline
- Autenticación mediante API REST
- Gestión de sesiones
- Registro de inicio y cierre de sesión
- Temporizador de sesión flotante
- Integración con la bandeja del sistema
- Identificación de la estación de trabajo
- Modo kiosco (pantalla completa)
- Protección contra cierre no autorizado
- Acceso administrativo de emergencia
- Mostrar u ocultar contraseña

---

## Estructura del proyecto

```text
labcenter
│
├── Infrastructure
│   └── TrayIconManager.cs
│
├── Models
│   ├── AuthenticatedUser.cs
│   └── LabCenterConfiguration.cs
│
├── Services
│   ├── AdminAuthenticationService.cs
│   ├── ApiAuthenticationService.cs
│   ├── AuthenticationBridge.cs
│   ├── IAdminAuthenticationService.cs
│   ├── IConfigurationService.cs
│   ├── ILaboratoryCatalog.cs
│   ├── ISessionLifecycleService.cs
│   ├── IUserAuthenticationService.cs
│   ├── LaboratoryCatalog.cs
│   ├── NullSessionLifecycleService.cs
│   ├── OfflineAuthenticationService.cs
│   ├── SessionTimer.cs
│   └── SettingsConfigurationService.cs
│
├── Views
│   ├── MovableWindowBehavior.cs
│   └── SessionTimerForm.cs
│
├── Form1.cs
├── Program.cs
└── App.config
```

---

## Componentes principales

### Autenticación

Implementa múltiples mecanismos de autenticación, incluyendo modo offline, autenticación administrativa y autenticación mediante API REST utilizando una capa de abstracción común.

### Gestión de Sesiones

Controla el ciclo de vida completo de la sesión del usuario, incluyendo el inicio, cierre y validación de la sesión.

### Configuración

Centraliza la configuración de la aplicación y de cada estación de trabajo.

### Infraestructura

Contiene los componentes específicos del sistema operativo, como el administrador de la bandeja del sistema de Windows.

### Interfaz de Usuario

Implementa la ventana de inicio de sesión y el temporizador flotante utilizando Windows Forms.

### Temporizador de Sesión

Controla el tiempo transcurrido de la sesión de forma independiente a la interfaz gráfica y actualiza los componentes que lo utilizan.

---

## Tecnologías

- C#
- .NET Framework
- Windows Forms
- Programación Orientada a Objetos (POO)
- REST API
- HttpClient
- Newtonsoft.Json