using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using AForge.Video.DirectShow;
using PreciseAirCursor.Extentions;

namespace PreciseAirCursor.VideoRecognition
    {
    class VideoFrameProvider
        {
        private Stopwatch stopWatch;
        private int framesCount;
        private int processedImages;
        private object locker = new object();

        public event Action<Bitmap> OnNewFrameObtained = delegate { };
        public event Action<int> OnFramesPerSecondRateUpdated = delegate { };

        private VideoCaptureDevice videoSource;
        private volatile bool isProcessing;

        public bool Start()
            {
            videoSource = new CameraGetter().GetSelectedVideoSource();
            if (videoSource == null)
                {
                "Web camera wasn't found!".NotifyAboutError();
                return false;
                }

            videoSource.VideoResolution = initVideoResolution(videoSource.VideoCapabilities);
            if (videoSource.VideoResolution == null)
                {
                "Web camera has too low resolution!".NotifyAboutError();
                return false;
                }

            videoSource.NewFrame += videoSource_NewFrame;
            videoSource.Start();

            stopWatch = new Stopwatch();
            stopWatch.Start();

            return true;
            }

        public void Stop()
            {
            if (!videoIsActive)
                {
                return;
                }

            videoSource.SignalToStop();
            videoSource = null;
            }

        private bool videoIsActive { get { return videoSource != null && videoSource.IsRunning; } }

        private VideoCapabilities initVideoResolution(VideoCapabilities[] videoCapabilities)
            {
            VideoCapabilities lowestAcceptableResolutionCapability = null;

            int MIN_SQUARE_SIZE_PX = 640;
            MIN_SQUARE_SIZE_PX = 352;

            var lowestResolution = new Size(int.MaxValue, int.MaxValue);
            for (int capabilityIndex = 0; capabilityIndex < videoCapabilities.Length; capabilityIndex++)
                {
                Size size = videoCapabilities[capabilityIndex].FrameSize;
                if (size.Width < MIN_SQUARE_SIZE_PX) continue;

                if (lowestResolution.Width > size.Width || lowestResolution.Height > size.Height)
                    {
                    lowestResolution = size;
                    lowestAcceptableResolutionCapability = videoCapabilities[capabilityIndex];
                    }
                }

            return lowestAcceptableResolutionCapability;
            }

        private void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
            {
            if (isProcessing)
                {
                return;
                }

            lock (locker)
                {
                isProcessing = true;

                OnNewFrameObtained(eventArgs.Frame.Clone() as Bitmap);

                processedImages++;
                if (stopWatch.ElapsedMilliseconds > 1000)
                    {
                    OnFramesPerSecondRateUpdated(processedImages);
                    processedImages = 0;
                    stopWatch.Restart();
                    }

                isProcessing = false;
                }

            framesCount++;
            if (framesCount > 500)
                {
                framesCount = 1;
                System.GC.Collect();
                }
            }
        }
    }
