using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PreciseAirCursor.VideoRecognition;

namespace PreciseAirCursor
    {
    public partial class MainForm : Form
        {
        private VideoFrameProvider videoFrameProvider;
        private AirCursor airCursor;

        public MainForm()
            {
            InitializeComponent();
            Left = 0;
            Top = 0;
            }

        private void MainForm_Load(object sender, EventArgs e)
            {
            videoFrameProvider = new VideoFrameProvider();
            if (!videoFrameProvider.Start())
                {
                Close();
                return;
                }

            videoFrameProvider.OnNewFrameObtained += videoFrameProvider_OnNewFrameObtained;
            videoFrameProvider.OnFramesPerSecondRateUpdated += setFramesPerSecond;

            airCursor = new AirCursor();
            }

        void setFramesPerSecond(int framesPerSecond)
            {
            if (this.InvokeRequired)
                {
                this.Invoke(new Action<int>(setFramesPerSecond), new object[] { framesPerSecond });
                }

            framesPerSecondLabel.Text = framesPerSecond.ToString();
            }

        void videoFrameProvider_OnNewFrameObtained(Bitmap obj)
            {
            //var bitmap = obj.Clone();
            airCursor.Process(obj);

            videoPictureBox.Image = obj;
            }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
            {
            videoFrameProvider.Stop();
            }
        }
    }
