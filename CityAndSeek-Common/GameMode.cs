using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CityAndSeek.Common
{
    public enum GameMode
    {
        /// <summary>
        /// Each player gets a target player and they have to catch that player.
        /// </summary>
        Assassins,

        /// <summary>
        /// Effectively gang-up chasey. One person is “it” and once they catch someone, that person becomes “it” also.
        /// </summary>
        Infection
    }
}
