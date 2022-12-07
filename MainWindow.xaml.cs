using MovieApp.MVVM.View;
using MovieApp.Windows;
using System.Windows;
using System.Windows.Input;

namespace MovieApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppContent.Content = new MovieListing();
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeApp(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (movieListingBtn.IsChecked == true)
            {
                AppContent.Content = new MovieListing();
            }
            else if (randomMovieBtn.IsChecked == true)
            {
                AppContent.Content = new RandomMovie();
            }
            else
            {
                AppContent.Content = new Favorites();
            }
        }

        private void ShowAboutWindow(object sender, MouseButtonEventArgs e)
        {
            About AboutWindow = new About();

            AboutWindow.Show();
        }

        private void ShowDevelopersWindow(object sender, MouseButtonEventArgs e)
        {
            Developers DevelopersWindow = new Developers();

            DevelopersWindow.Show();
        }
    }
}
