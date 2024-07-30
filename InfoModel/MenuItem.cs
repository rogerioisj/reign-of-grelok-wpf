using reign_of_grelok_wpf.stages;

namespace reign_of_grelok_wpf.infoModel
{
    /// <summary>
    /// Enum to handle with event types.
    /// </summary>
    enum EventType
    {
        Text,
        Load
    }

    /// <summary>
    /// Class to handle with options presented in button menu
    /// </summary>
    class MenuItem
    {
        public string Title { get; }
        private readonly ShowTextAction? TextAction;
        private readonly LoadStageAction? LoadAction;
        public EventType EventType { get; }

        /// <summary>
        /// Constructor to MenuItem class for case of action be type of ShowTextAction
        /// </summary>
        /// <param name="title">Title for menu item</param>
        /// <param name="action">Method to be called when the button is clicked</param>
        /// <param name="eventType">Type of event when button is clicker</param>
        public MenuItem(string title, ShowTextAction action, EventType eventType)
        {
            this.Title = title;
            this.TextAction = action;
            this.EventType = eventType;
            this.LoadAction = null;
        }

        /// <summary>
        /// Constructor to MenuItem class for case of action be type of ShowTextAction
        /// </summary>
        /// <param name="title">Title for menu item</param>
        /// <param name="action">Method to be called when the button is clicked</param>
        /// <param name="eventType">Type of event when button is clicker</param>
        public MenuItem(string title, LoadStageAction action, EventType eventType)
        {
            this.Title = title;
            this.TextAction = null;
            this.EventType = eventType;
            this.LoadAction = action;
        }

        /// <summary>
        /// Function to returns the action based on show a text.
        /// </summary>
        /// <returns>Method to be called</returns>
        /// <exception cref="InvalidOperationException">Case the EventType does not correspond to the a text action</exception>
        public ShowTextAction getTextAction() 
        {
            if (this.TextAction == null) throw new InvalidOperationException("Method calls for an invalid type");

            return this.TextAction;
        }

        /// <summary>
        /// Function to returns the action based on load a new menu.
        /// </summary>
        /// <returns>Method to be called</returns>
        /// <exception cref="InvalidOperationException">Case the EventType does not correspond to the a load action</exception>
        public LoadStageAction getLoadAction()
        {
            if (this.LoadAction == null) throw new InvalidOperationException("Method calls for an invalid type");

            return this.LoadAction;
        }
    }

    /// <summary>
    /// Class to handle with menu itens only for stage cases. Extends from MenuItem.
    /// </summary>
    class StageMenuItem : MenuItem
    {
        public bool isAvailable { get; set; }

        /// <summary>
        /// Constructor for StageMenuItem.
        /// </summary>
        /// <param name="title">Title for menu item</param>
        /// <param name="action"></param>
        /// <param name="eventType"></param>
        /// <param name="isAvailable"></param>
        public StageMenuItem(string title, ShowTextAction action, EventType eventType, bool isAvailable) : base(title, action, eventType)
        {
            this.isAvailable = isAvailable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">Title for menu item</param>
        /// <param name="action">Method to be called when the button is clicked</param>
        /// <param name="eventType">Type of event when button is clicker</param>
        /// <param name="isAvailable">Set if the item is available or not</param>
        public StageMenuItem(string title, LoadStageAction action, EventType eventType, bool isAvailable) : base(title, action, eventType)
        {
            this.isAvailable = isAvailable;
        }

        /// <summary>
        /// Method to get MenuItem instance
        /// </summary>
        /// <returns>A MenuItem instance</returns>
        public MenuItem GetMenuItem() { return this; }
    }
}
