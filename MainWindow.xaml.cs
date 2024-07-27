using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace reign_of_grelok_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Menu { get; }
        public MainWindow()
        {
            Menu = new ObservableCollection<string> { "Option 1", "Option 2", "Option 3" };
            DataContext = this;
            InitializeComponent();
            MapLocaleName.Content = "TEste";
            MapLocaleName.Content = "TEste";
        }

        private void EventMessageButtonChange(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string option = button.Content.ToString();
                EventDesciptionTextBox.Text = $"el faucibus eros";
            }
        }
    }
}