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
        public int Id;

        /// <summary>
        /// Name of the game.
        /// </summary>
        public string Name = "Game";

        /// <summary>
        /// Password to join the game.
        /// </summary>
        public string Password = "";
        
        /// <summary>
        /// Game mode being played.
        /// </summary>
        public GameMode GameMode = GameMode.Assassins;

        /// <summary>
        /// Current state of the game.
        /// </summary>
        public GameState GameState = GameState.Setup;

        /// <summary>
        /// Players in this game.
        /// </summary>
        public List<Player> Players = new List<Player>();

        public override string ToString()
        {
            return $"\"{Name}\" ({Id})";
        }
    }
}
