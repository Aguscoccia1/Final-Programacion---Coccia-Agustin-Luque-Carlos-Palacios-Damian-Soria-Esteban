# Final-Programacion---Coccia-Agustin-Luque-Carlos-Palacios-Damian-Soria-Esteban

Analizador de datos de F1
--Descripción

Este proyecto en C# permite analizar un dataset histórico de Fórmula 1 contenido en un archivo CSV y realizar distintas consultas estadísticas. El programa permite obtener:
-Cantidad de podios de un piloto.
-Datos de un equipo en un año específico, incluyendo puntos totales.
-La remontada más grande registrada.
-El listado completo de equipos ordenados alfabéticamente.
-Todos los datos cargados desde el archivo.
-El equipo con mayor cantidad de victorias.

Este trabajo fue desarrollado como parte de una práctica universitaria de Programación 1 (Tecnicatura Superior en Desarrollo de Software, 2025).

--Cómo usar o iniciar
1.Clonar el repositorio o descargar el proyecto.
2.Colocar el archivo f1_last5years.csv en la ruta esperada o modificar la variable rutaArchivo en ObtenerDatos() para apuntar a su ubicación real.
3.Compilar el proyecto en un entorno compatible con C# y .NET.
4.Ejecutar el programa. Se mostrará un menú por consola con las distintas opciones disponibles.

--Estructura del código

Clase o Método / Función

-ObtenerDatos(): Lee el archivo .csv y lo carga en un arreglo bidimensional (string[,]) en memoria.
-ContarPodios(datos, nombrePiloto): Recorre los datos y cuenta cuántas veces el piloto terminó en posición ≤ 3.
-AñoEquipo(datos, equipo, año): Muestra resultados por carrera de un equipo en un año dado y calcula puntos totales.
-Remontada(datos): Encuentra la mayor remontada (diferencia entre posición de largada y final).
-MostrarEquiposOrdenados(datos): Extrae nombres de escuderías únicos y los lista en orden alfabético mediante un algoritmo de ordenamiento burbuja.
-MostrarTodosLosDatos(datos): Imprime en consola cada fila del dataset con sus campos.
-EquipoConMasVictorias(datos): Usa un diccionario para contar cuántas victorias (posición final igual a 1) tiene cada equipo y determina el máximo.
-MetodoBurbuja(string[] v): Implementa un algoritmo de ordenamiento simple para arreglos de strings.

--Detalles técnicos y suposiciones
-El archivo CSV sigue el formato: Año, Equipo, Piloto, Carrera, Posición Clasificación, Posición Final, Puntos.
-Se omite la primera fila del CSV por considerarse encabezado.
-Se utilizan listas, diccionarios y métodos de LINQ para organizar y procesar los datos.
