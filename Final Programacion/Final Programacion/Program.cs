using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class TP3
{
    static string[,] datos = ObtenerDatos();

    static void Main()
    {
        int opcion = 1;

        Console.WriteLine("------Analizador de datos de F1------");
        do
        {
            Console.WriteLine("Elija una opcion:" +
                "\n1.Buscar un piloto específico con nombre y mostrar cuántos podios tuvo en total. " +
                "\n2.Obtener los datos de un equipo de un año especifico. " +
                "\n3.Mostrar cuál fue la remontada más grande " +
                "\n4.Mostrar los nombres de todos los equipos ordenados por orden alfabético. " +
                "\n5.Mostrar todos los datos" +
                "\n6.Mostrar el equipo con más victorias" +
                "\n7.Salir");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1: //buscar piloto y sus podios
                    Console.WriteLine("Ingrese el nombre completo del piloto: ");
                    string nombreBuscado = Console.ReadLine();
                    int podio = ContarPodios(datos, nombreBuscado);
                    Console.WriteLine("El corredor " + nombreBuscado + " ha entrado en el podio " + podio + " veces.");
                    break;

                case 2://Obtener datos de un equipo en un año especifico
                    Console.WriteLine("¿De qué equipo desea saber los datos?");
                    string equipoBuscado = Console.ReadLine();
                    Console.WriteLine("¿De qué año desea saber los datos?");
                    int añoBuscado = int.Parse(Console.ReadLine());
                    AñoEquipo(datos, equipoBuscado, añoBuscado);
                    break;

                case 3:// remontada más grande
                    Remontada(datos);
                    break;

                case 4://todos los equipos por orden alfabetico
                    MostrarEquiposOrdenados(datos);
                    break;

                case 5://todos los datos
                    MostrarTodosLosDatos(datos);
                    break;

                case 6:
                    EquipoConMasVictorias(datos);
                    break;

                case 7:
                    Console.WriteLine("Cerrando programa...");
                    return;

                default:
                    Console.WriteLine("Opcion no valida");
                    break;
            }

        } while (opcion != 7);
    }

    static string[,] ObtenerDatos()
    {
        string rutaArchivo = @"..\..\..\f1_last5years.csv";
        string[] lineas = File.ReadAllLines(rutaArchivo);

        int filas = lineas.Length;
        int cols = lineas[0].Split(',').Length;

        string[,] datos = new string[filas, cols];

        for (int i = 0; i < filas; i++)
        {
            string[] values = lineas[i].Split(',');
            for (int j = 0; j < cols; j++)
            {
                datos[i, j] = values[j];
            }
        }

        return datos;
    }

    static int ContarPodios(string[,] datos, string nombreBuscado)
    {
        int podio = 0;

        for (int i = 1; i < datos.GetLength(0); i++)
        {
            string nombre = datos[i, 2];
            int posicion = int.Parse(datos[i, 5]);

            if (nombreBuscado.ToLower() == nombre.ToLower() && posicion <= 3)
            {
                podio++;
            }
        }

        return podio;
    }

    static void AñoEquipo(string[,] datos, string equipoBuscado, int añoBuscado)
    {
        int totalPuntos = 0;
        for (int i = 1; i < datos.GetLength(0); i++)
        {
            string nombreEquipo = datos[i, 1];
            int año = int.Parse(datos[i, 0]);

            if (equipoBuscado.ToLower() == nombreEquipo.ToLower() && añoBuscado == año)
            {
                Console.WriteLine("En la carrera de " + datos[i, 3] + ", el corredor " +
                                  datos[i, 2] + " realizó " + datos[i, 6] + " puntos.");
                totalPuntos += int.Parse(datos[i, 6]);
            }
        }
        Console.WriteLine("Puntos totales del equipo " + equipoBuscado + ": " + totalPuntos);
    }

    static void Remontada(string[,] datos)
    {
        int mejorRemontada = 0;
        int filaMejorRemontada = 0;

        for (int i = 1; i < datos.GetLength(0); i++)
        {
            int posicionInicial = int.Parse(datos[i, 4]);
            int posicionFinal = int.Parse(datos[i, 5]);

            int remontada = posicionInicial - posicionFinal;

            if (remontada > mejorRemontada)
            {
                mejorRemontada = remontada;
                filaMejorRemontada = i;
            }
        }

        Console.WriteLine($"La mejor remontada es de {mejorRemontada} posiciones.");
        Console.WriteLine($"Hecha por el conductor: {datos[filaMejorRemontada, 2]}.");
        Console.WriteLine($"En la temporada: {datos[filaMejorRemontada, 0]}.");
        Console.WriteLine($"En la carrera: {datos[filaMejorRemontada, 3]}.");
        Console.WriteLine($"Del equipo: {datos[filaMejorRemontada, 1]}.");
    }

    static void MostrarEquiposOrdenados(string[,] datos)
    {
        List<string> equipos = new List<string>();
        for (int i = 1; i < datos.GetLength(0); i++)
        {
            string equipo = datos[i, 1];
            if (!equipos.Contains(equipo))
            {
                equipos.Add(equipo);
            }
        }

        string[] equiposArreglo = equipos.ToArray();
        MetodoBurbuja(equiposArreglo);

        Console.WriteLine("Equipos ordenados alfabéticamente:");
        foreach (string e in equiposArreglo)
        {
            Console.WriteLine(e);
        }
    }

    static void MostrarTodosLosDatos(string[,] datos)
    {
        Console.WriteLine("Temporada | Equipo | Piloto | Carrera | Pos. Clasificación | Pos. Final | Puntos");
        for (int i = 1; i < datos.GetLength(0); i++)
        {
            Console.WriteLine($"{datos[i, 0]} | {datos[i, 1]} | {datos[i, 2]} | {datos[i, 3]} | {datos[i, 4]} | {datos[i, 5]} | {datos[i, 6]}");
        }
    }

    // NUEVA FUNCIONALIDAD: Muestra el equipo con la mayor cantidad de victorias (Posición Final = 1)
    static void EquipoConMasVictorias(string[,] datos)
    {
        Dictionary<string, int> victoriasPorEquipo = new Dictionary<string, int>();

        for (int i = 1; i < datos.GetLength(0); i++)
        {
            string equipo = datos[i, 1];
            int posicionFinal = int.Parse(datos[i, 5]);

            if (posicionFinal == 1) // victoria
            {
                if (!victoriasPorEquipo.ContainsKey(equipo))
                    victoriasPorEquipo[equipo] = 0;

                victoriasPorEquipo[equipo]++;
            }
        }

        if (victoriasPorEquipo.Count == 0)
        {
            Console.WriteLine("No hay victorias registradas.");
            return;
        }

        string equipoMax = victoriasPorEquipo.First().Key;
        int maxVictorias = victoriasPorEquipo[equipoMax];

        foreach (var item in victoriasPorEquipo)
        {
            if (item.Value > maxVictorias)
            {
                equipoMax = item.Key;
                maxVictorias = item.Value;
            }
        }

        Console.WriteLine($"\nEl equipo con más victorias es {equipoMax} con {maxVictorias} victorias.");
    }

    static void MetodoBurbuja(string[] v)
    {
        int n = v.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool ordenado = true;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (string.Compare(v[j], v[j + 1]) > 0)
                {
                    ordenado = false;
                    string temp = v[j];
                    v[j] = v[j + 1];
                    v[j + 1] = temp;
                }
            }
            if (ordenado)
                break;
        }
    }
}

