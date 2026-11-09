using System;

public class HabitacionHotel
{
    private int numero;
    private string tipo;
    private double precioPorNoche;
    private bool disponible;
    private string clienteActual;

    public int Numero
    {
        get { return numero; }
    }

    public string Tipo
    {
        get { return tipo; }
        set
        {
            if (value == "Individual" ||
                value == "Doble" ||
                value == "Suite")
            {
                tipo = value;
            }
            else
            {
                Console.WriteLine("Tipo de habitación no válido.");
            }
        }
    }

    public double PrecioPorNoche
    {
        get { return precioPorNoche; }
        set
        {
            if (value > 0)
            {
                precioPorNoche = value;
            }
            else
            {
                Console.WriteLine("El precio debe ser mayor que cero.");
            }
        }
    }

    public bool Disponible
    {
        get { return disponible; }
    }

    public string ClienteActual
    {
        get { return clienteActual; }
    }

    public HabitacionHotel(int numero, string tipo, double precioPorNoche)
    {
        this.numero = numero;
        this.tipo = "Individual";
        this.precioPorNoche = 50000;
        this.disponible = true;
        this.clienteActual = "";

        Tipo = tipo;
        PrecioPorNoche = precioPorNoche;
    }

    public double Reservar(string nombreCliente, int noches)
    {
        if (disponible && noches > 0 &&
            !string.IsNullOrWhiteSpace(nombreCliente))
        {
            disponible = false;
            clienteActual = nombreCliente;

            return precioPorNoche * noches;
        }

        return -1;
    }

    public bool Liberar()
    {
        if (!disponible)
        {
            disponible = true;
            clienteActual = "";

            return true;
        }

        return false;
    }

    public string MostrarInfo()
    {
        string estado = disponible ? "Disponible" : "Ocupada";
        string cliente = disponible ? "Ninguno" : clienteActual;

        return "Habitación: " + numero +
               "\nTipo: " + tipo +
               "\nPrecio por noche: $" +
               precioPorNoche.ToString("N0") +
               "\nEstado: " + estado +
               "\nCliente actual: " + cliente;
    }

    public void AplicarDescuento(int porcentaje)
    {
        if (porcentaje < 0)
        {
            Console.WriteLine(
                "El descuento no puede ser negativo.");

            return;
        }

        double nuevoPrecio =
            precioPorNoche * (1 - porcentaje / 100.0);

        if (nuevoPrecio <= 0)
        {
            Console.WriteLine(
                "El precio no puede quedar en cero o negativo.");

            return;
        }

        precioPorNoche = nuevoPrecio;
    }

    public string ObtenerDatosResumen()
    {
        string estado;

        if (disponible)
        {
            estado = "Disponible";
        }
        else
        {
            estado = "Ocupada por: " + clienteActual;
        }

        return "[" + numero + "] " +
               tipo + " - $" +
               precioPorNoche.ToString("N0") +
               " - " + estado;
    }
}


public class Program
{
    public static void Main(string[] args)
    {

        HabitacionHotel habitacion101 =
            new HabitacionHotel(101, "Individual", 50000);

        HabitacionHotel habitacion202 =
            new HabitacionHotel(202, "Doble", 80000);

        HabitacionHotel habitacion303 =
            new HabitacionHotel(303, "Suite", 150000);

        HabitacionHotel[] habitaciones =
        {
            habitacion101,
            habitacion202,
            habitacion303
        };

        Console.WriteLine("=== ESTADO INICIAL ===");

        Console.WriteLine(
            habitacion101.MostrarInfo());

        Console.WriteLine("----------------------");

        Console.WriteLine(
            habitacion202.MostrarInfo());

        Console.WriteLine("----------------------");

        Console.WriteLine(
            habitacion303.MostrarInfo());

        Console.WriteLine("----------------------");

        Console.WriteLine("\n=== OPERACIONES ===");

        double costo1 =
            habitacion101.Reservar(
                "Jaider Reyes", 3);

        Console.WriteLine(
            "\nReserva para Jaider Reyes:");

        Console.WriteLine(
            "Costo total: $" +
            costo1.ToString("N0"));


        double costo2 =
            habitacion202.Reservar(
                "Alan Walker", 2);

        Console.WriteLine(
            "\nReserva para Alan Walker:");

        Console.WriteLine(
            "Costo total: $" +
            costo2.ToString("N0"));


        double costo3 =
            habitacion101.Reservar(
                "Otro Cliente", 1);

        Console.WriteLine(
            "\nIntento de reservar nuevamente la 101:");

        if (costo3 == -1)
        {
            Console.WriteLine(
                "Reserva fallida: la habitación está ocupada.");
        }
        else
        {
            Console.WriteLine(
                "Costo total: $" +
                costo3.ToString("N0"));
        }


        bool liberada =
            habitacion101.Liberar();

        Console.WriteLine(
            "\nLiberar habitación 101:");

        if (liberada)
        {
            Console.WriteLine(
                "Habitación liberada correctamente.");
        }
        else
        {
            Console.WriteLine(
                "La habitación ya estaba disponible.");
        }


        habitacion303.AplicarDescuento(10);

        Console.WriteLine(
            "\nDescuento del 10% aplicado a la habitación 303.");

        Console.WriteLine(
            "Nuevo precio por noche: $" +
            habitacion303.PrecioPorNoche.ToString("N0"));


        double costo4 =
            habitacion101.Reservar(
                "Dayana Peña", 1);

        Console.WriteLine(
            "\nReserva para Dayana Peña:");

        Console.WriteLine(
            "Costo total: $" +
            costo4.ToString("N0"));

        Console.WriteLine(
            "\n=== RESUMEN DE HABITACIONES ===");

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine(
                habitacion.ObtenerDatosResumen());
        }

        int ocupadas = 0;
        int libres = 0;

        double sumaPrecios = 0;


        foreach (HabitacionHotel habitacion in habitaciones)
        {
            if (habitacion.Disponible)
            {
                libres++;
            }
            else
            {
                ocupadas++;
            }

            sumaPrecios =
                sumaPrecios +
                habitacion.PrecioPorNoche;
        }

        double precioPromedio =
            sumaPrecios / habitaciones.Length;

        Console.WriteLine(
            "\n=== MÉTRICAS FINALES ===");

        Console.WriteLine(
            "Habitaciones ocupadas: " +
            ocupadas);

        Console.WriteLine(
            "Habitaciones libres: " +
            libres);

        Console.WriteLine(
            "Precio promedio por noche: $" +
            precioPromedio.ToString("N0"));

        Console.WriteLine(
            "\n=== ESTADO FINAL ===");

        foreach (HabitacionHotel habitacion in habitaciones)
        {
            Console.WriteLine(
                habitacion.MostrarInfo());

            Console.WriteLine("----------------------");
        }
    }
}
```
