using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CityAndSeek.Helpers
{
    public class DelayedAction
    {
        private bool _aborted = false;

        public Action Action { get; private set; }

        public DelayedAction(Action action)
        {
            Action = action;
        }

        public async Task Run(int delay)
        {
            await Task.Delay(delay);

            if (!_aborted)
                Action();
        }
        
        public void Abort()
        {
            _aborted = true;
        }
    }
}