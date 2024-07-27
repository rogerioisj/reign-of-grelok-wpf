using Reign_of_Grelok.state;

namespace Reign_of_Grelok.stages
{
    class Town
    {
        private Inventory inventoryInstance;
        private Management stateManagementInstance;

        public Town(Inventory inventory, Management management)
        {
            this.inventoryInstance = inventory;
            this.stateManagementInstance = management;
        }
        public void Load(CallbackStageMenu callback)
        {
            Console.WriteLine("REINO DE GRELOK (beta v.632)");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("\nCidade");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Pressione de acordo com o destino:");
            Console.WriteLine("1 - Olhar ao redor");
            if (stateManagementInstance.AlreadyCheckTown())
            {
                Console.WriteLine("2 - Falar com ferreiro");
                Console.WriteLine("3 - Falar com padre");
            }
            Console.WriteLine("4 - Ir para Norte");
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
                            this.stateManagementInstance.AlreadyCheckTown(),
                            callback
                            );
                    this.ShowBlacksmithMessage();
                    this.Load(callback);
                    break;
                case '3':
                    this.CheckIfOptionIsAvailable(
                            this.stateManagementInstance.AlreadyCheckTown(),
                            callback
                            );
                    this.ShowPriestMessage();
                    this.Load(callback);
                    break;
                case '4':
                    Console.Clear();
                    callback();
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
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está na poeirenta praça do mercado de uma cidade tranquila. " +
                "Muitas das lojas e casas estão abandonadas, e os cidadãos que podem ser vistos falam em voz baixa, lançando olhares furtivos para o horizonte escuro no extremo norte. " +
                "O toque de uma bigorna quebra regularmente o silêncio, onde um ferreiro bigodudo se debruça sobre o seu trabalho numa tenda próxima.\r\n\r\n" +
                "O ferreiro está aqui, trabalhando.\r\n\r\n" +
                "Um padre está aqui, bebendo."
             );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.SeeTown();
        }

        private void ShowBlacksmithMessage()
        {
            if (this.stateManagementInstance.AlreadyClearGem() && this.inventoryInstance.HasRefinedGem())
            {
                this.ShowBlacksmithMessageWithGem();
                return;
            }

            this.ShowStandardBlacksmithMessage();
        }

        private void ShowStandardBlacksmithMessage()
        {
            Console.Clear();
            Console.Write("Você se aproxima do ferreiro...\n\n\n");
            Console.WriteLine(
                "Seus olhos lacrimejam por causa da fumaça e do calor bajulador dentro da tenda. " +
                "O homem enorme enxuga o suor da cabeça careca e levanta os olhos do trabalho.\r\n\r\n\"" +
                "Não falta trabalho a ser feito com Grelok assustando todo mundo. Deixe-me cumprir meus pedidos, estranho.\" " +
                "Com isso, o ferreiro dispensa você de sua tenda e molha uma lâmina quente em água, sibilando com vapor."
             );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowBlacksmithMessageWithGem()
        {
            Console.Clear();
            Console.Write("Você se aproxima do ferreiro...\n\n\n");
            Console.WriteLine(
                "O ferreiro olha para você rispidamente e está prestes a dispensá-lo quando você tira a pedra preciosa polida de sua bolsa. " +
                "Ele deixa o martelo de lado e torce o bigode.\r\n\r\n\"" +
                "Uma pedra muito boa, claro.\" Ele diz, admirando a pedra lapidada: \"" +
                "Do que você estaria precisando, então?\"\r\n\r\n" +
                "Seguindo suas instruções cuidadosas, o ferreiro reforja sua espada enferrujada com o fragmento mágico no centro da lâmina."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.ForgeMagicalSword();
        }

        private void ShowPriestMessage()
        {
            if (this.inventoryInstance.HasKey() && this.stateManagementInstance.AlreadyKilledZombie())
            {
                this.ShowZombieHeadWithItemPriestMessage();
                return;
            }

            if (this.stateManagementInstance.AlreadyKilledZombie())
            {
                this.ShowZombieHeadWithoutItemPriestMessage();
                return;
            }
            this.ShowStandardPriestMessage();
        }

        private void ShowStandardPriestMessage()
        {
            Console.Clear();
            Console.Write("Você se aproxima do clérigo...\n\n\n");
            Console.WriteLine(
                "O padre percebe sua aproximação e levanta os olhos do seu gole.\r\n" +
                "“Grelok chegou e estamos abandonados!”, ele grita. " +
                "“Urp!”, ele continua.\r\n\r\n" +
                "Ao se recuperar do fedor do arroto sacerdotal, você é informado de que o padre fugiu de sua capela próxima. " +
                "Quando Grelok chegou à montanha, os mortos em seu cemitério começaram a se levantar e sua congregação se dispersou.\r\n\r\n" +
                "“Se você pudesse livrar o lugar dos zumbis”, ele lhe diz, “eu te darei a chave, e você pode ir ao boticário”"
             );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowZombieHeadWithoutItemPriestMessage()
        {
            Console.Clear();
            Console.Write("Você se aproxima do clérigo...\n\n\n");
            Console.WriteLine("O padre amaldiçoa bêbado os mortos-vivos que contaminaram sua igreja. " +
                "Você apresenta a ele a cabeça decapitada do zumbi que está em sua bolsa.\r\n\r\n" +
                "“Louvado seja você!”, ele soluça. " +
                "“Talvez a influência de Grelok não seja tão forte!”. " +
                "Com isso, ele vira sua garrafa de cabeça para baixo e a joga na lareira, onde ela explode em chamas roxas e queima quase instantaneamente.\r\n\r\n\"" +
                "Devo reunir os fiéis.\" " +
                "Ele pressiona uma chave de latão na palma da sua mão:" +
                " \"Por favor, sirva-se do pouco que puder ser útil em minha capela.\""
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.GetKey();
        }

        private void ShowZombieHeadWithItemPriestMessage()
        {
            Console.Clear();
            Console.Write("Você se aproxima do clérigo...\n\n\n");
            Console.WriteLine("O padre está bebendo água, debruçado sobre um grosso volume encadernado em couro preso ao pescoço por uma grossa tira de couro. " +
                "Ele percebe você apenas quando você chega muito perto.\r\n\r\n\"" +
                "Ah, bom amigo! Você já foi abrir a capela? " +
                "Meu corpo ainda dói de tanto beber, infelizmente, mas logo reunirei a congregação e voltarei sozinho.\"");
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
