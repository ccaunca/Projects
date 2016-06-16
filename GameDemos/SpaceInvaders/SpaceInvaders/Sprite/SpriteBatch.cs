using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum SpriteBatchName
    {
        Aliens,
        Squids,
        Octopi,
        Crabs,
        Background,
        Boxes,
        Shields,
        Missiles,
        UFO,
        Ships,
        Walls,
        Splats,
        Fonts
    }
    public class SpriteBatch : Container
    {
        public SpriteBatchName name;

        public SpriteBatch(int reserveSize = 5, int reserveIncrement = 1)
            : base(reserveSize, reserveIncrement)
        {

        }
        public void Attach(SpriteBase pSpriteBase)
        {
            Debug.Assert(pSpriteBase != null);
            SpriteBatchNode sbNode = (SpriteBatchNode)this.BaseAdd();
            Debug.Assert(sbNode != null);

            sbNode.Set(pSpriteBase, this);
        }
        public void Remove(SpriteBatchNode sbNode)
        {
            Debug.Assert(sbNode != null);
            this.BaseRemove(sbNode);
        }
        public void Set(SpriteBatchName name, int reserveSize, int reserveIncrement)
        {
            this.name = name;
            this.FillReserve(reserveSize);
        }

        protected override CLink CreateNode()
        {
            return new SpriteBatchNode();
        }

        protected override bool Compare(CLink cLink1, CLink cLink2)
        {
            bool result = false;
            //SpriteBatchNode spNode1 = (SpriteBatchNode)cLink1;
            //SpriteBatchNode spNode2 = (SpriteBatchNode)cLink2;
            //if (spNode1.name == spNode2.name)
            //{
            //    result = true;
            //}
            return result;
        }

        protected override void DumpNode(CLink node)
        {
            SpriteBatchNode spNode = (SpriteBatchNode)node;
            Debug.WriteLine(String.Format("\t\tSpriteBatchNode:({0})", spNode.GetHashCode()));
            Debug.WriteLine(String.Format("\t\tstatus:{0}", spNode.status));
        }
    }
}
