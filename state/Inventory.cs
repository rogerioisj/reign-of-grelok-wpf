using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.stages;

namespace reign_of_grelok_wpf.state
{
    delegate void CallbackStageMenu(char? key);

    class Inventory
    {
        private bool rustSword;
        private bool drinkingFlask;
        private bool zombieHead;
        private bool refinedGemStone;
        private bool magicalShard;
        private bool magicSword;
        private bool brassKey;
        private bool rawGemStone;
        private bool fullFlask;
        private Dictionary<string, InventoryItem> itens;
        private List<StageMenuItem> menu;

        public Inventory()
        {
            rustSword = true;
            drinkingFlask = true;
            zombieHead = false;
            refinedGemStone = false;
            magicalShard = false;
            magicSword = false;
            brassKey = false;
            rawGemStone = false;
            fullFlask = false;

            this.menu = new List<StageMenuItem>();
            menu.Add(new StageMenuItem("Espada enferrujada", _ => this.showItemDescription("Espada enferrujada"), EventType.Text, true));
            menu.Add(new StageMenuItem("Frasco de bebida", _ => this.showItemDescription("Frasco de bebida"), EventType.Text, true));

            itens = new Dictionary<string, InventoryItem>();
            itens.Add("Espada enferrujada", new InventoryItem("Espada enferrujada", "Sua arma. Enferrujada, mas confiável.", true));
            itens.Add("Frasco de bebida", new InventoryItem("Frasco de bebida", "Um frasco muito pequeno para transportar água.", true));
            itens.Add("Cabeça de zumbi", new InventoryItem("Cabeça de zumbi", "O cheiro pode torná-lo impopular...", false));
            itens.Add("Pedra preciosa refinada", new InventoryItem("Pedra preciosa refinada", "Uma pedra preciosa brilhante e facetada.", false));
            itens.Add("Fragmento mágico", new InventoryItem("Fragmento mágico", "O fragmento de gema pulsa com luz mágica...", false));
            itens.Add("Espada mágica", new InventoryItem("Espada mágica", "Uma arma encantada para derrotar Grelok!", false));
            itens.Add("Chave de latão", new InventoryItem("Chave de latão", "Chave dada a você pelo padre.", false));
            itens.Add("Pedra preciosa bruta", new InventoryItem("Pedra preciosa bruta", "Esta pedra preciosa pode ser valiosa...", false));
            itens.Add("Frasco de bebida cheio", new InventoryItem("Frasco de bebida cheio", "Seu Frasco está cheio de água benta.", false));
        }

        public StageInfo LoadStageInfo()
        {
            var availableMenu = this.getMenuAvailable();
            var stage = new StageInfo("Inventário", availableMenu);

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

        private string showItemDescription(string key)
        {
            var item = itens[key];
            return item.description;
        }

        private void checkIfItemISAvailable(bool item, CallbackStageMenu callback)
        {
            if (!item)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida!\n\n\n");
                //this.Load(callback);
            }
        }

        public void GetZombieHead() { this.zombieHead = true; }

        public void FulfillFlask()
        {
            this.drinkingFlask = false;
            this.fullFlask = true;
        }

        public void GetKey()
        {
            this.brassKey = true;
            this.zombieHead = false;
        }

        public void GetGem() { this.rawGemStone = true; }

        public void RefineGem()
        {
            this.rawGemStone = false;
            this.refinedGemStone = true;
            this.magicalShard = true;
        }

        public void ForgeMagicalSword()
        {
            this.magicSword = true;
            this.rustSword = false;
            this.magicalShard = false;
            this.refinedGemStone = false;
        }

        public bool HasZombieHead() { return this.zombieHead; }

        public bool HasKey() { return this.brassKey; }

        public bool HasRefinedGem() { return this.refinedGemStone; }

        public bool HasMagicalSword() { return this.magicSword; }
    }

    class InventoryItem
    {
        public string name { get; }
        public string description { get; }
        public bool available { get; }
        public InventoryItem(string name, string description, bool available)
        {
            this.name = name;
            this.description = description;
            this.available = available;
        }
    }
}
