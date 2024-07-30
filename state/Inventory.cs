using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.stages;

namespace reign_of_grelok_wpf.state
{
    /// <summary>
    /// Delgate to handle with back action.
    /// </summary>
    /// <param name="key">Optional parameter for return stages except for plains.</param>
    delegate void CallbackStageMenu(char? key);

    /// <summary>
    /// Logical behavior for inventory.
    /// </summary>
    class Inventory
    {
        private InventoryItem rustSword;
        private InventoryItem drinkingFlask;
        private InventoryItem zombieHead;
        private InventoryItem refinedGemStone;
        private InventoryItem magicalShard;
        private InventoryItem magicSword;
        private InventoryItem brassKey;
        private InventoryItem rawGemStone;
        private InventoryItem fullFlask;
        private Dictionary<string, InventoryItem> itens;

        public Inventory()
        {
            rustSword = new InventoryItem("Espada enferrujada", "Sua arma. Enferrujada, mas confiável.", true);
            drinkingFlask = new InventoryItem("Frasco de bebida", "Um frasco muito pequeno para transportar água.", true);
            zombieHead = new InventoryItem("Cabeça de zumbi", "O cheiro pode torná-lo impopular...", false);
            refinedGemStone = new InventoryItem("Pedra preciosa refinada", "Uma pedra preciosa brilhante e facetada.", false);
            magicalShard = new InventoryItem("Fragmento mágico", "O fragmento de gema pulsa com luz mágica...", false);
            magicSword = new InventoryItem("Espada mágica", "Uma arma encantada para derrotar Grelok!", false);
            brassKey = new InventoryItem("Chave de latão", "Chave dada a você pelo padre.", false);
            rawGemStone = new InventoryItem("Pedra preciosa bruta", "Esta pedra preciosa pode ser valiosa...", false);
            fullFlask = new InventoryItem("Frasco de bebida cheio", "Seu Frasco está cheio de água benta.", false);


            itens = new Dictionary<string, InventoryItem>();
            itens.Add(rustSword.name, rustSword);
            itens.Add(drinkingFlask.name, drinkingFlask);
            itens.Add(zombieHead.name, zombieHead);
            itens.Add(refinedGemStone.name, refinedGemStone);
            itens.Add(magicalShard.name, magicalShard);
            itens.Add(magicSword.name, magicSword);
            itens.Add(brassKey.name, brassKey);
            itens.Add(rawGemStone.name, rawGemStone);
            itens.Add(fullFlask.name, fullFlask);
        }

        /// <summary>
        /// Handles with menu to be presented.
        /// </summary>
        /// <param name="backEvent">Method to be called at when back button be pressed.</param>
        /// <returns></returns>
        public StageInfo LoadStageInfo(LoadStageAction backEvent)
        {
            var availableMenu = this.getMenuAvailable(backEvent);
            var stage = new StageInfo("Inventário", availableMenu);

            return stage;
        }

        /// <summary>
        /// Method to load only available options for menu.
        /// </summary>
        /// <param name="backEvent"></param>
        /// <returns></returns>
        private Dictionary<string, MenuItem> getMenuAvailable(LoadStageAction backEvent)
        {
            var menuParsed = new Dictionary<string, MenuItem>();

            itens.Keys.ToList().ForEach(delegate (string key) {
                if (itens[key].available)
                    menuParsed.Add(itens[key].name, new MenuItem(itens[key].name, _ => this.showItemDescription(itens[key].name), EventType.Text));
            });

            menuParsed.Add("Voltar", new MenuItem("Voltar", backEvent, EventType.Load));

            return menuParsed;
        }

        /// <summary>
        /// Method to show item description.
        /// </summary>
        /// <param name="key">Dictionary key to be search</param>
        /// <returns></returns>
        private string showItemDescription(string key)
        {
            var item = itens[key];
            return item.description;
        }

        /// <summary>
        /// Method to set as available, zombie head item
        /// </summary>
        public void GetZombieHead() { this.zombieHead.available = true; }

        /// <summary>
        /// Method to set as available, full flask item and  remove drinkingFlask
        /// </summary>
        public void FulfillFlask()
        {
            this.drinkingFlask.available = false;
            this.fullFlask.available = true;
        }

        /// <summary>
        /// Method to set as available, brass key item and remove zombie head.
        /// </summary>
        public void GetKey()
        {
            this.brassKey.available = true;
            this.zombieHead.available = false;
        }

        /// <summary>
        /// Method to set as available, raw gem stone item.
        /// </summary>
        public void GetGem() { this.rawGemStone.available = true; }

        /// <summary>
        /// Method to set as available, refined gem stone and magical shard. Removes raw gem stone item.
        /// </summary>
        public void RefineGem()
        {
            this.rawGemStone.available = false;
            this.refinedGemStone.available = true;
            this.magicalShard.available = true;
        }

        /// <summary>
        /// Method to set as available, magical sword item. Removes rust sword, magical shard and refined gem stone.
        /// </summary>
        public void ForgeMagicalSword()
        {
            this.magicSword.available = true;
            this.rustSword.available = false;
            this.magicalShard.available = false;
            this.refinedGemStone.available = false;
        }

        /// <summary>
        /// Method to check if the player has brass key in inventory.
        /// </summary>
        /// <returns>A bool confirming</returns>
        public bool HasKey() { return this.brassKey.available; }

        /// <summary>
        /// Method to check if the player has refined gem in inventory.
        /// </summary>
        /// <returns>A bool confirming</returns>
        public bool HasRefinedGem() { return this.refinedGemStone.available; }

        /// <summary>
        /// Method to check if the player has magical sword in inventory.
        /// </summary>
        /// <returns>A bool confirming</returns>
        public bool HasMagicalSword() { return this.magicSword.available; }
    }

    /// <summary>
    /// Class to handle with itens for inventory.
    /// </summary>
    class InventoryItem
    {
        public string name { get; }
        public string description { get; }
        public bool available { get; set; }
        public InventoryItem(string name, string description, bool available)
        {
            this.name = name;
            this.description = description;
            this.available = available;
        }
    }
}
