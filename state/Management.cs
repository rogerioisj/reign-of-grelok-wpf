using System;

namespace reign_of_grelok_wpf.state
{
    /// <summary>
    /// Class to handle with state management
    /// </summary>
    class Management
    {
        private bool hasSawTown;
        private bool hasSawChapel;
        private bool hasSawSwamp;
        private bool hasSawMontainside;
        private bool hasTalkedToWizard;
        private bool hasKilledZombie;
        private bool hasUsedKey;
        private bool hasTakedZombieHead;
        private bool hasTakedGem;
        private bool hasClearGem;
        private bool hasFinishedGame;
        public Management() {
            hasSawTown = false;
            hasSawChapel = false;
            hasSawSwamp = false;
            hasSawMontainside = false;
            hasTalkedToWizard = false;
            hasKilledZombie = false;
            hasUsedKey = false;
            hasTakedGem = false;
            hasClearGem = false;
            hasFinishedGame = false;
        }

        /// <summary>
        /// Method to change state when the player clicks on "See around" at town stage.
        /// </summary>
        public void SeeTown() { hasSawTown = true; }

        /// <summary>
        /// Method to change state when the player clicks on "See around" at chapel stage.
        /// </summary>
        public void SeeChapel() { hasSawChapel = true; }

        /// <summary>
        /// Method to change state when the player clicks on "See around" at swamp stage.
        /// </summary>
        public void SeeSwamp() { hasSawSwamp = true; }

        /// <summary>
        /// Method to change state when the player clicks on "See around" at montain side stage.
        /// </summary>
        public void SeeMontainside() { hasSawMontainside = true; }

        /// <summary>
        /// Method to check player already killed the zombie at chapel.
        /// </summary>
        public void KillZombie() { hasKilledZombie = true; }

        /// <summary>
        /// Method to get the zombie head at chapel.
        /// </summary>
        public void GetZombieHead() { hasTakedZombieHead = true; }

        /// <summary>
        /// Method to use the key at chapel.
        /// </summary>
        public void UseKey() { hasUsedKey = true; }

        /// <summary>
        /// Method to get the gem at montainside.
        /// </summary>
        public void GetRawGem() { hasTakedGem = true; }

        /// <summary>
        /// Method to talk to the wizard at swamp.
        /// </summary>
        public void TalkToWizard() {  hasTalkedToWizard = true; }

        /// <summary>
        /// Method to store event to clear the raw gem.
        /// </summary>
        public void ClearGem() {  hasClearGem = true; }

        /// <summary>
        /// Method to store event when the game is finished.
        /// </summary>
        public void FinishGame() { hasFinishedGame = true; }

        /// <summary>
        /// Method to check if the player already kill the zombie.
        /// </summary>
        /// <returns>Returns a bool</returns>
        public bool AlreadyKilledZombie() { return hasKilledZombie; }

        /// <summary>
        /// Method to check if the player already used the key at chapel.
        /// </summary>
        /// <returns>Returns a bool</returns>
        public bool AlreadyUsedKey() { return hasUsedKey; }

        /// <summary>
        /// Method to check if the player already talked to the wizard.
        /// </summary>
        /// <returns>Returns a bool</returns>
        public bool AlreadyTalkedToWizard() { return hasTalkedToWizard; }

        /// <summary>
        /// Method to check if the player already take the gem at montain side.
        /// </summary>
        /// <returns>Returns a bool</returns>
        public bool AlreadyTakedGem() { return hasTakedGem; }

        /// <summary>
        /// Method to check if the player already get the zombie head at chapel.
        /// </summary>
        /// <returns>Returns a bool</returns>
        public bool AlreadyTakedZombieHead() { return hasTakedZombieHead; }

        /// <summary>
        /// Method to check if the player already clear the gem with the wizard.
        /// </summary>
        /// <returns>Returns a bool</returns>
        public bool AlreadyClearGem() { return hasClearGem; }

        /// <summary>
        /// Method to check if the player already finished the game.
        /// </summary>
        /// <returns>Returns a bool</returns>
        public bool AlreadyFinishedGame() { return hasFinishedGame; }
    }
}
