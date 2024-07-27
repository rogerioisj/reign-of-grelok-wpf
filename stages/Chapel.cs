using Reign_of_Grelok.state;

namespace Reign_of_Grelok.stages
{
    class Chapel
    {
        private Inventory inventoryInstance;
        private Management stateManagementInstance;

        public Chapel(Inventory inventory, Management management) { 
            this.inventoryInstance = inventory;
            this.stateManagementInstance = management;
        }

        public void Load(CallbackStageMenu callback)
        {
            Console.WriteLine("REINO DE GRELOK (beta v.632)");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("\nCapela");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Pressione de acordo com o destino:");
            Console.WriteLine("1 - Olhar ao redor");

            if (stateManagementInstance.AlreadyCheckChapel())
            {
                if ( !this.stateManagementInstance.AlreadyKilledZombie() ) Console.WriteLine("2 - Atacar zumbi com a espada");
                Console.WriteLine("3 - Examinar cova");
            }

            Console.WriteLine("4 - Ir para Oeste");

            if (this.stateManagementInstance.AlreadyUsedKey()) Console.WriteLine("5 - Examinar Capela");

            Console.WriteLine("I - Iventário");
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
                    this.ShowStageMessage();
                    this.Load(callback);
                    break;
                case '2':
                    this.CheckIfOptionIsAvailable(
                            this.stateManagementInstance.AlreadyCheckChapel() && !this.stateManagementInstance.AlreadyKilledZombie(),
                            callback
                            );
                    this.AttackZombie();
                    this.Load(callback);
                    break;
                case '3':
                    this.CheckIfOptionIsAvailable(
                            this.stateManagementInstance.AlreadyCheckChapel(),
                            callback
                            );
                    this.ShowGraveMessage();
                    this.Load(callback);
                    break;
                case '4':
                    Console.Clear();
                    callback();
                    break;
                case '5':
                    this.CheckIfOptionIsAvailable(
                            this.stateManagementInstance.AlreadyUsedKey(),
                            callback
                            );
                    this.CheckChapel();
                    this.Load(callback);
                    break;
                case 'q':
                case 'Q':
                    Environment.Exit(0);
                    break;
                case 'i':
                case 'I':
                    Console.Clear();
                    this.inventoryInstance.Load(_ => this.Load(callback));
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida!\n\n\n");
                    this.Load(callback);
                    break;
            }
        }

        private void CheckIfOptionIsAvailable(bool option, CallbackStageMenu callback)
        {
            if (!option)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida!\n\n\n");
                this.Load(callback);
            }
        }

        private void ShowStageMessage()
        {
            if (this.stateManagementInstance.AlreadyKilledZombie() && this.stateManagementInstance.AlreadyUsedKey())
            {
                this.ShowPostActionOpenDoorsStageMessage();
                return;
            }

            if (this.stateManagementInstance.AlreadyKilledZombie() && this.inventoryInstance.HasKey() && !this.stateManagementInstance.AlreadyUsedKey())
            {
                this.ShowPostActionWithKeyStageMessage();
                return;
            }

            if (this.stateManagementInstance.AlreadyKilledZombie())
            {
                this.ShowPostActionStageMessage();
                return;
            }

            this.ShowStandardStageMessage();
        }

        private void ShowStandardStageMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "Um zumbi cambaleia sem rumo por perto.\r\n\r\n" +
                "Há uma cova aberta nas proximidades."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.SeeChapel();
        }

        private void ShowPostActionStageMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "Há uma cova aberta nas proximidades."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.SeeChapel();
        }

        private void ShowPostActionWithKeyStageMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "Você usa a chave dada pelo padre, e agora, as portas da capela estão abertas.\r\n\r\n" +
                "Há uma cova aberta nas proximidades."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.SeeChapel();
            this.stateManagementInstance.UseKey();
        }

        private void ShowPostActionOpenDoorsStageMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "As portas da capela estão abertas.\r\n\r\n" +
                "Há uma cova aberta nas proximidades."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowGraveMessage()
        {
            if (this.stateManagementInstance.AlreadyKilledZombie() && this.inventoryInstance.HasZombieHead())
            {
                this.ShowPostActionGraveWithoutItemMessage();
                return;
            }

            if (this.stateManagementInstance.AlreadyKilledZombie())
            {
                this.ShowPostActionGraveMessage();
                return;
            }
            this.ShowStandardGraveMessage();
        }

        private void ShowStandardGraveMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine();
            Console.WriteLine("Há uma cova profunda e vazia no cemitério. Vários ratos inchados flutuando em trinta centímetros de água suja no fundo. Não caia!");
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowPostActionGraveMessage()
        {
            Console.Clear();
            Console.WriteLine("Você espia dentro da cova aberta...\n\n");
            Console.WriteLine();
            Console.WriteLine("Há uma cova profunda e vazia no cemitério. " +
                "Vários ratos inchados e um cadáver de zumbi flutuam em trinta centímetros de água suja no fundo. Não caia!\r\n\r\n" +
                "Uma grotesca cabeça de zumbi está presa em uma raiz perto do topo da cova. " +
                "Você embala o troféu horrível como prova de sua ação."
            );
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.GetZombieHead();
        }

        private void ShowPostActionGraveWithoutItemMessage()
        {
            Console.Clear();
            Console.WriteLine("Você espia dentro da cova aberta...\n\n");
            Console.WriteLine();
            Console.WriteLine(
                "Há uma cova profunda e vazia no cemitério. " +
                "Vários ratos inchados e um cadáver de zumbi flutuam em trinta centímetros de água suja no fundo. Não caia!"
            );
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.GetZombieHead();
        }

        private void AttackZombie()
        {
            Console.Clear();
            Console.WriteLine("Seu golpe joga o zumbi em uma cova.");
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.KillZombie();
        }

        private void CheckChapel()
        {
            Console.Clear();
            Console.WriteLine("Você entra na capela vazia...\n\n");
            Console.WriteLine();
            Console.WriteLine(
                "Partículas de poeira pendem preguiçosamente nos raios de luz colorida que se estendem pelas janelas pontiagudas pela capela. " +
                "Os bancos, o púlpito e tudo o mais estão cobertos por uma névoa fina. " +
                "Perto da entrada existe uma cisterna de pedra muito profunda. " +
                "Está cheio até a borda com água benta."
            );
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.FulfillFlask();
        }
    }
}
