using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        #region Properties
        string _versionNum = "2.0";
        int _windowWidth = 896;
        int _windowHeight = 1024;
        bool _areCollisionBoxesActive = false;   // Toggle collision boxes on/off
        #endregion
        #region Public Methods
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {   // Game Window Device setup
            this.SetWindowName(String.Format("Space Invaders {0}", _versionNum));
            this.SetWidthHeight(_windowWidth, _windowHeight);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            
        }
        
        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            GameManager.Create(_areCollisionBoxesActive);
        }
        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {
            TimerManager.Update(this.GetTime());
            InputManager.Update();
            GameObjectManager.Update();
            CollisionPairManager.Process();
            SoundManager.GetEngine().Update();
            DelayedGameObjectManager.Process();
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            SpriteBatchManager.Draw();
            FontManager.Draw();
        }
        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {   // TODO: Clean up Managers
            //TextureManager.Destroy();
        }
        #endregion
    }
}