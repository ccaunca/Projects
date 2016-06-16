using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    
    public class SoundManager : Manager
    {
        private static SoundManager pInstance = null;
        private static IrrKlang.ISoundEngine soundEngine = null;
        private SoundManager(int reserveSize = 9, int reserveGrowth = 1)
            : base (reserveSize, reserveGrowth)
        {
            
        }
        public static SoundManager GetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }
        public static IrrKlang.ISoundEngine GetEngine()
        {
            Debug.Assert(soundEngine != null);
            return soundEngine;
        }
        public static void PlaySound(Sound sound)
        {
            IrrKlang.ISoundEngine sndEngine = GetEngine();
            sndEngine.Play2D(sound.source, false, false, false);
        }
        public static void Create(int reserveSize = 9, int reserveGrowth = 1)
        {
            if (pInstance == null)
            {
                soundEngine = new IrrKlang.ISoundEngine();
                pInstance = new SoundManager(reserveSize, reserveGrowth);
            }
        }
        public static Sound Add(SoundName soundName)
        {
            SoundManager soundMan = SoundManager.GetInstance();
            Sound pSound = (Sound)soundMan.BaseAdd();
            pSound.Set(soundName);
            return pSound;
        }
        public static Sound Find(SoundName soundName)
        {
            SoundManager soundMan = SoundManager.GetInstance();
            Sound pSound = (Sound)soundMan.BaseFind(new Sound { name = soundName });
            return pSound;
        }
        protected override bool Compare(DLink dLink1, DLink dLink2)
        {
            bool result = false;
            Sound pSound1 = (Sound)dLink1;
            Sound pSound2 = (Sound)dLink2;
            if (pSound1.name == pSound2.name)
            {
                result = true;
            }
            return result;
        }

        protected override DLink CreateNode()
        {
            Sound pSound = new Sound(SoundName.Null);
            return pSound;
        }

        protected override void DumpNode(DLink node)
        {
            Sound pSound = (Sound)node;
            Debug.WriteLine("Sound {0}: name {1}", pSound.GetHashCode(), pSound.name);
        }
    }
}
