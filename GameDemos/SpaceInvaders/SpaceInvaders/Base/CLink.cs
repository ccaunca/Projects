using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public enum CLinkStatus
    {
        Active,
        Reserve,
        Unitialized
    }
    // Double Linked List
    public abstract class CLink
    {
        #region Properties
        public CLink pCPrev;
        public CLink pCNext;
        public CLinkStatus status;
        #endregion
        #region Constructor
        protected CLink()
        {
            this.Clear();
        }
        #endregion
        public void Clear()
        {
            this.pCPrev = null;
            this.pCNext = null;
            this.status = CLinkStatus.Unitialized;
        }
    }
}
