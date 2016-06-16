using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class NullGameObject : GameObject
    {
        public NullGameObject()
            : base(GameObjectName.Uninitialized, SpriteBaseName.Null, 0)
        {

        }
        public override int getIndex()
        {
            throw new NotImplementedException();
        }

        public override void Accept(Visitor other)
        {
            throw new NotImplementedException();
        }
    }
}
