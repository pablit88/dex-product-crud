# dex-product-crud
Code challenge for Dex

Backend:
Esta solución utiliza SQL Server como DBMS.

Instalación de la base de datos
SQL Server: Crear un login con los roles dbcreator y public. Las credenciales de este login se deben corresponder con las colocadas en el connection string del archivo Api/appsettings.json.

Desde la carpeta src dentro de la solución ProductCrud ejecutar los siguientes comandos para agregar y aplicar migraciones:

dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api

dotnet ef database update --project Infrastructure --startup-project Api