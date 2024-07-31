using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.state;

namespace reign_of_grelok_wpf.stages
{
    /// <summary>
    /// Plains Stage
    /// </summary>
    class Plains
    {
        private Inventory inventoryInstance;
        private Town townInstance;
        private Chapel chapelInstance;
        private Swamp swampInstance;
        private Montainside montainsideInstance;
        private List<StageMenuItem> menuItemList;
        private Dictionary<string, MenuItem> menuToBeExported;

        /// <summary>
        /// Plains stage constructor
        /// </summary>
        /// <param name="inventoryInstance">An inventory instance</param>
        /// <param name="town">A Town stage instance</param>
        /// <param name="chapel">A Chapel stage instance</param>
        /// <param name="swamp">A Swamp stage instance</param>
        /// <param name="montainside">A montainside stage instance</param>
        public Plains(Inventory inventoryInstance, Town town, Chapel chapel, Swamp swamp, Montainside montainside)
        {
            this.inventoryInstance = inventoryInstance;
            this.townInstance = town;
            this.chapelInstance = chapel;
            this.swampInstance = swamp;
            this.montainsideInstance = montainside;

            menuToBeExported = new Dictionary<string, MenuItem>();
            menuItemList = new List<StageMenuItem>();
            menuItemList.Add(new StageMenuItem("Olhar ao redor", _ => this.ShowStageMessage(), EventType.Text, true));
            menuItemList.Add(new StageMenuItem("Ir para Norte", _ => this.montainsideInstance.LoadStageInfo(_ => this.LoadStageInfo()), EventType.Load, true));
            menuItemList.Add(new StageMenuItem("Ir para Sul", _ => this.townInstance.LoadStageInfo(_ => this.LoadStageInfo()), EventType.Load, true));
            menuItemList.Add(new StageMenuItem("Ir para Leste", _ => this.chapelInstance.LoadStageInfo(_ => this.LoadStageInfo()), EventType.Load, true));
            menuItemList.Add(new StageMenuItem("Ir para Oeste", _ => this.swampInstance.LoadStageInfo(_ => this.LoadStageInfo()), EventType.Load, true));
            menuItemList.Add(new StageMenuItem("Inventário", _ => this.inventoryInstance.LoadStageInfo(_ => this.LoadStageInfo()), EventType.Load, true));
        }

        /// <summary>
        /// A method to load stage infos.
        /// </summary>
        /// <returns>StageInfo instance</returns>
        public StageInfo LoadStageInfo()
        {
            var availableMenu = this.getMenuAvailable();
            var stage = new StageInfo("Planicies", availableMenu);

            return stage;
        }

        /// <summary>
        /// Method to get all menu options available.
        /// </summary>
        /// <returns>A Dictionary<string, MenuItem> instance</returns>
        private Dictionary<string, MenuItem> getMenuAvailable()
        {
            menuToBeExported = new Dictionary<string, MenuItem>();
            menuItemList.ForEach(delegate (StageMenuItem item) {
                if (item.isAvailable) menuToBeExported.Add(item.Title, item.GetMenuItem());
            });

            return menuToBeExported;
        }

        /// <summary>
        /// A method to present the stage message and load the new menu options
        /// </summary>
        /// <returns>Stage description</returns>
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
