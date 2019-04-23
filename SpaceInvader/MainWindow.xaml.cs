using System;
using System.Windows;

namespace SpaceInvader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {

        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;
            
            PreviewKeyDown += graphicsContext.KeyPressed;
            Closing += MainWindow_Closing;
            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            graphicsContext.Dispose();
            graphicsContext = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            graphicsContext.Height = ActualHeight;
            graphicsContext.Width = ActualWidth;
            graphicsContext.InitGame();
            graphicsContext.Start();
        }
    }
}
