using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum DLinkStatus
    {
        Active,
        Reserve,
        Unitialized
    }
    // Double Linked List
    public abstract class DLink
    {
        #region Properties
        public DLink pDPrev;
        public DLink pDNext;
        public DLinkStatus status;
        #endregion
        #region Constructor
        protected DLink()
        {
            this.Clear();
        }
        #endregion
        public void Clear()
        {
            this.pDPrev = null;
            this.pDNext = null;
            this.status = DLinkStatus.Unitialized;
        }
    }
}
