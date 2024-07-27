using System;

namespace Reign_of_Grelok.state
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
        private List<string> itemDescriptions;

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
            itemDescriptions = new List<string>();
            itemDescriptions.Add("Sua arma. Enferrujada, mas confiável.");
            itemDescriptions.Add("Um frasco muito pequeno para transportar água.");
            itemDescriptions.Add("O cheiro pode torná-lo impopular...");
            itemDescriptions.Add("Uma pedra preciosa brilhante e facetada.");
            itemDescriptions.Add("O fragmento de gema pulsa com luz mágica...");
            itemDescriptions.Add("Uma arma encantada para derrotar Grelok!");
            itemDescriptions.Add("Chave dada a você pelo padre.");
            itemDescriptions.Add("Esta pedra preciosa pode ser valiosa...");
            itemDescriptions.Add("Seu Frasco está cheio de água benta.");
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

            this.LoadOptions(key, callback);
        }

        private void LoadOptions(char key, CallbackStageMenu callback)
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
        }

        private void checkIfItemISAvailable(bool item, CallbackStageMenu callback)
        {
            if (!item)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida!\n\n\n");
                this.Load(callback);
            }
        }

        private void showItemDescription(int index)
        {
            Console.Clear();
            Console.WriteLine(this.itemDescriptions[index]);
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
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
}
