using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreciseAirCursor.VideoRecognition
    {
    public class AirPointerParameters
        {
        public readonly int DeltaX;

        public readonly int DeltaY;

        public readonly int DeltaRatio;

        public AirPointerParameters(int deltaX, int deltaY, int deltaRatio)
            {
            DeltaX = deltaX;
            DeltaY = deltaY;
            DeltaRatio = deltaRatio;
            }
        }
    }
