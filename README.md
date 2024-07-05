# Información del proyecto
En el proyecto es un CRUD realizado en .NET con un una estructura MVC. Se utilizó Entity Framework

# Finalidad del sistema
El sistema tiene como fin administrar el stock y las ordenes de compra de un local para mascotas.
Se podrá cargar la información necesaria de los productos disponibles del local y cargar las ordenes de compra que se realicen, en la plataforma.
La lógica de bajas de stock ante el despacho de una compra se gestionan automáticamente.

# Usuarios y Roles
Cada usuario puede registrarse con email y password.
Cada usuario tendra un único rol asignado. Por defecto el Administrador será el encargado de tener acceso a todas las funcionalidad del sistema. Luego existirán los roles Ventas y Logística según el sector al que perteneces el usuario. Puede haber tantos usuarios como se desee siempre y estos podrán tener acceso a las funcionalidades del sistema una vez que se les haya asignado un rol.

# Comienzo

## Correr migraciones
```
dotnet ef database update 
```
Este comando ejecutará la actualización de todas las migraciones en la base de datos SQLite.

## Iniciar el proyecto
En la raíz del proyecto correr el siguiente comando:
```
dotnet run
```

## Cargar información

Como usuario Administrador tendrá la responsabilidad de generar la carga de información necesaria en el sistema. El sistema tiene 4 secciones para cargar información:
- Productos
- Categorías de Alimentos
- Categorías de Animales
- Marcas

Previo inicio de carga de productos, se debe tener en cuenta la precarga de ambas categorías disponibles, mas las marcas de sus productos.
Una vez se encuentre disponible la información de Categorías y Marcas se puede comenzar la carga de Productos.
Cada producto, ademas de sus atributos, contendrá una relación a Categoría de Alimentos, Categoría de Animales y Marcas.

Como Administrador puede realizar todas las acciones CRUD en cualquiera de estas secciones, incluyendo Usuarios, Ordenes y Roles.

## Facilidad de búsquedas
Cada sección tendrá un filtro dónde se podrá buscar la información deseada por varios campos detallados en los filtros de búsqueda.

## Asignación de roles
Lo hace el administrador, se puede ingresar a la pestaña Usuarios y asignar el rol previamente cargado a los usuarios que se desee.
Los roles disponibles se pueden generar y ver en la pestaña Roles.

## Generación de órdenes de compra
Se generan desde la pestaña Ordenes y esta asignado por defecto al rol Ventas. Se genera la orden con los productos disponibles y con una fecha de creación.

## Despacho de compras
El encargado será el rol Logistica y podrá ver el detalle de la orden con sus productos y despachar las ordenes de compra disponibles previa consulta automática de stock disponible.
Si el stock de algún producto no supera la cantidad del pedido, la orden no podrá despacharse.

## Integridad de información
- Las ordenes de compra ya despachadas no podrán ser eliminadas por usuarios que no sean Administradores. Aquellas ordenes que se eliminen, también eliminará los detalles de dicha orden.
- Eliminar un usuario sólo estará permitido si no tiene órdenes asignadas.
- Eliminar un producto sólo estará permitido si no es parte de un detalle de órden.
- Eliminar un rol sólo estará permitido si no existen usuario con dicho rol asignado.
- Eliminar una Marca o Categoría, sólo stará permitido si no hay Productos que tengan asignado dicha Marca o Categoría.
