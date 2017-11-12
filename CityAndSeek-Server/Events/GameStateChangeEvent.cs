using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityAndSeek.Common;

namespace CityAndSeek.Server.Events
{
    public class GameStateChangeEvent : EventArgs
    {
        public GameState OldState;
        public GameState NewState;

        public GameStateChangeEvent(GameState oldState, GameState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
