using System;

namespace SpaceInvaders
{
    class FillTimerManager : Command
    {
        public override void execute(float currentTime)
        {
            AnimationSprite animSpriteSquids = new AnimationSprite(SpriteBaseName.Squid);
            animSpriteSquids.Attach(ImageName.SquidA);
            animSpriteSquids.Attach(ImageName.SquidB);
            AnimationSprite animSpriteCrabs = new AnimationSprite(SpriteBaseName.Crab);
            animSpriteCrabs.Attach(ImageName.CrabA);
            animSpriteCrabs.Attach(ImageName.CrabB);
            AnimationSprite animSpriteOctopi = new AnimationSprite(SpriteBaseName.Octopus);
            animSpriteOctopi.Attach(ImageName.OctopusA);
            animSpriteOctopi.Attach(ImageName.OctopusB);
            SoundCommand playFastInvader1 = new SoundCommand(SoundName.fastInvader1);
            SoundCommand playFastInvader2 = new SoundCommand(SoundName.fastInvader2);
            SoundCommand playFastInvader3 = new SoundCommand(SoundName.fastInvader3);
            SoundCommand playFastInvader4 = new SoundCommand(SoundName.fastInvader4);
            Grid pGrid = (Grid)GameObjectManager.Find(GameObjectName.Grid);
            float marchSpeed = pGrid.marchSpeed;
            float bombFrequency = pGrid.bombFrequency;
            TimerManager.Dump();
            TimerManager.Add(TimerEventName.MoveGrid, marchSpeed, marchSpeed, new MoveGridEvent());
            TimerManager.Add(TimerEventName.AnimateSquids, marchSpeed, marchSpeed, animSpriteSquids);
            TimerManager.Add(TimerEventName.AnimateCrabs, marchSpeed, marchSpeed, animSpriteCrabs);
            TimerManager.Add(TimerEventName.AnimateOctopi, marchSpeed, marchSpeed, animSpriteOctopi);
            TimerManager.Add(TimerEventName.PlayFastInvaders4, marchSpeed, 4 * marchSpeed, playFastInvader4);
            TimerManager.Add(TimerEventName.PlayFastInvaders1, 2 * marchSpeed, 4 * marchSpeed, playFastInvader1);
            TimerManager.Add(TimerEventName.PlayFastInvaders2, 3 * marchSpeed, 4 * marchSpeed, playFastInvader2);
            TimerManager.Add(TimerEventName.PlayFastInvaders3, 4 * marchSpeed, 4 * marchSpeed, playFastInvader3);
            TimerManager.Add(TimerEventName.BombSpawn, bombFrequency, bombFrequency, new BombSpawnEvent(pGrid));
            TimerManager.Add(TimerEventName.UFOSpawn, TimerManager.GetCurrentTime() + (float)UFOManager.GetRandom().Next(5, 10), (float)UFOManager.GetRandom().Next(5, 10), new UFOSpawnEvent());
        }
    }
}
