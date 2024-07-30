using reign_of_grelok_wpf.infoModel;
using reign_of_grelok_wpf.state;

namespace reign_of_grelok_wpf.stages
{
    class Swamp : IStage
    {
        public event EventHandler MenuUpdated;

        private Inventory inventoryInstance;
        private Management stateManagementInstance;

        private StageMenuItem lookAround;
        private StageMenuItem talkToWizard;

        private List<StageMenuItem> menuItemList;
        private Dictionary<string, MenuItem> menuToBeExported;

        public Swamp(Inventory inventory, Management management, EventHandler handler)
        {
            MenuUpdated = handler;
            inventoryInstance = inventory;
            stateManagementInstance = management;

            lookAround = new StageMenuItem("Olhar ao redor", _ => ShowStageMessage(), EventType.Text, true);
            talkToWizard = new StageMenuItem("Falar com mago", _ => ShowWizardMessage(), EventType.Text, false);

            menuToBeExported = new Dictionary<string, MenuItem>();
            menuItemList = new List<StageMenuItem>();
            menuItemList.Add(lookAround);
            menuItemList.Add(talkToWizard);
        }

        public StageInfo LoadStageInfo(LoadStageAction backAction)
        {
            var availableMenu = getMenuAvailable(backAction);
            var stage = new StageInfo("Pantano", availableMenu);

            return stage;
        }

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

            menuToBeExported.Add("Ir para Leste", new MenuItem("Ir para Leste", _ => backAction(null), EventType.Load));
            menuToBeExported.Add("Inventário", new MenuItem("Inventário", _ => this.inventoryInstance.LoadStageInfo(_ => this.LoadStageInfo(backAction)), EventType.Load));

            return menuToBeExported;
        }

        private string ShowStageMessage()
        {
            return ShowStandardStageMessage();
        }

        private string ShowStandardStageMessage()
        {
            stateManagementInstance.SeeSwamp();
            talkToWizard.isAvailable = true;

            OnMenuUpdated();

            return "Você olha ao seu redor...\n\n" +
                "Você está em um caminho estreito de pedra em um pântano escuro. " +
                "Bolhas gordurosas flutuam até o topo das águas do pântano em ambos os lados e estouram preguiçosamente, salpicando suas pernas com lama e limo. " +
                "Uma pequena torre de pedra fica aqui. Nenhuma porta é visível e as pedras são lisas e polidas. " +
                "Uma varanda se projeta no meio da face da torre. " +
                "Os cheiros inebriantes do incenso misturam-se com o fedor nauseante do pântano. " +
                "O caminho de pedra desenrola-se para leste, em direção a uma ampla planície além dos pântanos.\r\n\r\n" +
                "Um mago está aqui, gesticulando loucamente em sua varanda.";
        }

        private string ShowWizardMessage()
        {
            if (stateManagementInstance.AlreadyTalkedToWizard() && stateManagementInstance.AlreadyClearGem())
            {
                return ShowWizardPostClearGemMessage();
            }

            if (stateManagementInstance.AlreadyTalkedToWizard() && stateManagementInstance.AlreadyTakedGem())
            {
                return ShowWizardWithGemMessage();
            }

            if (stateManagementInstance.AlreadyTalkedToWizard())
            {
                return ShowWizardPostActionMessage();
            }

            return ShowWizardStandardMessage();
        }

        private string ShowWizardStandardMessage()
        {
            stateManagementInstance.TalkToWizard();

            return "Você fala com o mago...\n\n\n" +
            "O mago acena freneticamente para você da varanda dele. “Você está aqui, você chegou!”, ele exclama. " +
                "Depois de um silêncio constrangedor, ele enfia o dedo excitado em uma bola de cristal, quase derrubando-a no pântano.\r\n\r\n\"" +
                "Eu vi, você vê. Você é quem derrotou Grelok. Hoo-hoo!\" O homenzinho pula no corrimão, girando uma pirueta. \"" +
                "Agora chegou a hora de fazer minha parte. Jogue a joia!\"\r\n\r\nA testa do mago se franze. \"" +
                "As coisas estão um pouco fora de ordem, não é? Volte quando tiver uma pedra preciosa poderosa. " +
                "Em breve - nunca consegui cumprir uma profecia antes!\"";
        }

        private string ShowWizardPostActionMessage()
        {
            return "Você fala com o mago...\n\n\n" +
            "O mago está enxotando você, com as mangas balançando.\r\n\r\n\"Vá! Encontre a pedra preciosa e volte, para que eu possa fazer minha parte!\"";
        }

        private string ShowWizardWithGemMessage()
        {
            inventoryInstance.RefineGem();
            stateManagementInstance.ClearGem();

            return "Você fala com o mago...\n\n\n" +
            "\"Hoo-hoo! O matador de Grelok se aproxima, com a pedra bruta na mão, assim como eu vi!\" " +
                "O chapéu pontudo do mago balança com entusiasmo quando ele aponta o dedo para você. " +
                "De repente, um arco de luz laranja claro se estende do dedo nodoso e tira a pedra preciosa de sua bolsa antes que você possa reagir. " +
                "A pedra preciosa para e paira no ar diante do nariz do mago.\r\n\r\n\"Essência seja verdadeira, poderes renovem-se, Fatty-Hoo-Do!\" " +
                "Com isso, ele bate na pedra flutuante, esmagando-a contra a pedra lisa da torre. Em uma explosão de luz, a pedra se divide em duas, e uma cai em cada palma estendida do pequeno bruxo saltitante.\r\n\r\n\"" +
                "Fragmento para a espada. Envolva-a em ferro e ela encontrará o coração negro de Grelok para você. Leve o joio também. " +
                "Você precisará do pagamento para um ferreiro forjar a arma.\" " +
                "Ele joga as pedras nas quais você salta para pegá-las com segurança.";
        }

        private string ShowWizardPostClearGemMessage()
        {
            return "Você fala com o mago...\n\n\n" +
                "\"Leve você a uma ferraria! Forje o fragmento com a espada e derrote Grelok!\"\r\n\r\nO mago joga algumas pedras para afastá-lo e se ocupa em conjurar nuvens coloridas de fumaça.";
        }

        protected virtual void OnMenuUpdated()
        {
            MenuUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
