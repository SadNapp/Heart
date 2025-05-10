using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Form1 : Form
    {
        private List<float> _heartRadii; 
        private List<bool> _shrinkingList; 
        private List<Thread> _animationThreads; 
        private List<Color> _heartColors; 
        private System.ComponentModel.IContainer _components = null;

        private const int HEARTS_COUNT = 5; 
        private const int FORM_WIDTH = 800; 
        private const int FORM_HEIGHT = 450;

        public Form1()
        {
            InitializeComponent();
           
            this.DoubleBuffered = true;
            this.Paint += Form1_Paint; 
            this.ClientSize = new Size(FORM_WIDTH, FORM_HEIGHT); 

           
            _heartRadii = new List<float>();
            _shrinkingList = new List<bool>();
            _animationThreads = new List<Thread>();
            _heartColors = new List<Color>();

            Random random = new Random();

           
            for (int i = 0; i < HEARTS_COUNT; i++)
            {
                _heartRadii.Add(Math.Min(FORM_WIDTH, FORM_HEIGHT) / 4); 
                _shrinkingList.Add(true); 
                _heartColors.Add(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))); 

                
                Thread animationThread = new Thread(AnimateHeart);
                animationThread.IsBackground = true; 
                _animationThreads.Add(animationThread);
                animationThread.Start(i); 
            }

            
            System.Windows.Forms.Timer colorTimer = new System.Windows.Forms.Timer();
            colorTimer.Interval = 1000; 
            colorTimer.Tick += ColorTimer_Tick;
            colorTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
            Graphics g = e.Graphics;
            g.Clear(BackColor); 

           
            int formWidth = ClientSize.Width;
            int formHeight = ClientSize.Height;

           
            int centerX = formWidth / 2;
            int centerY = formHeight / 2;

            
            for (int i = 0; i < HEARTS_COUNT; i++)
            {
                
                Pen pen = new Pen(_heartColors[i], 2);
                SolidBrush brush = new SolidBrush(_heartColors[i]);

                
                DrawHeart(g, centerX, centerY, _heartRadii[i], pen, brush);
                pen.Dispose();
                brush.Dispose();
            }
           
        }

        private void AnimateHeart(object obj)
        {
            int heartIndex = (int)obj; 

            while (true) 
            {
               
                if (_shrinkingList[heartIndex])
                {
                    _heartRadii[heartIndex] -= 5; 
                    if (_heartRadii[heartIndex] <= 0)
                    {
                        _shrinkingList[heartIndex] = false;
                    }
                }
                else
                {
                    _heartRadii[heartIndex] += 5; 
                    if (_heartRadii[heartIndex] >= Math.Min(ClientSize.Width, ClientSize.Height) / 4) 
                    {
                        _shrinkingList[heartIndex] = true;
                    }
                }

               
                if (IsHandleCreated) 
                {
                    try
                    {
                        Invoke(new MethodInvoker(Invalidate)); 
                    }
                    catch (ObjectDisposedException)
                    {
                        
                        return; 
                    }
                }

                Thread.Sleep(50); 
            }
        }

        private void DrawHeart(Graphics g, int centerX, int centerY, float radius, Pen pen, Brush brush)
        {
            
            float x, y;
            GraphicsPath path = new GraphicsPath();

            for (double t = 0; t < 2 * Math.PI; t += 0.01) 
            {
                x = (float)(16 * Math.Pow(Math.Sin(t), 3));
                y = (float)(-13 * Math.Cos(t) + 5 * Math.Cos(2 * t) + 2 * Math.Cos(3 * t) + Math.Cos(4 * t)); 

                float pointX = centerX + radius * x / 16;
                float pointY = centerY + radius * y / 16;

                if (t == 0)
                    path.AddLine(pointX, pointY, pointX, pointY);
                else
                    path.AddLine(pointX, pointY, pointX, pointY);
            }
            path.CloseFigure();
            g.FillPath(brush, path);
            g.DrawPath(pen, path);
        }
       
        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            for (int i = 0; i < HEARTS_COUNT; i++)
            {
                _heartColors[i] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }

            if (IsHandleCreated)
            {
                try
                {
                    Invoke(new MethodInvoker(Invalidate));
                }
                catch (ObjectDisposedException)
                {
                   
                }
            }
        }
    }
}