using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityAndSeek.Common
{
    public static class CityAndSeekCommon
    {
        /// <summary>
        /// This will be incremented to designate a new API level which is incompatible any other API level.<br />
        /// If the API level of the client and server don't match, a connection should *not* be made.
        /// </summary>
        public const int ApiLevel = 1;
    }
}
