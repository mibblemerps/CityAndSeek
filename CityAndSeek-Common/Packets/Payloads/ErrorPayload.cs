using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common.Packets.Payloads
{
    public class ErrorPayload
    {
        /// <summary>
        /// The intent that was sent that this error relates to.<br />
        /// If not related to a particular intent, this can be null.
        /// </summary>
        public Intent? ForIntent = null;

        /// <summary>
        /// Error message
        /// </summary>
        public string Message;

        public ErrorPayload(Intent? forIntent, string message)
        {
            ForIntent = forIntent;
            Message = message;
        }
    }
}
