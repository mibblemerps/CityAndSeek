using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CityAndSeek.Common
{
    /// <summary>
    /// Represents a City and Seek game and it's state.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Unique ID for this game.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Name of the game.
        /// </summary>
        public virtual string Name { get; set; } = "Game";

        /// <summary>
        /// Password to join the game.
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Game mode being played.
        /// </summary>
        public virtual GameMode GameMode { get; set; } = GameMode.Assassins;

        /// <summary>
        /// Current state of the game.
        /// </summary>
        public virtual GameState GameState { get; set; } = GameState.Setup;

        /// <summary>
        /// Players in this game.
        /// </summary>
        public virtual List<Player> Players { get; set; } = new List<Player>();

        public override string ToString()
        {
            return $"\"{Name}\" ({Id})";
        }
    }
}
