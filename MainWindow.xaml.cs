using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.stages;
using reign_of_grelok_wpf.state;
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
        public ObservableCollection<string> Menu { get; private set; }
        private Management stateManagement;
        private Inventory inventory;
        private Town town;
        private Chapel chapel;
        private Swamp swamp;
        private Montainside montainside;
        private Plains plains;
        private StageInfo stageInfo;
        public MainWindow()
        {
            inventory = new Inventory();
            stateManagement = new Management();
            town = new Town(inventory, stateManagement);
            chapel = new Chapel(inventory, stateManagement);
            swamp = new Swamp(inventory, stateManagement);
            montainside = new Montainside(inventory, stateManagement);
            plains = new Plains(inventory, town, chapel, swamp, montainside);

            stageInfo = plains.LoadStageInfo();

            Menu = new ObservableCollection<string>(stageInfo.GetMenu());
            DataContext = this;

            InitializeComponent();

            MapLocaleName.Content = stageInfo.GetStageName();
        }

        private void EventMessageButtonChange(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string key = button.Content.ToString();

                var type = stageInfo.GetMenuItemEventType(key);

                if (type == EventType.Text)
                {
                    var action = stageInfo.GetShowTextAction(key);
                    EventDesciptionTextBox.Text = action(null);
                    return;
                }

                if (type == EventType.Load)
                {
                    var action = stageInfo.GetLoadStageAction(key);
                    stageInfo = action();

                    MapLocaleName.Content = stageInfo.GetStageName();

                    Menu.Clear();

                    foreach (var menuItem in stageInfo.GetMenu())
                    {
                        Menu.Add(menuItem);
                    }
                    return;
                }
            }
        }
    }
}