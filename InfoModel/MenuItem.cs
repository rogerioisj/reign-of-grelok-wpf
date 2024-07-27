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
        public string Title { get; }
        private readonly ShowTextAction? TextAction;
        private readonly LoadStageAction? LoadAction;
        public EventType EventType { get; }

        public MenuItem(string title, ShowTextAction action, EventType eventType)
        {
            this.Title = title;
            this.TextAction = action;
            this.EventType = eventType;
            this.LoadAction = null;
        }

        public MenuItem(string title, LoadStageAction action, EventType eventType)
        {
            this.Title = title;
            this.TextAction = null;
            this.EventType = eventType;
            this.LoadAction = action;
        }

        public ShowTextAction getTextAction() 
        {
            if (this.TextAction == null) throw new InvalidOperationException("Method calls for an invalid type");

            return this.TextAction;
        }

        public LoadStageAction getLoadAction()
        {
            if (this.LoadAction == null) throw new InvalidOperationException("Method calls for an invalid type");

            return this.LoadAction;
        }
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
