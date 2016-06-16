using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum MissileType
    {
        Missile, MissileRoot, Uninitialized
    }
    public abstract class MissileCategory : GameObject
    {
        protected MissileType type;
        protected MissileCategory(GameObjectName goName, SpriteBaseName sName, MissileType type, int idx)
            : base(goName, sName, idx)
        {
            this.type = type;
        }
        public override void Accept(Visitor other)
        {
            throw new NotImplementedException();
        }
        static public GameObject GetMissile(GameObject go1, GameObject go2)
        {
            GameObject pMissile;
            if (go1 is MissileCategory)
            {
                pMissile = (GameObject)go1;
            }
            else
            {
                pMissile = (GameObject)go2;
            }

            Debug.Assert(pMissile is MissileCategory);

            return pMissile;
        }
        public static GameObject GetNonMissile(GameObject go1, GameObject go2)
        {
            GameObject pNonMissile;
            if (go1 is MissileCategory)
            {
                pNonMissile = (GameObject)go2;
            }
            else
            {
                pNonMissile = (GameObject)go1;
            }
            Debug.Assert(!(pNonMissile is MissileCategory));
            return pNonMissile;
        }
    }
}
