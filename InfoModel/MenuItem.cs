using reign_of_grelok_wpf.stages;

namespace reign_of_grelok_wpf.infoModel
{
    class MenuItem
    {
        private readonly string Title;
        private readonly MenuAction Action;

        public MenuItem(string title, MenuAction action)
        {
            this.Title = title;
            this.Action = action;
        }

        public string getTitle() { return this.Title; }

        public MenuAction getAction() { return this.Action; }
    }

    class StageMenuItem : MenuItem
    {
        public bool isAvailable {  get; }

        public StageMenuItem(string title, MenuAction action, bool isAvailable) : base(title, action)
        {
            this.isAvailable = isAvailable;
        }

        public MenuItem GetMenuItem() { return this; }
    }
}
