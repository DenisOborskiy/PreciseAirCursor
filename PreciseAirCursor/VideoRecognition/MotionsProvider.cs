using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using AForge;
using AForge.Helpers;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using GlyphRecognitionProto;

namespace PreciseAirCursor.VideoRecognition
    {
    public class MotionsProvider
        {
        private AirPointerInfo lastAirPointerInfo;

        internal void Process(Bitmap bitmap)
            {
            var airPointerInfo = process(bitmap);
            if (airPointerInfo == null || airPointerInfo.Equals(lastAirPointerInfo))
                {
                return;
                }

            Trace.WriteLine(airPointerInfo);

            if (lastAirPointerInfo != null
                && (DateTime.Now - lastEventTime).TotalMilliseconds < 700)
                {
                OnAirMotion(new AirPointerParameters(
                    lastAirPointerInfo.X - airPointerInfo.X,
                    airPointerInfo.Y - lastAirPointerInfo.Y,
                    airPointerInfo.SizeFactor));
                }

            lastEventTime = DateTime.Now;
            lastAirPointerInfo = airPointerInfo;
            }

        private const int GLYPH_RESOLUTION_WIDTH = 2;
        private const double MIN_CONFIDENCE = 0.6;

        private AirPointerInfo process(Bitmap image)
            {
            Bitmap grayImage = Grayscale.CommonAlgorithms.BT709.Apply(image);

            Bitmap edges = new DifferenceEdgeDetector().Apply(grayImage);

            const int THRESHOLD = 40;
            new Threshold(THRESHOLD).ApplyInPlace(edges);

            var blobCounter = new BlobCounter();
            blobCounter.MinHeight = 32;
            blobCounter.MinWidth = 32;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            blobCounter.ProcessImage(edges);
            Blob[] blobs = blobCounter.GetObjectsInformation();

            // lock grayscale image, so we could access it's pixel values
            BitmapData grayData = grayImage.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, grayImage.PixelFormat);
            var grayUI = new UnmanagedImage(grayData);

            // list of found dark/black quadrilaterals surrounded by white area
            var foundObjects = new List<List<IntPoint>>();

            // shape checker for checking quadrilaterals
            var shapeChecker = new SimpleShapeChecker();

            // 5 - check each blob
            for (int i = 0, n = blobs.Length; i < n; i++)
                {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);
                List<IntPoint> corners = null;

                // does it look like a quadrilateral ?
                if (shapeChecker.IsQuadrilateral(edgePoints, out corners))
                    {
                    // if ( CheckIfShapeIsAcceptable( corners ) )
                        {
                        // get edge points on the left and on the right side
                        List<IntPoint> leftEdgePoints, rightEdgePoints;
                        blobCounter.GetBlobsLeftAndRightEdges(blobs[i], out leftEdgePoints, out rightEdgePoints);

                        // calculate average difference between pixel values from outside of the shape and from inside
                        float diff = grayUI.CalculateAverageEdgesBrightnessDifference(leftEdgePoints, rightEdgePoints);

                        const int EDGES_BRIGHTNESS_DIFFERENCE_TO_IGNORE = 20;
                        // check average difference, which tells how much outside is lighter than inside on the average
                        if (diff > EDGES_BRIGHTNESS_DIFFERENCE_TO_IGNORE)
                            {
                            foundObjects.Add(corners);
                            }
                        }
                    }
                }

            grayImage.UnlockBits(grayData);

            const int SQUARE_SIZE = 500;
            // further processing of each potential glyph
            foreach (List<IntPoint> corners in foundObjects)
                {
                // 6 - do quadrilateral transformation
                QuadrilateralTransformation quadrilateralTransformation =
                    new QuadrilateralTransformation(corners, SQUARE_SIZE, SQUARE_SIZE);

                Bitmap transformed = quadrilateralTransformation.Apply(grayImage);

                // 7 - otsu thresholding
                var otsuThresholdFilter = new OtsuThreshold();
                Bitmap transformedOtsu = otsuThresholdFilter.Apply(transformed);
                // log.AddImage("Transformed Otsu #" + counter, transformedOtsu);

                int glyphSize = GLYPH_RESOLUTION_WIDTH + 2;
                var gr = new SquareBinaryGlyphRecognizer(glyphSize);

                bool[,] glyphValues = gr.Recognize(transformedOtsu,
                    new Rectangle(0, 0, SQUARE_SIZE, SQUARE_SIZE));

                if (gr.confidence < MIN_CONFIDENCE || !rightHandGlyph(glyphValues)) continue;

                var result = new AirPointerInfo(corners) { Confidence = gr.confidence };
                return result;
                }

            return null;
            }

        private bool rightHandGlyph(bool[,] glyphValues)
            {
            var totalResolution = GLYPH_RESOLUTION_WIDTH
                                  + 2; // border left + border right

            if (glyphValues.GetLength(0) != totalResolution
                || glyphValues.GetLength(1) != totalResolution)
                {
                return false;
                }

            for (int i = 0; i < totalResolution; i++)
                {
                if (glyphValues[0, i]
                    || glyphValues[i, 0]
                    || glyphValues[totalResolution - 1, i]
                    || glyphValues[i, totalResolution - 1]) return false;
                }

            var oneCount = 0;
            if (glyphValues[1, 1])
                {
                oneCount++;
                }
            if (glyphValues[2, 2])
                {
                oneCount++;
                }
            if (glyphValues[2, 1])
                {
                oneCount++;
                }
            if (glyphValues[1, 2])
                {
                oneCount++;
                }

            return oneCount == 1;
            }

        public event Action<AirPointerParameters> OnAirMotion = delegate { };
        private DateTime lastEventTime;
        }
    }
