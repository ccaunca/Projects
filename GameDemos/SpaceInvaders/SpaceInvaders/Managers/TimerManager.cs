using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimerManager : Manager
    {
        public static readonly Random random = new Random();
        public float currentTime;
        public static float lagTime;
        private static TimerManager pInstance;
        private TimerManager(int reserveSize = 5, int reserveIncrement = 1)
            : base(reserveSize, reserveIncrement)
        {
            lagTime = 5.0f;
        }
        #region Public Methods
        public static TimerEvent Add(TimerEventName name, float triggerTime, float deltaTime, Command command)
        {
            TimerManager timerMan = TimerManager.GetInstance();
            TimerEvent timerEvent = (TimerEvent)timerMan.BaseAdd();
            Debug.Assert(timerEvent != null);
            timerEvent.Set(name, triggerTime, deltaTime, command);
            timerMan.BaseInsertSorted(timerEvent);
            return timerEvent;
        }
        public static void Create(int reserveSize = 3, int reserveIncrement = 1)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);
            if (pInstance == null)
            {
                pInstance = new TimerManager(reserveSize, reserveIncrement);
            }
        }
        public static void Dump()
        {
            TimerManager timerMan = TimerManager.GetInstance();
             timerMan.BaseDump();
        }
        public static TimerEvent Find(TimerEventName name)
        {
            TimerManager timerMan = TimerManager.GetInstance();
            return (TimerEvent)timerMan.BaseFind((DLink)new TimerEvent { name = name });
        }
        public static float GetCurrentTime()
        {
            TimerManager timerMan = TimerManager.GetInstance();
            return timerMan.currentTime;
        }
        public static void InitializeTimerManager()
        {
            Grid pGrid = (Grid)GameObjectManager.Find(GameObjectName.Grid);
            float marchSpeed = pGrid.marchSpeed;
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
            TimerManager.Add(TimerEventName.MoveGrid, GetCurrentTime() + marchSpeed, marchSpeed, new MoveGridEvent());
            TimerManager.Add(TimerEventName.AnimateSquids, GetCurrentTime() + marchSpeed, marchSpeed, animSpriteSquids);
            TimerManager.Add(TimerEventName.AnimateCrabs, GetCurrentTime() + marchSpeed, marchSpeed, animSpriteCrabs);
            TimerManager.Add(TimerEventName.AnimateOctopi, GetCurrentTime() + marchSpeed, marchSpeed, animSpriteOctopi);
            TimerManager.Add(TimerEventName.PlayFastInvaders4, GetCurrentTime() + marchSpeed, 4 * marchSpeed, playFastInvader4);
            TimerManager.Add(TimerEventName.PlayFastInvaders1, GetCurrentTime() + 2 * marchSpeed, 4 * marchSpeed, playFastInvader1);
            TimerManager.Add(TimerEventName.PlayFastInvaders2, GetCurrentTime() + 3 * marchSpeed, 4 * marchSpeed, playFastInvader2);
            TimerManager.Add(TimerEventName.PlayFastInvaders3, GetCurrentTime() + 4 * marchSpeed, 4 * marchSpeed, playFastInvader3);
            TimerManager.Add(TimerEventName.BombSpawn, GetCurrentTime() + marchSpeed, pGrid.bombFrequency, new BombSpawnEvent(pGrid));
            TimerManager.Add(TimerEventName.UFOSpawn, GetCurrentTime() + (float)random.Next(5, 10), (float)random.Next(5, 10), new UFOSpawnEvent());
        }
        public static void UpdateTimerManager(Grid pGrid)
        {
            float decayFactor = pGrid.decayFactor;
            float bombDecayFactor = pGrid.bombDecayFactor;
            pGrid.UpdateMarchSpeed(decayFactor);
            pGrid.UpdateBombFrequency();

            float newTriggerTime;
            float newDeltaTime;
            Command cmd;
            // need to update the game TimerEvents to accommodate updated values
            TimerEvent timerEvent = TimerManager.Find(TimerEventName.MoveGrid);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor;
                newDeltaTime = timerEvent.deltaTime - decayFactor;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.MoveGrid, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.AnimateSquids);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor;
                newDeltaTime = timerEvent.deltaTime - decayFactor;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.AnimateSquids, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.AnimateCrabs);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor;
                newDeltaTime = timerEvent.deltaTime - decayFactor;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.AnimateCrabs, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.AnimateOctopi);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor;
                newDeltaTime = timerEvent.deltaTime - decayFactor;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.AnimateOctopi, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.PlayFastInvaders4);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor * 4;
                newDeltaTime = timerEvent.deltaTime - decayFactor * 4;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.PlayFastInvaders4, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.PlayFastInvaders1);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor * 4;
                newDeltaTime = timerEvent.deltaTime - decayFactor * 4;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.PlayFastInvaders1, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.PlayFastInvaders2);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor * 4;
                newDeltaTime = timerEvent.deltaTime - decayFactor * 4;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.PlayFastInvaders2, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.PlayFastInvaders3);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - decayFactor * 4;
                newDeltaTime = timerEvent.deltaTime - decayFactor * 4;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.PlayFastInvaders3, newTriggerTime, newDeltaTime, cmd);
            }

            timerEvent = TimerManager.Find(TimerEventName.BombSpawn);
            if (timerEvent != null)
            {
                newTriggerTime = timerEvent.triggerTime - bombDecayFactor;
                newDeltaTime = timerEvent.deltaTime - bombDecayFactor;
                cmd = timerEvent.command;
                TimerManager.Remove(timerEvent);
                TimerManager.Add(TimerEventName.BombSpawn, newTriggerTime, newDeltaTime, cmd);
            }
        }
        public static void ClearTimerManager()
        {
            TimerManager.Remove(TimerManager.Find(TimerEventName.BombSpawn));
            TimerManager.Remove(TimerManager.Find(TimerEventName.MoveGrid));
            TimerManager.Remove(TimerManager.Find(TimerEventName.AnimateCrabs));
            TimerManager.Remove(TimerManager.Find(TimerEventName.AnimateOctopi));
            TimerManager.Remove(TimerManager.Find(TimerEventName.AnimateSquids));
            TimerManager.Remove(TimerManager.Find(TimerEventName.PlayFastInvaders1));
            TimerManager.Remove(TimerManager.Find(TimerEventName.PlayFastInvaders2));
            TimerManager.Remove(TimerManager.Find(TimerEventName.PlayFastInvaders3));
            TimerManager.Remove(TimerManager.Find(TimerEventName.PlayFastInvaders4));
            TimerManager.Remove(TimerManager.Find(TimerEventName.UFOSpawn));
            TimerManager.Remove(TimerManager.Find(TimerEventName.UFOSpawnBomb));
            TimerManager.Remove(TimerManager.Find(TimerEventName.PlayUFOSound));
        }
        public static void Remove(DLink node)
        {
            if (node != null)
            {
                TimerManager timerMan = TimerManager.GetInstance();
                timerMan.BaseRemove(node);
            }
        }
        public static void UpdateDelta(TimerEventName timerEventName)
        {
            TimerEvent pTimerEvent = TimerManager.Find(timerEventName);
            float newTriggerTime = pTimerEvent.triggerTime;
            float newDeltaTime = pTimerEvent.deltaTime - 0.05f;
            Command cmd = pTimerEvent.command;
            TimerManager.Remove(pTimerEvent);
        }
        public static void Update(float currentTime)
        {
            TimerManager timerMan = TimerManager.GetInstance();
            timerMan.currentTime = currentTime;
            DLink curr = timerMan.pActive;
            TimerEvent te = (TimerEvent)curr;
            while (curr != null)
            {
                if (timerMan.currentTime >= te.triggerTime)
                {
                    //Debug.WriteLine("Processing TimerEvent {0} @ {1}", te.name, currentTime);
                    te.Process(currentTime);
                    timerMan.BaseRemove(te);
                    if (te.name != TimerEventName.RemoveGameObject && te.name != TimerEventName.SetGameState &&
                        te.name != TimerEventName.InitiateTimerEvents && te.name != TimerEventName.GameStart &&
                        te.name != TimerEventName.RemoveUFO && te.name != TimerEventName.StopUFOSound)
                    {
                        if (te.name == TimerEventName.UFOSpawn)
                        {
                            float random = UFOManager.GetRandom().Next(5, 10);
                            Debug.WriteLine("UFO Spawn in {0} secs", random);
                            te.deltaTime = random;
                        }
                        Add(te.name, te.triggerTime + te.deltaTime, te.deltaTime, te.command);
                    }
                }
                curr = curr.pDNext;
            }
        }
        
        #endregion
        private static TimerManager GetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new TimerManager();
            }
            return pInstance;
        }
        #region Base Class Implementation
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            bool result = false;
            TimerEvent timerEvent1 = (TimerEvent)dLink1;
            TimerEvent timerEvent2 = (TimerEvent)dLink2;
            if (timerEvent1.name == timerEvent2.name)
            {
                result = true;
            }
            return result;
        }

        protected override DLink CreateNode()
        {
            DLink dLink = new TimerEvent();
            return dLink;
        }

        protected override void DumpNode(DLink node)
        {
            Debug.Assert(node != null);
            TimerEvent tEvent = (TimerEvent)node;
            Debug.WriteLine(String.Format("TimerEvent:({0})", tEvent.GetHashCode()));
            Debug.WriteLine(String.Format("TimerEventName:{0}", tEvent.name.ToString()));
            Debug.WriteLine(String.Format("triggerTime:{0}", tEvent.triggerTime.ToString()));
            Debug.WriteLine(String.Format("deltaTime:{0}", tEvent.deltaTime.ToString()));
        }
        #endregion
    }
}
