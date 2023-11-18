using System;
using System.Runtime.Serialization;

namespace VentaControlEstampillas.Domain.Exceptions
{
    [Serializable]
    public sealed class ExcepcionFechaException : CoreBusinessException
    {
        public ExcepcionFechaException()
        {
        }

        public ExcepcionFechaException(string msg) : base(msg)
        {
        }

        public ExcepcionFechaException(string message, Exception inner) : base(message, inner)
        {
        }

        private ExcepcionFechaException(SerializationInfo info, StreamingContext context)
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
