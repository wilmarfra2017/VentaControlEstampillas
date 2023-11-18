namespace VentaControlEstampillas.Domain.Services
{
    [DomainService]
    public class ServDescuentoRecargoEstampilla
    {
        const double MIN_VALOR = 9999.99;
        const double MAX_VALOR = 10000.01;
        const double DESCUENTO_LUNES = 0.98;
        const double RECARGO_SABADO = 1.02;
        const double DESCUENTO_CANTID_MIN = 0.90;
        const double DESCUENTO_CANTID_MAX = 0.87;
        const int LIMITE_CANTIDAD_MIN = 20;
        const int LIMITE_CANTIDAD_MED = 59;
        const int LIMITE_CANTIDAD_MAX = 60;



        public static double CalcularPrecioConDescuentosYRecargos(double precioUnitario, int cantidadEstampillas)
        {
            double precioConDescuentos = AplicarDescuentoLunes(precioUnitario);
            double precioConRecargos = AplicarRecargoSabado(precioConDescuentos);
            double precioTotal = precioConRecargos * cantidadEstampillas;

            return AplicarDescuentoPorCantidad(precioTotal, cantidadEstampillas);
        }


        public static double AplicarDescuentoLunes(double precioUnitario)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && precioUnitario >= MIN_VALOR && precioUnitario <= MAX_VALOR)
            {
                return precioUnitario * DESCUENTO_LUNES; // Descuento del 2%
            }
            return precioUnitario;
        }

        public static double AplicarRecargoSabado(double precioUnitario)
        {            
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                return precioUnitario * RECARGO_SABADO; // Aumento del 2%
            }
            return precioUnitario;
        }

        public static double AplicarDescuentoPorCantidad(double precioTotal, int cantidadEstampillas)
        {
            if (cantidadEstampillas >= LIMITE_CANTIDAD_MIN && cantidadEstampillas <= LIMITE_CANTIDAD_MED)
            {
                return precioTotal * DESCUENTO_CANTID_MIN;  // Descuento del 10%
            }

            if (cantidadEstampillas >= LIMITE_CANTIDAD_MAX)
            {
                return precioTotal * DESCUENTO_CANTID_MAX;  // Descuento del 13%
            }
            return precioTotal;
        }

        public virtual DayOfWeek DiaActual => DateTime.Now.DayOfWeek;

    }
}
