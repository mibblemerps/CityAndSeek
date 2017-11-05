using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CityAndSeek.Common
{
    public enum GameState
    {
        /// <summary>
        /// Whilst the game is being setup (configured, players added, etc..)
        /// </summary>
        Setup,

        /// <summary>
        /// The grace period when a game is started that allows players to disperse out.
        /// </summary>
        Starting,
        
        /// <summary>
        /// When the game is running normally.
        /// </summary>
        Running,

        /// <summary>
        /// Once the game has ended and players are reviewing statistics and how the game went.<br />
        /// A game can stay in this state indefinitely.<br />
        /// Once a game has ended it cannot be ran again.
        /// </summary>
        Ended,

        /// <summary>
        /// The game is temporarily paused.<br />
        /// Upon unpausing, the game will transition to the Running state.
        /// </summary>
        Paused
    }
}
