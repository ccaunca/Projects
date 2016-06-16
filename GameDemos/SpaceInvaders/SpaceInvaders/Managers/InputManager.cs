using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputManager
    {
        private static InputManager pInstance = null;
        public InputSubject SubjectArrowLeft;
        public InputSubject SubjectArrowRight;
        public InputSubject SubjectSpacebar;
        public InputSubject SubjectOne;
        public InputSubject SubjectTwo;
        private bool SpaceKeyPrev;
        private InputManager()
        {
            this.SubjectArrowLeft = new InputSubject();
            this.SubjectArrowRight = new InputSubject();
            this.SubjectSpacebar = new InputSubject();
            this.SubjectOne = new InputSubject();
            this.SubjectTwo = new InputSubject();
            this.SpaceKeyPrev = false;
        }
        private static InputManager GetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputManager();
            }
            return pInstance;
        }
        public static InputSubject GetArrowLeftSubject()
        {
            InputManager inputMan = InputManager.GetInstance();
            return inputMan.SubjectArrowLeft;
        }
        public static InputSubject GetArrowRightSubject()
        {
            InputManager inputMan = InputManager.GetInstance();
            return inputMan.SubjectArrowRight;
        }
        public static InputSubject GetSpaceSubject()
        {
            InputManager inputMan = InputManager.GetInstance();
            return inputMan.SubjectSpacebar;
        }
        public static InputSubject GetOneSubject()
        {
            InputManager inputMan = InputManager.GetInstance();
            return inputMan.SubjectOne;
        }
        public static InputSubject GetTwoSubject()
        {
            InputManager inputMan = InputManager.GetInstance();
            return inputMan.SubjectTwo;
        }
        public static void Update()
        {
            InputManager inputMan = InputManager.GetInstance();
            bool spaceKey = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
            if (spaceKey == true && inputMan.SpaceKeyPrev == false)
            {
                inputMan.SubjectSpacebar.NotifyObservers();
            }
            inputMan.SpaceKeyPrev = spaceKey;
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                inputMan.SubjectArrowLeft.NotifyObservers();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                inputMan.SubjectArrowRight.NotifyObservers();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1) == true)
            {
                inputMan.SubjectOne.NotifyObservers();
            }
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2) == true)
            {
                inputMan.SubjectTwo.NotifyObservers();
            }
            inputMan.SpaceKeyPrev = spaceKey;
        }        
    }
}