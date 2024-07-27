using Reign_of_Grelok.state;

namespace Reign_of_Grelok.stages
{
    class Swamp
    {
        private Inventory inventoryInstance;
        private Management stateManagementInstance;

        public Swamp(Inventory inventory, Management management)
        {
            this.inventoryInstance = inventory;
            this.stateManagementInstance = management;
        }

        public void Load(CallbackStageMenu callback)
        {
            Console.WriteLine("REINO DE GRELOK (beta v.632)");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("\nPantano");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Pressione de acordo com o destino:");
            Console.WriteLine("1 - Olhar ao redor");
            if (stateManagementInstance.AlreadyCheckSwamp())
            {
                Console.WriteLine("2 - Falar com mago");
            }
            Console.WriteLine("4 - Ir para Leste");
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
                        this.stateManagementInstance.AlreadyCheckSwamp(),
                        callback
                        );
                    this.ShowWizardMessage();
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
            this.ShowStandardStageMessage();
        }

        private void ShowStandardStageMessage()
        {
            Console.Clear();
            Console.WriteLine("Você olha ao seu redor...\n\n");
            Console.WriteLine(
                "Você está em um caminho estreito de pedra em um pântano escuro. " +
                "Bolhas gordurosas flutuam até o topo das águas do pântano em ambos os lados e estouram preguiçosamente, salpicando suas pernas com lama e limo. " +
                "Uma pequena torre de pedra fica aqui. Nenhuma porta é visível e as pedras são lisas e polidas. " +
                "Uma varanda se projeta no meio da face da torre. " +
                "Os cheiros inebriantes do incenso misturam-se com o fedor nauseante do pântano. " +
                "O caminho de pedra desenrola-se para leste, em direção a uma ampla planície além dos pântanos.\r\n\r\n" +
                "Um mago está aqui, gesticulando loucamente em sua varanda."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.SeeSwamp();
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

        private void ShowWizardMessage()
        {
            if (this.stateManagementInstance.AlreadyTalkedToWizard() && this.stateManagementInstance.AlreadyClearGem())
            {
                this.ShowWizardPostClearGemMessage();
                return;
            }

            if (this.stateManagementInstance.AlreadyTalkedToWizard() && this.stateManagementInstance.AlreadyTakedGem())
            {
                this.ShowWizardWithGemMessage();
                return;
            }

            if (this.stateManagementInstance.AlreadyTalkedToWizard())
            {
                this.ShowWizardPostActionMessage();
                return;
            }

            this.ShowWizardStandardMessage();
        }

        private void ShowWizardStandardMessage()
        {
            Console.Clear();
            Console.Write("Você fala com o mago...\n\n\n");
            Console.WriteLine(
                "O mago acena freneticamente para você da varanda dele. “Você está aqui, você chegou!”, ele exclama. " +
                "Depois de um silêncio constrangedor, ele enfia o dedo excitado em uma bola de cristal, quase derrubando-a no pântano.\r\n\r\n\"" +
                "Eu vi, você vê. Você é quem derrotou Grelok. Hoo-hoo!\" O homenzinho pula no corrimão, girando uma pirueta. \"" +
                "Agora chegou a hora de fazer minha parte. Jogue a joia!\"\r\n\r\nA testa do mago se franze. \"" +
                "As coisas estão um pouco fora de ordem, não é? Volte quando tiver uma pedra preciosa poderosa. " +
                "Em breve - nunca consegui cumprir uma profecia antes!\""
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.stateManagementInstance.TalkToWizard();
        }

        private void ShowWizardPostActionMessage()
        {
            Console.Clear();
            Console.Write("Você fala com o mago...\n\n\n");
            Console.WriteLine("O mago está enxotando você, com as mangas balançando.\r\n\r\n\"Vá! Encontre a pedra preciosa e volte, para que eu possa fazer minha parte!\"");
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowWizardWithGemMessage()
        {
            Console.Clear();
            Console.Write("Você fala com o mago...\n\n\n");
            Console.WriteLine(
                "\"Hoo-hoo! O matador de Grelok se aproxima, com a pedra bruta na mão, assim como eu vi!\" " +
                "O chapéu pontudo do mago balança com entusiasmo quando ele aponta o dedo para você. " +
                "De repente, um arco de luz laranja claro se estende do dedo nodoso e tira a pedra preciosa de sua bolsa antes que você possa reagir. " +
                "A pedra preciosa para e paira no ar diante do nariz do mago.\r\n\r\n\"Essência seja verdadeira, poderes renovem-se, Fatty-Hoo-Do!\" " +
                "Com isso, ele bate na pedra flutuante, esmagando-a contra a pedra lisa da torre. Em uma explosão de luz, a pedra se divide em duas, e uma cai em cada palma estendida do pequeno bruxo saltitante.\r\n\r\n\"" +
                "Fragmento para a espada. Envolva-a em ferro e ela encontrará o coração negro de Grelok para você. Leve o joio também. " +
                "Você precisará do pagamento para um ferreiro forjar a arma.\" " +
                "Ele joga as pedras nas quais você salta para pegá-las com segurança."
            );
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.RefineGem();
            this.stateManagementInstance.ClearGem();
        }

        private void ShowWizardPostClearGemMessage()
        {
            Console.Clear();
            Console.Write("Você fala com o mago...\n\n\n");
            Console.WriteLine("\"Leve você a uma ferraria! Forje o fragmento com a espada e derrote Grelok!\"\r\n\r\nO mago joga algumas pedras para afastá-lo e se ocupa em conjurar nuvens coloridas de fumaça.");
            Console.WriteLine();
            Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            this.inventoryInstance.RefineGem();
            this.stateManagementInstance.ClearGem();
        }
    }
}
