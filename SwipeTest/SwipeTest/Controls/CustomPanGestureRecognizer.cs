using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SwipeTest.Controls
{
    public class CustomPanGestureRecognizer : PanGestureRecognizer
    {
        public new event EventHandler<PanUpdatedEventArgs> PanUpdated;

        private readonly Dictionary<int, Tuple<DateTime, object>> _currentGestures;

        public CustomPanGestureRecognizer()
        {
            _currentGestures = new Dictionary<int, Tuple<DateTime, object>>();

            base.PanUpdated += CustomPanGestureRecognizer_PanUpdated;

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 250), () =>
            {
                foreach (var kvp in _currentGestures)
                {
                    var time = kvp.Value.Item1;
                    var sender = kvp.Value.Item2;

                    if (time.AddMilliseconds(500) < DateTime.UtcNow)
                        // If there's been no update in half a second, terminate the gesture
                        PanUpdated.Invoke(sender, new PanUpdatedEventArgs(GestureStatus.Canceled, kvp.Key));
                }
                return true;
            });
        }

        private void CustomPanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                case GestureStatus.Running:
                    _currentGestures[e.GestureId] = new Tuple<DateTime, object>(DateTime.UtcNow, sender);
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    if (_currentGestures.ContainsKey(e.GestureId))
                        _currentGestures.Remove(e.GestureId);
                    break;
            }
            PanUpdated?.Invoke(sender, e);
        }
    }
}
