using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public enum TimerEventName
    {
        AnimateOctopi, AnimateCrabs, AnimateSquids, MoveGrid,
        PlayExplosionSound, PlayFastInvaders1, PlayFastInvaders2, PlayFastInvaders3, PlayFastInvaders4,
        RemoveGameObject, BombSpawn, TypeLetter, SetGameState,
        InitiateTimerEvents, GameStart,
        UFOSpawn, RemoveUFO, PlayUFOSound, StopUFOSound, UFOSpawnBomb,
        Uninitialized
    }
    public class TimerEvent : DLink
    {
        public float triggerTime;
        public float deltaTime;
        public Command command;
        public TimerEventName name;
        public TimerEvent()
            : base()
        {
            this.triggerTime = 1.0f;
            this.deltaTime = 1.0f;
            this.command = null;
            this.name = TimerEventName.Uninitialized;
        }
        public TimerEvent(TimerEventName name, Command command, float tTime, float dTime)
        {
            this.triggerTime = tTime;
            this.deltaTime = dTime;
            this.command = command;
            this.name = name;
        }
        ~TimerEvent()
        {
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
            this.command = null;
            this.name = TimerEventName.Uninitialized;
        }
        public void Set(TimerEventName name, float triggerTime, float deltaTime, Command command)
        {
            this.name = name;
            this.triggerTime = triggerTime;
            this.deltaTime = deltaTime;
            this.command = command;
        }
        public void Process(float currentTime)
        {
            this.command.execute(currentTime);
        }
    }
}
