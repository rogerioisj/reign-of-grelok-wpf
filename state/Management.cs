using System;

namespace reign_of_grelok_wpf.state
{
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

        public void SeeTown() { hasSawTown = true; }

        public void SeeChapel() { hasSawChapel = true; }

        public void SeeSwamp() { hasSawSwamp = true; }

        public void SeeMontainside() { hasSawMontainside = true; }

        public void KillZombie() { hasKilledZombie = true; }

        public void GetZombieHead() { hasTakedZombieHead = true; }

        public void UseKey() { hasUsedKey = true; }

        public void GetRawGem() { hasTakedGem = true; }

        public void TalkToWizard() {  hasTalkedToWizard = true; }

        public void ClearGem() {  hasClearGem = true; }

        public void FinishGame() { hasFinishedGame = true; }

        public bool AlreadyCheckTown() { return hasSawTown; }

        public bool AlreadyCheckChapel() { return hasSawChapel; }

        public bool AlreadyCheckSwamp() { return hasSawSwamp; }

        public bool AlreadyCheckMontainside() { return hasSawMontainside; }

        public bool AlreadyKilledZombie() { return hasKilledZombie; }

        public bool AlreadyUsedKey() { return hasUsedKey; }

        public bool AlreadyTalkedToWizard() { return hasTalkedToWizard; }

        public bool AlreadyTakedGem() { return hasTakedGem; }

        public bool AlreadyTakedZombieHead() { return hasTakedZombieHead; }

        public bool AlreadyClearGem() { return hasClearGem; }

        public bool AlreadyFinishedGame() { return hasFinishedGame; }
    }
}
