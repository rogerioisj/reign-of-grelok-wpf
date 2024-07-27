using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.state;

namespace reign_of_grelok_wpf.stages
{
    class Plains
    {
        private Inventory inventoryInstance;
        private Town townInstance;
        private Chapel chapelInstance;
        private Swamp swampInstance;
        private Montainside montainsideInstance;
        private List<StageMenuItem> menu;

        public Plains(Inventory inventoryInstance, Town town, Chapel chapel, Swamp swamp, Montainside montainside)
        {
            this.inventoryInstance = inventoryInstance;
            this.townInstance = town;
            this.chapelInstance = chapel;
            this.swampInstance = swamp;
            this.montainsideInstance = montainside;

            menu = new List<StageMenuItem>();
            menu.Add(new StageMenuItem("Olhar ao redor", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Ir para Norte", _ => this.montainsideInstance.LoadStageInfo(), EventType.Load, true));
            menu.Add(new StageMenuItem("Ir para Sul", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Ir para Leste", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Ir para Oeste", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Inventário", _ => this.inventoryInstance.LoadStageInfo(_ => this.LoadStageInfo()), EventType.Load, true));
        }

        public StageInfo LoadStageInfo()
        {
            var availableMenu = this.getMenuAvailable();
            var stage = new StageInfo("Planicies", availableMenu);

            return stage;
        }

        private Dictionary<string, MenuItem> getMenuAvailable()
        {
            var menuParsed = new Dictionary<string, MenuItem>();
            menu.ForEach(delegate (StageMenuItem item) {
                if (item.isAvailable) menuParsed.Add(item.Title, item.GetMenuItem());
            });

            return menuParsed;
        }

        private string ShowStageMessage()
        {
            return "Você está em uma ampla planície. " +
                "Os contrafortes se estendem ao norte, onde as nuvens se reúnem em torno de um pico sinistro. " +
                "Um caminho de terra serpenteia de uma capela solitária para o leste, " +
                "através das planícies onde você está, e para o sul até uma cidade movimentada. " +
                "Névoas finas se acumulam sobre os pântanos no oeste, " +
                "onde uma torre fina fica sozinha no pântano.";
        }
    }
}
