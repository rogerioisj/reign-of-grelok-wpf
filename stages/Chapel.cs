using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.state;

namespace reign_of_grelok_wpf.stages
{
    /// <summary>
    /// Chapel stage
    /// </summary>
    class Chapel : IStage
    {
        public event EventHandler MenuUpdated;

        private Inventory inventoryInstance;
        private Management stateManagementInstance;
        private StageMenuItem lookAround;
        private StageMenuItem attackZombie;
        private StageMenuItem checkGrave;
        private StageMenuItem checkChapel;
        private List<StageMenuItem> menuItemList;
        private Dictionary<string, MenuItem> menuToBeExported;

        /// <summary>
        /// Chapel Stage constructor
        /// </summary>
        /// <param name="inventory"> An inventory instance</param>
        /// <param name="management">A management state instance</param>
        /// <param name="handler">A event handler to load new menu options from ui</param>
        public Chapel(Inventory inventory, Management management, EventHandler handler) { 
            inventoryInstance = inventory;
            stateManagementInstance = management;
            MenuUpdated = handler;

            lookAround = new StageMenuItem("Olhar ao redor", _ => ShowStageMessage(), EventType.Text, true);
            checkGrave = new StageMenuItem("Examinar cova", _ => ShowGraveMessage(), EventType.Text, false);
            attackZombie = new StageMenuItem("Atacar zumbi", _ => AttackZombie(), EventType.Text, false);
            checkChapel = new StageMenuItem("Examinar Capela", _ => CheckChapel(), EventType.Text, stateManagementInstance.AlreadyUsedKey());

            menuToBeExported = new Dictionary<string, MenuItem>();
            menuItemList = new List<StageMenuItem>();
            menuItemList.Add(lookAround);
            menuItemList.Add(checkGrave);
            menuItemList.Add(attackZombie);
            menuItemList.Add(checkChapel);
        }

        /// <summary>
        /// A method to load stage infos.
        /// </summary>
        /// <param name="backAction">A method to be called when back button is pressed</param>
        /// <returns>StageInfo instance</returns>
        public StageInfo LoadStageInfo(LoadStageAction backAction)
        {
            var availableMenu = getMenuAvailable(backAction);
            var stage = new StageInfo("Capela", availableMenu);

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

            menuToBeExported.Add("Ir para Oeste", new MenuItem("Ir para Oeste", _ => backAction(null), EventType.Load));
            menuToBeExported.Add("Inventário", new MenuItem("Inventário", _ => this.inventoryInstance.LoadStageInfo(_ => this.LoadStageInfo(backAction)), EventType.Load));

            return menuToBeExported;
        }

        /// <summary>
        /// A method to present the stage message and load the new menu options
        /// </summary>
        /// <returns>Stage description</returns>
        private string ShowStageMessage()
        {
            if (stateManagementInstance.AlreadyKilledZombie() && stateManagementInstance.AlreadyUsedKey())
            {
                return ShowPostActionOpenDoorsStageMessage();
            }

            if (this.stateManagementInstance.AlreadyKilledZombie() && inventoryInstance.HasKey() && !stateManagementInstance.AlreadyUsedKey())
            {
                return ShowPostActionWithKeyStageMessage();
            }

            if (this.stateManagementInstance.AlreadyKilledZombie())
            {
                return ShowPostActionStageMessage();
            }

            return ShowStandardStageMessage();
        }

        /// <summary>
        /// Method to get default stage message
        /// </summary>
        /// <returns>Stage description</returns>
        private string ShowStandardStageMessage()
        {
            stateManagementInstance.SeeChapel();
            checkGrave.isAvailable = true;
            attackZombie.isAvailable = true;

            OnMenuUpdated();

            return "Você olha ao seu redor...\n\n" +
            "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "Um zumbi cambaleia sem rumo por perto.\r\n\r\n" +
                "Há uma cova aberta nas proximidades.";
        }

        /// <summary>
        /// Method to get stage message after kill zombie
        /// </summary>
        /// <returns>Stage description</returns>
        private string ShowPostActionStageMessage()
        {
            stateManagementInstance.SeeChapel();
            return "Você olha ao seu redor...\n\n" +
            "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "Há uma cova aberta nas proximidades.";
        }

        /// <summary>
        /// Method to get stage message after get the key from priest
        /// </summary>
        /// <returns>Stage description</returns>
        private string ShowPostActionWithKeyStageMessage()
        {
            stateManagementInstance.SeeChapel();
            stateManagementInstance.UseKey();

            checkChapel.isAvailable = true;

            OnMenuUpdated();

            return "Você olha ao seu redor...\n\n" +
            "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "Você usa a chave dada pelo padre, e agora, as portas da capela estão abertas.\r\n\r\n" +
                "Há uma cova aberta nas proximidades.";
        }

        /// <summary>
        /// Method to get stage message after open chapel doors
        /// </summary>
        /// <returns>Stage description</returns>
        private string ShowPostActionOpenDoorsStageMessage()
        {
            return "Você olha ao seu redor...\n\n" +
            "Você está no final de um caminho de terra, de frente para uma pequena capela. " +
                "As paredes de estuque estão desbotadas, faltam muitas telhas. " +
                "As grandes portas de carvalho estão trancadas. " +
                "A congregação não está em lugar nenhum. " +
                "Um pequeno cemitério de lápides tortas fica à sombra do campanário rachado. " +
                "O caminho de terra serpenteia para oeste através de uma planície grande e indefinida.\r\n\r\n" +
                "As portas da capela estão abertas.\r\n\r\n" +
                "Há uma cova aberta nas proximidades.";
        }

        /// <summary>
        /// Method to select wich message will be presented
        /// </summary>
        /// <returns>Grave message</returns>
        private string ShowGraveMessage()
        {
            if (stateManagementInstance.AlreadyKilledZombie() && stateManagementInstance.AlreadyTakedZombieHead())
            {
                return ShowPostActionGraveWithoutItemMessage();
            }

            if (stateManagementInstance.AlreadyKilledZombie() && !stateManagementInstance.AlreadyTakedZombieHead())
            {
                return ShowPostActionGraveMessage();
            }

            return ShowStandardGraveMessage();
        }

        /// <summary>
        /// Grave message before kill the zombie
        /// </summary>
        /// <returns>Grave message</returns>
        private string ShowStandardGraveMessage()
        {
            return "Você olha ao seu redor...\n\n" +
            "Há uma cova profunda e vazia no cemitério. " +
            "Vários ratos inchados flutuando em trinta centímetros de água suja no fundo. Não caia!";
        }

        /// <summary>
        /// Grave message after kill the zombie
        /// </summary>
        /// <returns>Grave message</returns>
        private string ShowPostActionGraveMessage()
        {
            inventoryInstance.GetZombieHead();
            stateManagementInstance.GetZombieHead();
            return "Você espia dentro da cova aberta...\n\n" +
            "Há uma cova profunda e vazia no cemitério. " +
                "Vários ratos inchados e um cadáver de zumbi flutuam em trinta centímetros de água suja no fundo. Não caia!\r\n\r\n" +
                "Uma grotesca cabeça de zumbi está presa em uma raiz perto do topo da cova. " +
                "Você embala o troféu horrível como prova de sua ação.";
        }

        /// <summary>
        /// Grave message after kill the zombie and remove his head
        /// </summary>
        /// <returns>Grave message</returns>
        private string ShowPostActionGraveWithoutItemMessage()
        {
            return "Você espia dentro da cova aberta...\n\n" +
            "Há uma cova profunda e vazia no cemitério. " +
                "Vários ratos inchados e um cadáver de zumbi flutuam em trinta centímetros de água suja no fundo. Não caia!";
        }

        /// <summary>
        /// Method to kill zombie
        /// </summary>
        /// <returns>Zombie attack message</returns>
        private string AttackZombie()
        {
            stateManagementInstance.KillZombie();
            attackZombie.isAvailable = false;

            OnMenuUpdated();

            return "Seu golpe joga o zumbi em uma cova.";
        }

        /// <summary>
        /// Method to get a message when the Chapel is visited
        /// </summary>
        /// <returns>chapel message</returns>
        private string CheckChapel()
        {
            inventoryInstance.FulfillFlask();
            return "Você entra na capela vazia...\n\n" +
            "Partículas de poeira pendem preguiçosamente nos raios de luz colorida que se estendem pelas janelas pontiagudas pela capela. " +
                "Os bancos, o púlpito e tudo o mais estão cobertos por uma névoa fina. " +
                "Perto da entrada existe uma cisterna de pedra muito profunda. " +
                "Está cheio até a borda com água benta.";
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
