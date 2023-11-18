using System.Runtime.Serialization;

namespace VentaControlEstampillas.Domain.Exceptions
{
    [Serializable]
    public class ExcepcionEstadoException : CoreBusinessException
    {
        public ExcepcionEstadoException()
        {
        }

        public ExcepcionEstadoException(string message) : base(message)
        {
        }

        public ExcepcionEstadoException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExcepcionEstadoException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
                
            base.GetObjectData(info, context);
        }
    }
}
