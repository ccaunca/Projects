using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum SoundName
    {
        fastInvader1, fastInvader2, fastInvader3, fastInvader4,
        explosion, invaderKilled, shoot,
        ufoHighPitch, ufoLowPitch,
        Null, Uninitialized
    }
    public class Sound : DLink
    {
        public SoundName name;
        public IrrKlang.ISoundSource source;
        public Sound()
        {
            this.name = SoundName.Uninitialized;
            this.source = null;
        }
        public Sound(SoundName soundName)
        {
            this.name = soundName;
            this.source = LookupSource(soundName);
        }
        private IrrKlang.ISoundSource LookupSource(SoundName soundName)
        {
            IrrKlang.ISoundSource pSource = null;
            IrrKlang.ISoundEngine pSoundEngine = SoundManager.GetEngine();
            switch (soundName)
            {
                case SoundName.explosion :
                    pSource = pSoundEngine.AddSoundSourceFromFile("explosion.wav");
                    break;
                case SoundName.fastInvader1 :
                    pSource = pSoundEngine.AddSoundSourceFromFile("fastinvader1.wav");
                    break;
                case SoundName.fastInvader2 :
                    pSource = pSoundEngine.AddSoundSourceFromFile("fastinvader2.wav");
                    break;
                case SoundName.fastInvader3 :
                    pSource = pSoundEngine.AddSoundSourceFromFile("fastinvader3.wav");
                    break;
                case SoundName.fastInvader4 :
                    pSource = pSoundEngine.AddSoundSourceFromFile("fastinvader4.wav");
                    break;
                case SoundName.invaderKilled :
                    pSource = pSoundEngine.AddSoundSourceFromFile("invaderkilled.wav");
                    break;
                case SoundName.shoot :
                    pSource = pSoundEngine.AddSoundSourceFromFile("shoot.wav");
                    break;
                case SoundName.ufoHighPitch :
                    pSource = pSoundEngine.AddSoundSourceFromFile("ufo_highpitch.wav");
                    break;
                case SoundName.ufoLowPitch :
                    pSource = pSoundEngine.AddSoundSourceFromFile("ufo_lowpitch.wav");
                    break;
                case SoundName.Null :
                    pSource = null;
                    break;
                default :
                    Debug.Assert(false);
                    break;
            }
            return pSource;
        }

        public void Set(SoundName soundName)
        {
            this.name = soundName;
            this.source = LookupSource(soundName);
        }
    }
}
