using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video.DirectShow;

namespace PreciseAirCursor.VideoRecognition
    {
    public class CameraGetter
        {
        public VideoCaptureDevice GetSelectedVideoSource()
            {
            var videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videosources == null || videosources.Count == 0) return null;

            var monikerString = string.Empty;

            // choose camera 
            // ...

            if (string.IsNullOrEmpty(monikerString))
                {
                monikerString = videosources[0].MonikerString;
                }

            return new VideoCaptureDevice(monikerString);
            }
        }
    }
