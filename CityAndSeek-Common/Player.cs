using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common.StatusEffects;
using Newtonsoft.Json;

namespace CityAndSeek.Common
{
    public class Player
    {
        /// <summary>
        /// ID for this player, unique to this game.
        /// </summary>
        public int Id;

        /// <summary>
        /// Player's username.
        /// </summary>
        public string Name;

        /// <summary>
        /// List of status effects currently applied to this player.
        /// </summary>
        public List<StatusEffect> StatusEffects;

        /// <summary>
        /// Current position of player.
        /// </summary>
        /// todo: maybe this could be hidden from JSON? Is there any reason we'd send this?
        public LatLng Position;

        /// <summary>
        /// Public position of player.<br />
        /// Null means off-radar.
        /// </summary>
        public LatLng PublicPosition;

        /// <summary>
        /// Token
        /// </summary>
        [JsonIgnore] // For security reasons, this shouldn't be included in JSON
        public string Token;

        /// <summary>
        /// The game this player belongs to.
        /// </summary>
        [JsonIgnore]
        public Game Game;
    }
}
