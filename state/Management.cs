using System;

namespace Reign_of_Grelok.state
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

        public void SeeTown() { this.hasSawTown = true; }

        public void SeeChapel() { this.hasSawChapel = true; }

        public void SeeSwamp() { this.hasSawSwamp = true; }

        public void SeeMontainside() { this.hasSawMontainside = true; }

        public void KillZombie() { this.hasKilledZombie = true; }

        public void UseKey() { this.hasUsedKey = true; }

        public void GetRawGem() { this.hasTakedGem = true; }

        public void TalkToWizard() {  this.hasTalkedToWizard = true; }

        public void ClearGem() {  this.hasClearGem = true; }

        public void FinishGame() { this.hasFinishedGame = true; }

        public bool AlreadyCheckTown() { return this.hasSawTown; }

        public bool AlreadyCheckChapel() { return this.hasSawChapel; }

        public bool AlreadyCheckSwamp() { return this.hasSawSwamp; }

        public bool AlreadyCheckMontainside() { return this.hasSawMontainside; }

        public bool AlreadyKilledZombie() { return this.hasKilledZombie; }

        public bool AlreadyUsedKey() { return this.hasUsedKey; }

        public bool AlreadyTalkedToWizard() { return this.hasTalkedToWizard; }

        public bool AlreadyTakedGem() { return this.hasTakedGem; }

        public bool AlreadyClearGem() { return this.hasClearGem; }

        public bool AlreadyFinishedGame() { return this.hasFinishedGame; }
    }
}
