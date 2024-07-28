﻿using reign_of_grelok_wpf.infoModel;
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
            menu.Add(new StageMenuItem("Usar espada mágica em Grelok", _ => this.ShowAttackGrelokMessage(), EventType.Text, false));
            menu.Add(new StageMenuItem("Usar espada em Grelok", _ => this.ShowAttackGrelokMessage(), EventType.Text, true));
            menu.Add(new StageMenuItem("Investigar objeto brilhante", _ => this.ShowGemMessage(), EventType.Text, true));
        }

        public StageInfo LoadStageInfo(LoadStageAction backAction)
        {
            var availableMenu = this.getMenuAvailable(backAction);
            var stage = new StageInfo("Planicies", availableMenu);

            return stage;
        }

        private Dictionary<string, MenuItem> getMenuAvailable(LoadStageAction backAction)
        {
            var menuParsed = new Dictionary<string, MenuItem>();
            menu.ForEach(delegate (StageMenuItem item)
            {
                if (item.isAvailable) menuParsed.Add(item.Title, item.GetMenuItem());
            });

            menuParsed.Add("Ir para Sul", new MenuItem("Ir para Sul", _ => backAction(null), EventType.Load));
            menuParsed.Add("Inventário", new MenuItem("Inventário", _ => this.inventoryInstance.LoadStageInfo(_ => this.LoadStageInfo(backAction)), EventType.Load));

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

        private string ShowAttackGrelokMessage()
        {
            if (this.inventoryInstance.HasMagicalSword())
            {
                return this.ShowMagicalSwordAttackMessage();
            }

            return this.ShowStandardAttackMessage();
        }

        private string ShowStandardAttackMessage()
        {
            return "Suas armas insignificantes são inúteis em Grelok.";
        }

        private string ShowMagicalSwordAttackMessage()
        {
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

        private string ShowGemMessage()
        {
            this.inventoryInstance.GetGem();
            this.stateManagementInstance.GetRawGem();
            return "Você pega uma pedra preciosa bruta das rochas!";
        }
    }
}
