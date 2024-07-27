using reign_of_grelok_wpf.stages;

namespace reign_of_grelok_wpf.infoModel
{
    enum EventType
    {
        Text,
        Load
    }
    class MenuItem
    {
        private readonly string Title;
        private readonly ShowTextAction Action;
        public EventType EventType { get; }

        public MenuItem(string title, ShowTextAction action, EventType eventType)
        {
            this.Title = title;
            this.Action = action;
            this.EventType = eventType;
        }

        public string getTitle() { return this.Title; }

        public ShowTextAction getAction() { return this.Action; }
    }

    class StageMenuItem : MenuItem
    {
        public bool isAvailable {  get; }

        public StageMenuItem(string title, ShowTextAction action, EventType eventType, bool isAvailable) : base(title, action, eventType)
        {
            this.isAvailable = isAvailable;
        }

        public MenuItem GetMenuItem() { return this; }
    }
}
