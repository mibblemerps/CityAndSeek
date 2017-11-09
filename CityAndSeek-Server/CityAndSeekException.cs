using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Server
{
    /// <summary>
    /// A City and Seek related exception whilst servicing a client request.<br />
    /// When this is caught, the message is sent back to the client, so make sure the message is informative to an end-user.
    /// </summary>
    public class CityAndSeekException : Exception
    {
        public CityAndSeekException()
        {
        }

        public CityAndSeekException(string message) : base(message)
        {
        }

        public CityAndSeekException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Thrown when trying to send a request but the client isn't authenticated as a player.
        /// </summary>
        public class UnauthenticatedException : CityAndSeekException
        {
            public UnauthenticatedException() : base("Not authenticated as a player")
            {
            }
        }
    }
}
