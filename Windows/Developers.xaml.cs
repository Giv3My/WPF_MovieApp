using System.Windows;
using System.Windows.Input;

namespace MovieApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для Developers.xaml
    /// </summary>
    public partial class Developers : Window
    {
        public Developers()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
