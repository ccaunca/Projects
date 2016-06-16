using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UpdateLivesObserver : CollisionObserver
    {
        public override void Update()
        {
            int lives = ScoreManager.UpdateLives();
            if (lives == 0)
            {
                Debug.WriteLine("Game Over!");
            }
            else
            {
                PCSTree pRootTree = GameObjectManager.GetRootTree();
                SpriteBatch sbBoxes = SpriteBatchManager.Find(SpriteBatchName.Boxes);
                SpriteBatch sbAliens = SpriteBatchManager.Find(SpriteBatchName.Aliens);
                //MissileRoot pMissileRoot = new MissileRoot(GameObjectName.MissileRoot, SpriteBaseName.Null, 0.0f, 0.0f, 0);
                //pMissileRoot.ActivateCollisionSprite(sbBoxes);
                ShipRoot pShipRoot = (ShipRoot)GameObjectManager.Find(GameObjectName.ShipRoot);
                pShipRoot.ActivateCollisionSprite(sbBoxes);
                pShipRoot.ActivateGameSprite(sbAliens);
                ShipManager.Create();
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
                TimerManager.Add(TimerEventName.MoveGrid, marchSpeed, marchSpeed, new MoveGridEvent());
                TimerManager.Add(TimerEventName.AnimateSquids, marchSpeed, marchSpeed, animSpriteSquids);
                TimerManager.Add(TimerEventName.AnimateCrabs, marchSpeed, marchSpeed, animSpriteCrabs);
                TimerManager.Add(TimerEventName.AnimateOctopi, marchSpeed, marchSpeed, animSpriteOctopi);
                TimerManager.Add(TimerEventName.PlayFastInvaders4, marchSpeed, 4 * marchSpeed, playFastInvader4);
                TimerManager.Add(TimerEventName.PlayFastInvaders1, 2 * marchSpeed, 4 * marchSpeed, playFastInvader1);
                TimerManager.Add(TimerEventName.PlayFastInvaders2, 3 * marchSpeed, 4 * marchSpeed, playFastInvader2);
                TimerManager.Add(TimerEventName.PlayFastInvaders3, 4 * marchSpeed, 4 * marchSpeed, playFastInvader3);
                TimerManager.Add(TimerEventName.BombSpawn, 1.0f, 1.0f, new BombSpawnEvent(pGrid));
            }
        }
    }
}
