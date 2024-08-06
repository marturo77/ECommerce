Claro, aquí tienes una versión mejorada que enfatiza más los conceptos de arquitectura limpia según los estándares internacionales de ingeniería de software:

---

# Prueba Técnica por Marcelo Arturo Duarte

## Descripción del Prototipo

Este prototipo de aplicación e-commerce se desarrolló siguiendo los principios de arquitectura limpia, que son ampliamente reconocidos y recomendados en los estándares internacionales de ingeniería de software. A continuación se detallan las características principales de la arquitectura implementada:

### Estructura del Proyecto

El prototipo está compuesto por dos proyectos principales:

1. **Aplicación**: Contiene la lógica de negocio y el flujo operativo del programa, así como el manejo de las peticiones web.
2. **Infraestructura**: Incluye la implementación concreta de los conceptos y patrones utilizados en la aplicación.

### Principios y Patrones de Diseño

#### **Separación de Responsabilidades**
La división entre Aplicación e Infraestructura facilita la implementación de diferentes versiones de las interfaces. Esto permite cambiar fácilmente los proveedores en el registro de inyección de dependencias. Este principio asegura que cada componente del sistema tenga una única responsabilidad, promoviendo un diseño más limpio y fácil de mantener.

#### **Arquitectura Limpia (Clean Architecture)**
Se aplicaron los principios de la arquitectura limpia, que incluye:
- **Independencia del Framework**: La arquitectura no depende de ningún framework específico. Esto permite que los detalles de implementación cambien sin afectar la lógica de negocio.
- **Testabilidad**: La estructura del código facilita la creación de pruebas unitarias y de integración.
- **Independencia de la UI**: La lógica de negocio es independiente de la interfaz de usuario, lo que permite cambiar la UI sin afectar el núcleo de la aplicación.
- **Independencia de la Base de Datos**: La lógica de negocio no depende de detalles de acceso a datos, permitiendo cambiar la base de datos sin afectar el resto del sistema.
- **Independencia de Agencias Externas**: Las decisiones sobre tecnologías, herramientas y librerías externas se pueden cambiar sin afectar la lógica de negocio.

#### **Inversión de Control (IoC)**
La inversión de control se implementó a través de la inyección de dependencias, asegurando que las clases no controlen sus propias dependencias, sino que estas les sean proporcionadas desde el exterior. Esto promueve un acoplamiento bajo y una alta cohesión en el sistema.

#### **Patrón Mediator (MediatR)**
Se utilizó el patrón Mediator para manejar las solicitudes de comandos y consultas. Este patrón facilita la comunicación desacoplada entre los componentes, mejorando la mantenibilidad y escalabilidad de la aplicación.

### Implementación de Endpoints

Los endpoints del API se implementaron utilizando Minimal APIs, una característica introducida en .NET 6, que permite definir rutas de manera más concisa y directa. Este enfoque simplifica la comprensión y el mantenimiento del código.

### Ventajas de la Arquitectura Implementada

- **Facilidad de Extensión**: La implementación de interfaces permite agregar nuevas funcionalidades o cambiar componentes sin afectar el resto del sistema.
- **Mantenibilidad**: La separación clara de responsabilidades y el uso de patrones de diseño modernos facilitan el mantenimiento del código a largo plazo.
- **Escalabilidad**: La arquitectura está diseñada para crecer y adaptarse a las necesidades cambiantes del proyecto.

### Sobre la base de datos
Solamente se necesita compilar y publicarla en un servidor mssql 19 o compatible. Tener en cuenta la cadena de conexion para ser usadada en appsettings.json del proyecto webapi

### Sobre la aplicacion FrontEnd Angular 18
La aplicación frontend se desarrolló utilizando Angular 18, adoptando una arquitectura modular y basada en componentes. 
Esta arquitectura facilita la reutilización de código y la mantenibilidad del proyecto. 
La interfaz de usuario se diseñó con Bootstrap 5 para asegurar una experiencia de usuario responsiva y moderna. 
Además, se implementaron servicios de Angular para gestionar la comunicación con el backend, utilizando RxJS para manejar las operaciones asincrónicas de manera eficiente. La aplicación permite la gestión dinámica de productos y órdenes, integrando de manera fluida con la API desarrollada.