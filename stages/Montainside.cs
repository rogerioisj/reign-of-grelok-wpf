using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.state;

namespace reign_of_grelok_wpf.stages
{
    /// <summary>
    /// Montainside stage
    /// </summary>
    class Montainside : IStage
    {
        public event EventHandler MenuUpdated;

        private Inventory inventoryInstance;
        private Management stateManagementInstance;
        private StageMenuItem lookAround;
        private StageMenuItem spade;
        private StageMenuItem lookGem;
        private List<StageMenuItem> menuItemList;
        private Dictionary<string, MenuItem> menuToBeExported;

        /// <summary>
        /// Montainside Stage constructor
        /// </summary>
        /// <param name="inventory"> An inventory instance</param>
        /// <param name="management">A management state instance</param>
        /// <param name="handler">A event handler to load new menu options from ui</param>
        public Montainside(Inventory inventory, Management management, EventHandler handler)
        {
            inventoryInstance = inventory;
            stateManagementInstance = management;
            MenuUpdated = handler;

            lookAround = new StageMenuItem("Olhar ao redor", _ => ShowStageMessage(), EventType.Text, true);
            spade = new StageMenuItem("Atacar Grelok!", _ => ShowAttackGrelokMessage(), EventType.Text, true);
            lookGem = new StageMenuItem("Investigar objeto brilhante", _ => ShowGemMessage(), EventType.Text, true);

            menuToBeExported = new Dictionary<string, MenuItem>();
            menuItemList = new List<StageMenuItem>();
            menuItemList.Add(lookAround);
            menuItemList.Add(spade);
            menuItemList.Add(lookGem);
        }

        /// <summary>
        /// A method to load stage infos.
        /// </summary>
        /// <param name="backAction">A method to be called when back button is pressed</param>
        /// <returns>StageInfo instance</returns>
        public StageInfo LoadStageInfo(LoadStageAction backAction)
        {
            var availableMenu = getMenuAvailable(backAction);
            var stage = new StageInfo("Montanhas", availableMenu);

            return stage;
        }

        /// <summary>
        /// Method to get all menu options available.
        /// </summary>
        /// <param name="backAction">A method to be called when back button is pressed</param>
        /// <returns>A Dictionary<string, MenuItem> instance</returns>
        private Dictionary<string, MenuItem> getMenuAvailable(LoadStageAction backAction)
        {
            menuToBeExported.Clear();
            menuItemList.ForEach(delegate (StageMenuItem item)
            {
                if (item.isAvailable)
                {
                    menuToBeExported.Add(item.Title, item.GetMenuItem());
                }
            });

            menuToBeExported.Add("Ir para Sul", new MenuItem("Ir para Sul", _ => backAction(null), EventType.Load));
            menuToBeExported.Add("Inventário", new MenuItem("Inventário", _ => this.inventoryInstance.LoadStageInfo(_ => this.LoadStageInfo(backAction)), EventType.Load));

            return menuToBeExported;
        }

        /// <summary>
        /// A method to present the stage message and load the new menu options
        /// </summary>
        /// <returns>Stage description</returns>
        private string ShowStageMessage()
        {
            if (this.stateManagementInstance.AlreadyTakedGem())
            {
                return this.ShowWithoutGemStageMessage();
            }

            return this.ShowStandardStageMessage();
        }

        /// <summary>
        /// Method to get a message when Grelok is attacked
        /// </summary>
        /// <returns>Attack message</returns>
        private string ShowAttackGrelokMessage()
        {
            if (this.inventoryInstance.HasMagicalSword())
            {
                return this.ShowMagicalSwordAttackMessage();
            }

            return this.ShowStandardAttackMessage();
        }

        /// <summary>
        /// Get deafult message when Grelok is attacked
        /// </summary>
        /// <returns>Default attack message</returns>
        private string ShowStandardAttackMessage()
        {
            return "Suas armas insignificantes são inúteis em Grelok.";
        }

        /// <summary>
        /// Get message when the player attack Grelok with the Magical Sword
        /// </summary>
        /// <returns>Attack message</returns>
        private string ShowMagicalSwordAttackMessage()
        {
            stateManagementInstance.FinishGame();

            OnMenuUpdated();

            return
                "Quando você desembainha sua espada, Grelok abaixa sua grande cabeça com chifres e solta uma risada na sua cara. " +
                "Você cerra os dentes e desfere um golpe poderoso com as duas mãos, a lâmina mágica ressoando claramente, mesmo em meio ao tumulto de gargalhadas guturais.\r\n\r\n" +
                "Você balança a espada com tanta força que ela escapa de suas mãos e cai na boca aberta da monstruosidade, perdida de vista na escuridão árida da garganta de Grelok. " +
                "Você dá um passo para trás enquanto Grelok fecha a boca e fica de pé. Ele fica imóvel por um momento, depois começa a arranhar o pescoço. " +
                "Abafado, um toque pode ser ouvido como se estivesse a uma grande distância.\r\n\r\n" +
                "De repente, o peito de Grelok explode em uma fonte de sangue verde e viscoso. " +
                "O Toque pode ser ouvido claramente agora, e enquanto a força vital escorre ao redor da ponta saliente da espada mágica, as nuvens de tempestade que rodopiam no pico já estão se dissipando. " +
                "Grelok foi derrotado!\r\n\r\n " +
                "FIM\r\n " +
                "(Obrigado por jogar!)";
        }

        /// <summary>
        /// Method to get deafult stage message
        /// </summary>
        /// <returns>Stage description</returns>
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

        /// <summary>
        /// Method to get stage message without gem
        /// </summary>
        /// <returns>Stage description without gem</returns>
        private string ShowWithoutGemStageMessage()
        {
            return "Você olha ao seu redor...\n\n" +
            "Você está na face escarpada e castigada pelo vento de uma montanha. " +
                "Nuvens de tempestade serpenteiam acima do cume, atingindo você e a vegetação esparsa com chuvas torrenciais. " +
                "Muito abaixo, além do sopé, uma ampla planície se estende pelo horizonte sul.\r\n\r\n" +
                "Grelok está aqui, vomitando heresias.\r\n\r";
        }

        /// <summary>
        /// Method to get a message when payer get the gem
        /// </summary>
        /// <returns>Get gem action</returns>
        private string ShowGemMessage()
        {
            this.inventoryInstance.GetGem();
            this.stateManagementInstance.GetRawGem();
            lookGem.isAvailable = false;
            OnMenuUpdated();
            return "Você pega uma pedra preciosa bruta das rochas!";
        }

        /// <summary>
        /// Method to update menu options from ui
        /// </summary>
        protected virtual void OnMenuUpdated()
        {
            MenuUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
