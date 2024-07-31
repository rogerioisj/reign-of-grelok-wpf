using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.state;

namespace reign_of_grelok_wpf.stages
{
    /// <summary>
    /// Town Stage
    /// </summary>
    class Town : IStage
    {
        public event EventHandler MenuUpdated;

        private Inventory inventoryInstance;
        private Management stateManagementInstance;
        private StageMenuItem lookAround;
        private StageMenuItem talkToBlackSmith;
        private StageMenuItem talkToPriest;
        private List<StageMenuItem> menuItemList;
        private Dictionary<string, MenuItem> menuToBeExported;

        /// <summary>
        /// Town Stage constructor
        /// </summary>
        /// <param name="inventory"> An inventory instance</param>
        /// <param name="management">A management state instance</param>
        /// <param name="handler">A event handler to load new menu options from ui</param>
        public Town(Inventory inventory, Management management, EventHandler handler)
        {
            this.inventoryInstance = inventory;
            this.stateManagementInstance = management;
            this.MenuUpdated = handler;

            lookAround = new StageMenuItem("Olhar ao redor", _ => ShowStageMessage(), EventType.Text, true);
            talkToBlackSmith = new StageMenuItem("Falar com ferreiro", _ => ShowBlacksmithMessage(), EventType.Text, false);
            talkToPriest = new StageMenuItem("Falar com padre", _ => ShowPriestMessage(), EventType.Text, false);

            menuToBeExported = new Dictionary<string, MenuItem>();
            menuItemList = new List<StageMenuItem>();
            menuItemList.Add(lookAround);
            menuItemList.Add(talkToBlackSmith);
            menuItemList.Add(talkToPriest);
        }

        /// <summary>
        /// A method to load stage infos.
        /// </summary>
        /// <param name="backAction">A method to be called when back button is pressed</param>
        /// <returns>StageInfo instance</returns>
        public StageInfo LoadStageInfo(LoadStageAction backAction)
        {
            var availableMenu = this.getMenuAvailable(backAction);
            var stage = new StageInfo("Cidade", availableMenu);

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

            menuToBeExported.Add("Ir para Norte", new MenuItem("Ir para Norte", _ => backAction(null), EventType.Load));
            menuToBeExported.Add("Inventário", new MenuItem("Inventário", _ => this.inventoryInstance.LoadStageInfo(_ => this.LoadStageInfo(backAction)), EventType.Load));

            return menuToBeExported;
        }

        /// <summary>
        /// A method to present the stage message and load the new menu options
        /// </summary>
        /// <returns>Stage description</returns>
        private string ShowStageMessage()
        {
            stateManagementInstance.SeeTown();
            talkToBlackSmith.isAvailable = true;
            talkToPriest.isAvailable = true;
            OnMenuUpdated();
            return "Você olha ao seu redor...\n\n\n" +

                "Você está na poeirenta praça do mercado de uma cidade tranquila. " +
                "Muitas das lojas e casas estão abandonadas, e os cidadãos que podem ser vistos falam em voz baixa, lançando olhares furtivos para o horizonte escuro no extremo norte. " +
                "O toque de uma bigorna quebra regularmente o silêncio, onde um ferreiro bigodudo se debruça sobre o seu trabalho numa tenda próxima.\r\n\r\n" +
                "O ferreiro está aqui, trabalhando.\r\n\r\n" +
                "Um padre está aqui, bebendo.";
        }

        /// <summary>
        /// A method to show some text when the player talk to blacksmith
        /// </summary>
        /// <returns>A text based in what already happens</returns>
        private string ShowBlacksmithMessage()
        {
            if (stateManagementInstance.AlreadyClearGem() && inventoryInstance.HasRefinedGem())
            {
                return ShowBlacksmithMessageWithGem();
                
            }

            return ShowStandardBlacksmithMessage();
        }

        /// <summary>
        /// Method to get default message when talk to blacksmith
        /// </summary>
        /// <returns>Blacksmith text</returns>
        private string ShowStandardBlacksmithMessage()
        {
            return "Você se aproxima do ferreiro...\n\n\n" +
            "Seus olhos lacrimejam por causa da fumaça e do calor bajulador dentro da tenda. " +
                "O homem enorme enxuga o suor da cabeça careca e levanta os olhos do trabalho.\r\n\r\n\"" +
                "Não falta trabalho a ser feito com Grelok assustando todo mundo. Deixe-me cumprir meus pedidos, estranho.\" " +
                "Com isso, o ferreiro dispensa você de sua tenda e molha uma lâmina quente em água, sibilando com vapor.";
        }

        /// <summary>
        /// Method to get message when talk to blacksmith after get gem from wizard
        /// </summary>
        /// <returns>Blacksmith text</returns>
        private string ShowBlacksmithMessageWithGem()
        {
            inventoryInstance.ForgeMagicalSword();
            return "Você se aproxima do ferreiro...\n\n\n" +
                "O ferreiro olha para você rispidamente e está prestes a dispensá-lo quando você tira a pedra preciosa polida de sua bolsa. " +
                "Ele deixa o martelo de lado e torce o bigode.\r\n\r\n\"" +
                "Uma pedra muito boa, claro.\" Ele diz, admirando a pedra lapidada: \"" +
                "Do que você estaria precisando, então?\"\r\n\r\n" +
                "Seguindo suas instruções cuidadosas, o ferreiro reforja sua espada enferrujada com o fragmento mágico no centro da lâmina.";
        }

        /// <summary>
        /// A method to show some text when the player talk to priest
        /// </summary>
        /// <returns>A text based in what already happens</returns>
        private string ShowPriestMessage()
        {
            if (inventoryInstance.HasKey() && stateManagementInstance.AlreadyKilledZombie())
            {
                return ShowZombieHeadWithItemPriestMessage();
            }

            if (stateManagementInstance.AlreadyKilledZombie())
            {
                return ShowZombieHeadWithoutItemPriestMessage();
            }

            return ShowStandardPriestMessage();
        }

        /// <summary>
        /// Method to get default message when talk to priest
        /// </summary>
        /// <returns>Priest text</returns>
        private string ShowStandardPriestMessage()
        {
            return "Você se aproxima do clérigo...\n\n\n" +
            "O padre percebe sua aproximação e levanta os olhos do seu gole.\r\n" +
                "“Grelok chegou e estamos abandonados!”, ele grita. " +
                "“Urp!”, ele continua.\r\n\r\n" +
                "Ao se recuperar do fedor do arroto sacerdotal, você é informado de que o padre fugiu de sua capela próxima. " +
                "Quando Grelok chegou à montanha, os mortos em seu cemitério começaram a se levantar e sua congregação se dispersou.\r\n\r\n" +
                "“Se você pudesse livrar o lugar dos zumbis”, ele lhe diz, “eu te darei a chave, e você pode ir ao boticário”";
        }

        /// <summary>
        /// Method to get message when talk to preist after burn zombie head
        /// </summary>
        /// <returns>Priest message</returns>
        private string ShowZombieHeadWithoutItemPriestMessage()
        {
            inventoryInstance.GetKey();
            return "Você se aproxima do clérigo...\n\n\n" +
            "O padre amaldiçoa bêbado os mortos-vivos que contaminaram sua igreja. " +
                "Você apresenta a ele a cabeça decapitada do zumbi que está em sua bolsa.\r\n\r\n" +
                "“Louvado seja você!”, ele soluça. " +
                "“Talvez a influência de Grelok não seja tão forte!”. " +
                "Com isso, ele vira sua garrafa de cabeça para baixo e a joga na lareira, onde ela explode em chamas roxas e queima quase instantaneamente.\r\n\r\n\"" +
                "Devo reunir os fiéis.\" " +
                "Ele pressiona uma chave de latão na palma da sua mão:" +
                " \"Por favor, sirva-se do pouco que puder ser útil em minha capela.\"";
        }

        /// <summary>
        /// Method to get message when talk to preist after get zombie head
        /// </summary>
        /// <returns>Priest message</returns>
        private string ShowZombieHeadWithItemPriestMessage()
        {
            return "Você se aproxima do clérigo...\n\n\n" +
            "O padre está bebendo água, debruçado sobre um grosso volume encadernado em couro preso ao pescoço por uma grossa tira de couro. " +
                "Ele percebe você apenas quando você chega muito perto.\r\n\r\n\"" +
                "Ah, bom amigo! Você já foi abrir a capela? " +
                "Meu corpo ainda dói de tanto beber, infelizmente, mas logo reunirei a congregação e voltarei sozinho.\"";
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
