using SpaceInvader.Game;
using System;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SpaceInvader
{
    public class SpaceInvaderGame : UIElement, IDisposable
    {
        private DrawingGroup backingStore;
        private Timer timer;
        private Level level;
        public double Width { get; set; }
        public double Height { get; set; }

        public SpaceInvaderGame()
        {
            timer = new Timer(250);

            timer.Elapsed += Rendering;
            backingStore = new DrawingGroup();
            CompositionTarget.Rendering += Rendering;
            InitGame();
            Render();
            Start();
        }

        public SpaceInvaderGame(double width, double height)
            :this()
        {
            this.Width = width;
            this.Height = height;
        }
        
        public void Start()
        {
            timer.Start();
        }

        internal void KeyPressed(object sender, KeyEventArgs e)
        {
            level.KeyPressed(sender, e);
        }

        public void InitGame()
        {
            level = new Level(Width, Height);
        }

        private void Rendering(object sender, EventArgs e)
        {
            Render();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Render();

            base.OnRender(drawingContext);
            drawingContext.DrawDrawing(backingStore);
        }

        public void Render()
        {
            Dispatcher.Invoke(() =>
            {
                var drawingContext = backingStore.Open();
                Render(drawingContext);
                drawingContext.Close();
            });
        }
        
        private void Render(DrawingContext drawingContext)
        {
            level.Draw(drawingContext);
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
