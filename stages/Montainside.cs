using Reign_of_Grelok.state;

namespace Reign_of_Grelok.stages
{
    class Montainside
    {
        private Inventory inventoryInstance;
        private Management stateManagementInstance;

        public Montainside(Inventory inventory, Management management)
        {
            this.inventoryInstance = inventory;
            this.stateManagementInstance = management;
        }

        public void Load(CallbackStageMenu callback)
        {
            Console.WriteLine("REINO DE GRELOK (beta v.632)");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("\nMontanhas");
            Console.WriteLine("\nGrelok está aqui, vomitando heresias.");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Pressione de acordo com o destino:");
            Console.WriteLine("1 - Olhar ao redor");
            if (this.stateManagementInstance.AlreadyCheckMontainside())
            {
                if (this.inventoryInstance.HasMagicalSword()) Console.WriteLine("2 - Usar espada mágica em Grelok");
                else Console.WriteLine("2 - Usar espada em Grelok");
                if (!this.stateManagementInstance.AlreadyTakedGem()) Console.WriteLine("3 - Investigar objeto brilhante");
            }
            Console.WriteLine("4 - Ir para Sul");
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
                        this.stateManagementInstance.AlreadyCheckMontainside(),
                        callback
                        );
                    this.ShowAttackGrelokMessage();
                    this.Load(callback);
                    break;
                case '3':
                    this.CheckIfOptionIsAvailable(
                        this.stateManagementInstance.AlreadyCheckMontainside() && !this.stateManagementInstance.AlreadyTakedGem(),
                        callback
                        );
                    this.ShowGemMessage();
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

        private void ShowStageMessage()
        {
            if (this.stateManagementInstance.AlreadyTakedGem())
            {
                this.ShowWithoutGemStageMessage();
                return;
            }

            this.ShowStandardStageMessage();
        }

        private void ShowAttackGrelokMessage()
        {
            if (this.inventoryInstance.HasMagicalSword())
            {
                this.ShowMagicalSwordAttackMessage();
                return;
            }

            this.ShowStandardAttackMessage();
        }

        private void ShowStandardAttackMessage()
        {
            Console.Clear();
            Console.WriteLine("Suas armas insignificantes são inúteis em Grelok.");
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowMagicalSwordAttackMessage()
        {
            Console.Clear();
            Console.WriteLine(
                "Quando você desembainha sua espada, Grelok abaixa sua grande cabeça com chifres e solta uma risada na sua cara. " +
                "Você cerra os dentes e desfere um golpe poderoso com as duas mãos, a lâmina mágica ressoando claramente, mesmo em meio ao tumulto de gargalhadas guturais.\r\n\r\n" +
                "Você balança a espada com tanta força que ela escapa de suas mãos e cai na boca aberta da monstruosidade, perdida de vista na escuridão árida da garganta de Grelok. " +
                "Você dá um passo para trás enquanto Grelok fecha a boca e fica de pé. Ele fica imóvel por um momento, depois começa a arranhar o pescoço. " +
                "Abafado, um toque pode ser ouvido como se estivesse a uma grande distância.\r\n\r\n" +
                "De repente, o peito de Grelok explode em uma fonte de sangue verde e viscoso. " +
                "O Toque pode ser ouvido claramente agora, e enquanto a força vital escorre ao redor da ponta saliente da espada mágica, as nuvens de tempestade que rodopiam no pico já estão se dissipando. " +
                "Grelok foi derrotado!\r\n\r\n " +
                "FIM\r\n " +
                "(Obrigado por jogar!)"
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private void ShowStandardStageMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está na face escarpada e castigada pelo vento de uma montanha. " +
                "Nuvens de tempestade serpenteiam acima do cume, atingindo você e a vegetação esparsa com chuvas torrenciais. " +
                "Muito abaixo, além do sopé, uma ampla planície se estende pelo horizonte sul.\r\n\r\n" +
                "Grelok está aqui, vomitando heresias.\r\n\r\n" +
                "Um brilho entre as rochas chama sua atenção."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.SeeMontainside();
        }

        private void ShowWithoutGemStageMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está na face escarpada e castigada pelo vento de uma montanha. " +
                "Nuvens de tempestade serpenteiam acima do cume, atingindo você e a vegetação esparsa com chuvas torrenciais. " +
                "Muito abaixo, além do sopé, uma ampla planície se estende pelo horizonte sul.\r\n\r\n" +
                "Grelok está aqui, vomitando heresias.\r\n\r"
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
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

        private void ShowGemMessage()
        {
            Console.Clear();
            Console.WriteLine("Você pega uma pedra preciosa bruta das rochas!");
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.GetGem();
            this.stateManagementInstance.GetRawGem();
        }
    }
}
