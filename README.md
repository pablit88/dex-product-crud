# dex-product-crud
Code challenge for Dex

Backend:
Esta solución utiliza SQL Server como DBMS.

Instalación de la base de datos
SQL Server: Crear un login con los roles dbcreator y public. Las credenciales de este login se deben corresponder con las colocadas en el connection string del archivo Api/appsettings.json.

Desde la carpeta "dex-product-crud\backend\ProductCrud\src" ejecutar los siguientes comandos para crear y aplicar las migraciones de EF Core:

dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api

dotnet ef database update --project Infrastructure --startup-project Api

Página de Swagger con la documentación de la API se sirve localmente en:
https://localhost:7286/swagger/index.html

En el archivo "dex-product-crud\backend\ProductCrud\src\Api\appsettings.json" está la sección "CorsSettings" para permitir el origen de la App de Angular local pueda consumir la API.

Frontend:

Para servirla situarse en la carpeta "dex-product-crud\frontend\product-crud-app" y ejecutar el comando "ng serve".

La app se sirve localmente en:
http://localhost:4200
