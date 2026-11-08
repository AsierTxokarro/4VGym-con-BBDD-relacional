4GYM API
API REST del gimnasio 4VGym. Une los dos proyectos de clase:

4VGymAPI: aporta los controllers y el Swagger (antes con datos en memoria).
4VGymEF: aporta los modelos y la BBDD en SQL Server con Entity Framework.
La unión entre ambos son los repositorios: los controllers ya no usan listas en memoria, sino que leen de la BBDD con EF.

Cómo ejecutarlo
Crear la BBDD con el modelo de 4VGymEF.
Ejecutar SQLquery1.sql para cargar los datos de prueba.
Revisar la cadena de conexión y arrancar el proyecto.
Abrir Swagger en https://localhost:7186/swagger.
Los POST validan, pero no insertan
Como todavía no hemos visto inserciones con EF, los POST solo validan y devuelven 201 con el objeto recibido. Por eso, un GET posterior no mostrará el registro nuevo: es el comportamiento esperado.

Antes llamaba a Add y daba error 500. La causa: el Swagger obliga a enviar un id en el POST, pero en la BBDD la columna Id es IDENTITY (la genera SQL Server), y rechaza insertar un id explícito.

Soy consciente de que un POST real no debería recibir el id. No lo cambio porque el enunciado pide el mismo Swagger que 4VGymAPI. El método Add queda en el repositorio, sin usar, listo para cuando se vean las inserciones.

Decisiones
¿DTO o modelos de BBDD? Uso los modelos de EF directamente, porque el Swagger debe ser idéntico al original. Cuando haya inserciones reales usaría un DTO de entrada sin Id.
¿Dónde valido? En el controller, antes de llamar al repositorio. El repositorio solo accede a datos.
