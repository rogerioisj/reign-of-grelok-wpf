using System;
using System.Collections;
using System.Linq;
using reign_of_grelok_wpf.stages;

namespace reign_of_grelok_wpf.infoModel
{
    class StageInfo
    {
        private string stageName;
        private Dictionary<string, MenuItem> menu;

        public StageInfo(string name, Dictionary<string, MenuItem> menu) 
        {
            this.stageName = name;
            this.menu = menu;
        }

        public string GetStageName() { return stageName; }

        public List<string> GetMenu() { return menu.Keys.Cast<string>().ToList(); }

        public MenuAction GetMenuItemAction(string name) 
        {
            var item = menu[name];
            return item.getAction();
        }

        public EventType GetMenuItemEventType(string name)
        {
            var item = menu[name];
            return item.EventType;
        }
    }
}
