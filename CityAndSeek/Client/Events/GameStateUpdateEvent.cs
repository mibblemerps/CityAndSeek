using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CityAndSeek.Client.Events
{
    public class GameStateUpdateEvent : EventArgs
    {
        public Common.Game Game;

        public GameStateUpdateEvent(Common.Game game)
        {
            Game = game;
        }
    }
}