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
            menu.Add(new StageMenuItem("Frasco de bebida", _ => this.showItemDescription("Espada enferrujada"), EventType.Text, true));

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

        public void Load(CallbackStageMenu callback)
        {
            Console.WriteLine("REINO DE GRELOK (beta v.632)");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("\nInventário");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Pressione de acordo com o item:");
            if (rustSword) Console.WriteLine("1 - Espada enferrujada");
            if (drinkingFlask) Console.WriteLine("2 - Frasco de bebida");
            if (zombieHead) Console.WriteLine("3 - Cabeça de zumbi");
            if (refinedGemStone) Console.WriteLine("4 - Pedra preciosa refinada");
            if (magicalShard) Console.WriteLine("5 - Fragmento mágico");
            if (magicSword) Console.WriteLine("6 - Espada mágica");
            if (brassKey) Console.WriteLine("7 - Chave de latão");
            if (rawGemStone) Console.WriteLine("8 - Pedra preciosa bruta");
            if (fullFlask) Console.WriteLine("9 - Frasco de bebida cheio.");
            Console.WriteLine("B - Voltar");
            Console.WriteLine("Q - Sair");
            var keyInfo = Console.ReadKey();
            var key = keyInfo.KeyChar;

            //this.LoadOptions(key, callback);
        }

        /*private void LoadOptions(char key, CallbackStageMenu callback)
        {
            switch (key)
            {
                case '1':
                    this.checkIfItemISAvailable(rustSword, callback);
                    this.showItemDescription(0);
                    this.Load(callback);
                    break;
                case '2':
                    this.checkIfItemISAvailable(drinkingFlask, callback);
                    this.showItemDescription(1);
                    this.Load(callback);
                    break;
                case '3':
                    this.checkIfItemISAvailable(zombieHead, callback);
                    this.showItemDescription(2);
                    this.Load(callback);
                    break;
                case '4':
                    this.checkIfItemISAvailable(refinedGemStone, callback);
                    this.showItemDescription(3);
                    this.Load(callback);
                    break;
                case '5':
                    this.checkIfItemISAvailable(magicalShard, callback);
                    this.showItemDescription(4);
                    this.Load(callback);
                    break;
                case '6':
                    this.checkIfItemISAvailable(magicSword, callback);
                    this.showItemDescription(5);
                    this.Load(callback);
                    break;
                case '7':
                    this.checkIfItemISAvailable(brassKey, callback);
                    this.showItemDescription(6);
                    this.Load(callback);
                    break;
                case '8':
                    this.checkIfItemISAvailable(rawGemStone, callback);
                    this.showItemDescription(7);
                    this.Load(callback);
                    break;
                case '9':
                    this.checkIfItemISAvailable(fullFlask, callback);
                    this.showItemDescription(8);
                    this.Load(callback);
                    break;
                case 'b':
                case 'B':
                    Console.Clear();
                    callback(null);
                    break;
                case 'q':
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida!\n\n\n");
                    this.Load(callback);
                    break;
            }
        }*/

        private void checkIfItemISAvailable(bool item, CallbackStageMenu callback)
        {
            if (!item)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida!\n\n\n");
                this.Load(callback);
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
