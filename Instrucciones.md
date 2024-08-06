
# Instrucciones para Ejecutar el Prototipo

## Prerrequisitos

1. **Instalar .NET 8 SDK**: Asegúrate de tener instalado .NET 8 SDK en tu máquina. Puedes descargarlo desde [aquí](https://dotnet.microsoft.com/download/dotnet/8.0).
2. **Instalar Node.js**: Asegúrate de tener Node.js y npm (Node Package Manager) instalados. Puedes descargarlo desde [aquí](https://nodejs.org/).
3. **Instalar Angular CLI**: Si no tienes Angular CLI instalado, puedes instalarlo globalmente usando el siguiente comando:
   ```sh
   npm install -g @angular/cli
   ```
4. **Instalar un servidor de base de datos SQL Server**: Puedes usar SQL Server LocalDB, SQL Server Express, o cualquier otra instancia de SQL Server.

## Configuración del Backend

1. **Clonar el repositorio**:
   ```sh
   git clone <URL_DEL_REPOSITORIO>
   cd <CARPETA_DEL_REPOSITORIO>
   ```

2. **Configurar la base de datos**:
   - Edita el archivo `appsettings.json` en el proyecto de la API para configurar la cadena de conexión a tu base de datos SQL Server.

3. **Aplicar las migraciones de la base de datos**:
   ```sh
   dotnet ef database update
   ```

4. **Ejecutar el proyecto backend**:
   ```sh
   cd <CARPETA_DEL_PROYECTO_API>
   dotnet run
   ```

5. **Verificar que el backend esté corriendo**:
   - Abre un navegador y navega a `https://localhost:7151/swagger` para verificar que la API esté funcionando y puedas ver la documentación generada por Swagger.

## Configuración del Frontend

1. **Navegar al directorio del frontend**:
   ```sh
   cd <CARPETA_DEL_PROYECTO_ANGULAR>
   ```

2. **Instalar las dependencias**:
   ```sh
   npm install
   ```

3. **Configurar la URL de la API en Angular**:
   - Edita el archivo `environment.ts` en la carpeta `src/environments` para asegurarte de que la URL de la API es correcta, por ejemplo:
     ```typescript
     export const environment = {
       production: false,
       apiUrl: 'https://localhost:7151/api'
     };
     ```

4. **Ejecutar el proyecto frontend**:
   ```sh
   ng serve
   ```

5. **Verificar que el frontend esté corriendo**:
   - Abre un navegador y navega a `http://localhost:4200` para verificar que la aplicación frontend esté funcionando.

## Verificación del Funcionamiento

1. **Crear Productos**:
   - Navega a la sección de productos en la aplicación frontend.
   - Crea algunos productos usando el formulario disponible.

2. **Crear Órdenes**:
   - Navega a la sección de órdenes en la aplicación frontend.
   - Crea una nueva orden seleccionando productos y especificando cantidades.

3. **Verificar CRUD Completo**:
   - Asegúrate de que puedes crear, listar, actualizar y eliminar productos y órdenes desde la aplicación frontend.
   - Verifica que las operaciones de eliminación en cascada funcionan correctamente eliminando una orden y asegurándote de que los `OrderItemInfo` asociados también se eliminan.

4. **Verificar la Documentación de la API**:
   - Asegúrate de que todas las rutas de la API están documentadas y accesibles a través de Swagger en `https://localhost:7151/swagger`.

Siguiendo estos pasos, deberías ser capaz de configurar, ejecutar y verificar el correcto funcionamiento del prototipo de la aplicación e-commerce.
