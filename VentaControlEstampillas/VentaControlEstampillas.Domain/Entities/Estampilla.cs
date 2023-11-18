using VentaControlEstampillas.Domain.Exceptions;


namespace VentaControlEstampillas.Domain.Entities
{
    public class Estampilla : DomainEntity
    {
        private static readonly int[] DENOM_PERMITIDA = { 1000, 5000, 10000 };
        private static readonly string[] ESTADO_PERMITIDO = { "Activo", "Inactivo" }; 

        public Estampilla(int denominacion, DateTime fechaInicioValidez, DateTime fechaFinValidez, string estado)
        {
            Denominacion = DENOM_PERMITIDA.Contains(denominacion)
                      ? denominacion
                      : throw new CoreBusinessException("La denominación debe ser 1000, 5000 o 10000");
            FechaInicioValidez = fechaInicioValidez;
            FechaFinValidez = fechaFinValidez;                    
            Estado = string.IsNullOrEmpty(estado) ? "Activo" : estado;
        }

        public void ValidarInactividad()
        {
            if (Estado == "Inactivo")
            {
                throw new CoreBusinessException("La estampilla ya ha sido utilizada y está inactiva.");
            }
        }

        public static void ValidarNulabilidad(Estampilla estampilla)
        {
            if (estampilla == null)
            {
                throw new ArgumentNullException(nameof(estampilla), "El parámetro estampilla no puede ser null.");
            }
        }

        public static void ValidarDenominacion(DetalleVenta detalleVenta)
        {
            const double epsilon = 0.0001; 
            if (!Array.Exists(DENOM_PERMITIDA, denom => Math.Abs(denom - detalleVenta.PrecioUnitario) < epsilon))
            {
                throw new CoreBusinessException("El precio de la estampilla debe ser 1000, 5000 o 10000.");
            }
        }


        public bool EsFechaInicioMenorActual => FechaInicioValidez.Date < DateTime.Now.Date;
        public bool EsFechaFinMenorActual => FechaFinValidez.Date < DateTime.Now.Date;
        public bool EsFechaInicioMayorFechaFin => FechaFinValidez.Date < FechaInicioValidez.Date;
        public bool EsEstadoValido => ESTADO_PERMITIDO.Contains(Estado);



        public int Denominacion { get; init; }
        public DateTime FechaInicioValidez { get; init; }
        public DateTime FechaFinValidez { get; init; }
        public string Estado { get; set; }
    }

}

