using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common.Packets.Payloads
{
    /// <summary>
    /// Usually sent in response to a packet.<br />
    /// Ensure the Id on the packet is set.
    /// </summary>
    public class ErrorPayload : IPayload
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message;

        public ErrorPayload(string message)
        {
            Message = message;
        }
    }
}
