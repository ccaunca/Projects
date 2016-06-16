using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum BombType
    {
        BombRoot, Bomb, Uninitialized
    }
    public class BombCategory : GameObject
    {
        private BombType type;
        protected BombCategory(GameObjectName goName, SpriteBaseName sName, BombType type, int idx)
            : base(goName, sName, idx)
        {
            this.type = type;
        }

        static public GameObject GetBomb(GameObject go1, GameObject go2)
        {
            GameObject pBomb;
            if (go1 is BombCategory)
            {
                pBomb = (GameObject)go1;
            }
            else
            {
                pBomb = (GameObject)go2;
            }
            Debug.Assert(pBomb is BombCategory);
            return pBomb;
        }

        public override void Accept(Visitor other)
        {
            throw new NotImplementedException();
        }
    }
}
