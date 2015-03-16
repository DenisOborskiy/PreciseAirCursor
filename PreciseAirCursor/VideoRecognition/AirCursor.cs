using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PreciseAirCursor.VideoRecognition
    {
    public class AirCursor
        {
        private GesturesProvider gesturesProvider;

        public AirCursor()
            {
            gesturesProvider = new GesturesProvider();
            }

        internal void Process(Bitmap bitmap)
            {
            gesturesProvider.Process(bitmap);
            }
        }
    }
