using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.state;

namespace reign_of_grelok_wpf.stages
{
    class Montainside
    {
        private Inventory inventoryInstance;
        private Management stateManagementInstance;
        private List<StageMenuItem> menu;

        public Montainside(Inventory inventory, Management management)
        {
            this.inventoryInstance = inventory;
            this.stateManagementInstance = management;

            menu = new List<StageMenuItem>();
            menu.Add(new StageMenuItem("Olhar ao redor", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Usar espada mágica em Grelok", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Usar espada em Grelok", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Investigar objeto brilhante", _ => this.ShowStageMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Ir para Sul", _ => this.ShowStageMessage(), EventType.Text, true));
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
            menu.ForEach(delegate (StageMenuItem item)
            {
                if (item.isAvailable) menuParsed.Add(item.Title, item.GetMenuItem());
            });

            return menuParsed;
        }

        private string ShowStageMessage()
        {
            if (this.stateManagementInstance.AlreadyTakedGem())
            {
                return this.ShowWithoutGemStageMessage();
            }

            return this.ShowStandardStageMessage();
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

        private string ShowStandardStageMessage()
        {
            this.stateManagementInstance.SeeMontainside();

            return "Você olha ao seu redor...\n\n" +
                "Você está na face escarpada e castigada pelo vento de uma montanha. " +
                "Nuvens de tempestade serpenteiam acima do cume, atingindo você e a vegetação esparsa com chuvas torrenciais. " +
                "Muito abaixo, além do sopé, uma ampla planície se estende pelo horizonte sul.\r\n\r\n" +
                "Grelok está aqui, vomitando heresias.\r\n\r\n" +
                "Um brilho entre as rochas chama sua atenção.";
        }

        private string ShowWithoutGemStageMessage()
        {
            return "Você olha ao seu redor...\n\n" +
            "Você está na face escarpada e castigada pelo vento de uma montanha. " +
                "Nuvens de tempestade serpenteiam acima do cume, atingindo você e a vegetação esparsa com chuvas torrenciais. " +
                "Muito abaixo, além do sopé, uma ampla planície se estende pelo horizonte sul.\r\n\r\n" +
                "Grelok está aqui, vomitando heresias.\r\n\r";
        }

        private void CheckIfOptionIsAvailable(bool option, CallbackStageMenu callback)
        {
            if (!option)
            {
                Console.Clear();
                Console.WriteLine("Opção inválida!\n\n\n");
                //this.Load(callback);
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
