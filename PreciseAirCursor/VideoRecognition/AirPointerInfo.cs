using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge;

namespace PreciseAirCursor.VideoRecognition
    {
    public class AirPointerInfo
        {
        public int X { get; private set; }

        public int Y { get; private set; }

        public int SizeFactor { get; private set; }

        public double Confidence { get; set; }

        public override bool Equals(object obj)
            {
            if (obj == null || !(obj is AirPointerInfo))
                {
                return false;
                }

            var secondAirPointerInfo = (AirPointerInfo)obj;

            return secondAirPointerInfo.X == X
                   && secondAirPointerInfo.Y == Y
                   && secondAirPointerInfo.SizeFactor == SizeFactor;
            }

        public override string ToString()
            {
            return string.Format("{0},{1}    {2}; {3}", X, Y, SizeFactor, Math.Round(Confidence * 100.0));
            }

        public AirPointerInfo(List<IntPoint> points)
            {
            if (points == null || points.Count != 4)
                {
                throw new ArgumentException("Square has four vertices, four point are required");
                }
            init(points);
            }

        private void init(List<IntPoint> points)
            {
            var minX = int.MaxValue;
            var minY = int.MaxValue;
            var maxFactor = int.MinValue;

            for (var pointIndex = 0; pointIndex < points.Count; pointIndex++)
                {
                var currentPoint = points[pointIndex];

                if (currentPoint.X < minX)
                    {
                    minX = currentPoint.X;
                    }

                if (currentPoint.Y < minY)
                    {
                    minY = currentPoint.Y;
                    }

                var nextPoint = points[pointIndex == (points.Count - 1) ? 0 : pointIndex + 1];
                var hypotenuseSquare = (nextPoint.X - currentPoint.X) * (nextPoint.X - currentPoint.X)
                                       + (nextPoint.Y - currentPoint.Y) * (nextPoint.Y - currentPoint.Y);
                if (maxFactor < hypotenuseSquare)
                    {
                    maxFactor = hypotenuseSquare;
                    }
                }

            SizeFactor = maxFactor;
            X = minX;
            Y = minY;
            }
        }
    }
