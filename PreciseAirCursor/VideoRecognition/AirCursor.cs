using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PreciseAirCursor.Extentions;

namespace PreciseAirCursor.VideoRecognition
    {
    public class AirCursor
        {
        private MotionsProvider motionsProvider;
        private int currentRatio;
        private bool leftButtonPressed;

        public AirCursor()
            {
            motionsProvider = new MotionsProvider();
            motionsProvider.OnAirMotion += gesturesProvider_OnAirMotion;
            }

        void gesturesProvider_OnAirMotion(AirPointerParameters airDelta)
            {
            const double ratio = 2.5;
            var position = Cursor.Position;
            position.X += (int)(ratio * airDelta.DeltaX);
            position.Y += (int)(ratio * airDelta.DeltaY);

            Cursor.Position = position;

            updateButtonsState(airDelta.DeltaRatio);
            }

        private void updateButtonsState(int deepRatio)
            {
            if (currentRatio > 0)
                {
                bool leftButtonDown = deepRatio < 1000;
                if (leftButtonDown && !leftButtonPressed)
                    {
                    leftButtonPressed = true;
                    MouseHelper.LeftButtonDown();
                    }
                else if (leftButtonPressed && !leftButtonDown)
                    {
                    leftButtonPressed = false;
                    MouseHelper.LeftButtonUp();
                    }
                }

            currentRatio = deepRatio;
            }

        internal void Process(Bitmap bitmap)
            {
            motionsProvider.Process(bitmap);
            }
        }
    }
