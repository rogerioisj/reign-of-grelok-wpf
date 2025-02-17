﻿using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.stages;
using reign_of_grelok_wpf.state;
using System.Collections.ObjectModel;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace reign_of_grelok_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
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
            town = new Town(inventory, stateManagement, MenuUpdated);
            chapel = new Chapel(inventory, stateManagement, MenuUpdated);
            swamp = new Swamp(inventory, stateManagement, MenuUpdated);
            montainside = new Montainside(inventory, stateManagement, MenuUpdated);
            plains = new Plains(inventory, town, chapel, swamp, montainside);

            stageInfo = plains.LoadStageInfo();

            Menu = new ObservableCollection<string>(stageInfo.GetMenu());
            DataContext = this;

            InitializeComponent();

            MapLocaleName.Content = stageInfo.GetStageName();
        }

        /// <summary>
        /// Function to get updates for menu.
        /// </summary>
        /// <param name="sender">Object whos call this function.</param>
        /// <param name="e">Event param object. Ignore here.</param>
        private void MenuUpdated(object sender, EventArgs e)
        {
            if (stateManagement.AlreadyFinishedGame())
            {
                Menu.Clear();
                MapLocaleName.Content = "FIM!";
                return;
            }

            stageInfo = ((IStage)sender).LoadStageInfo(_ => plains.LoadStageInfo());

            Menu.Clear();
            foreach (var menuItem in stageInfo.GetMenu())
            {
                Menu.Add(menuItem);
            }
        }

        /// <summary>
        /// Method to handle with button clicks
        /// </summary>
        /// <param name="sender">The button element</param>
        /// <param name="e">Event param object. Ignore here.</param>
        private async void EventMessageButtonChange(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string key = button.Content.ToString();

                var type = stageInfo.GetMenuItemEventType(key);

                if (type == EventType.Text)
                {
                    var action = stageInfo.GetShowTextAction(key);
                    var eventText = action(null);
                    await ShowTextWithTypingEffect(eventText);
                    return;
                }

                if (type == EventType.Load)
                {
                    var action = stageInfo.GetLoadStageAction(key);
                    stageInfo = action(null);

                    MapLocaleName.Content = stageInfo.GetStageName();

                    Menu.Clear();

                    foreach (var menuItem in stageInfo.GetMenu())
                    {
                        Menu.Add(menuItem);
                    }

                    EventDesciptionTextBox.Text = "";

                    return;
                }
            }
        }

        /// <summary>
        /// Method to show text in `EventDesciptionTextBox` element with typewriter effect.
        /// </summary>
        /// <param name="text">Text to be presented</param>
        /// <returns></returns>
        private async Task ShowTextWithTypingEffect(string text)
        {
            EventDesciptionTextBox.Text = string.Empty;

            foreach (char c in text)
            {
                EventDesciptionTextBox.Text += c;
                await Task.Delay(50);
            }
        }
    }
}