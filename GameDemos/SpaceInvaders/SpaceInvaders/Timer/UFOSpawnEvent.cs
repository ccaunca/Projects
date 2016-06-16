using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFOSpawnEvent : Command
    {
        public GameObject pUFORoot;
        public SpriteBatch sbAliens;
        public SpriteBatch sbBoxes;
        private UFO pUFO;
        public UFOSpawnEvent()
        {
            this.sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
            Debug.Assert(sbAliens != null);

            this.sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
            Debug.Assert(sbBoxes != null);
        }
        public override void execute(float currentTime)
        {
            SpawnUFO();
        }
        private void SpawnUFO()
        {
            int random = UFOManager.GetRandom().Next(7, 10);
            this.pUFO = UFOManager.ActivateUFO(GameManager.GetCollisionBoxes());
            TimerManager.Add(TimerEventName.PlayUFOSound, TimerManager.GetCurrentTime() + 0.2f, 0.2f, new StartUFOSoundCommand());
            TimerManager.Add(TimerEventName.UFOSpawnBomb, TimerManager.GetCurrentTime() + (float)UFOManager.GetRandom().Next(1, 6), TimerManager.GetCurrentTime() + (float)UFOManager.GetRandom().Next(1, 6), new UFOBombSpawnEvent());
        }
    }
}
