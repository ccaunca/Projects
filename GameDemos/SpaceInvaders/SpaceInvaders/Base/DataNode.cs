using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum DataNodeName
    {
        Cat, Dog, Bird, Fish, Rabbit, Uninitialized
    };

    public class DataNode : DLink
    {
        #region Properties
        public DataNodeName name;
        public int x;
        #endregion
        #region Constructors
        public DataNode(DataNodeName val, int data)
        {
            this.Set(val, data);
        }
        public void Set(DataNodeName val, int data)
        {
            this.name = val;
            this.x = data;
        }
        #endregion
    }
}
