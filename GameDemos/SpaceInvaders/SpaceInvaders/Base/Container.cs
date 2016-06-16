using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Container : DLink
    {
        #region Properties
        public int mNumActive;
        public int mNumReserve;
        private int mNumDeltaGrow;
        public int mNumTotalNodes;
        private CLink pReserve;
        public CLink pActive;   // only making this public for SpriteBatchManager.Draw();
        #endregion
        #region Protected Methods
        protected Container(int reserveSize = 5, int reserveIncrement = 1)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(reserveIncrement > 0);

            this.mNumDeltaGrow = reserveIncrement;
            this.mNumReserve = 0;
            this.mNumActive = 0;
            this.mNumTotalNodes = 0;
            this.pActive = null;
            this.pReserve = null;

            this.FillReserve(reserveSize);
        }
        protected CLink BaseAdd()
        {   // take off Reserve and move to Active
            CheckReserve();
            CLink pReserveNode = pReserve;
            if (pActive == null)
            {
                this.pReserve = this.pReserve.pCNext;
                CheckReserve();
                this.pReserve.pCPrev = null;
                pReserveNode.pCNext = null;
                pReserveNode.pCPrev = null;
                this.pActive = pReserveNode;
                this.pActive.status = CLinkStatus.Active;
            }
            else
            {
                this.pReserve = this.pReserve.pCNext;
                CheckReserve();
                this.pReserve.pCPrev = null;
                this.pActive.pCPrev = pReserveNode;
                pReserveNode.pCNext = this.pActive;
                pReserveNode.pCPrev = null;
                this.pActive = pReserveNode;
                this.pActive.status = CLinkStatus.Active;
            }
            --this.mNumReserve;
            ++this.mNumActive;
            return this.pActive;
        }
        protected CLink BaseFind(CLink node)
        {   // find node in Active list
            CLink foundNode = null;
            if (pActive == null)
            {
                foundNode = null;
            }
            else
            {
                CLink curr = pActive;
                while (curr != null)
                {
                    if (Compare(curr, node))
                    {
                        foundNode = curr;
                        break;
                    }
                    curr = curr.pCNext;
                }
            }
            return foundNode;
        }
        protected void BaseRemove(CLink node)
        {   // remove from Active, and add to Reserve
            if (node != null)
            {
                if (node.pCPrev == null)
                {   // Remove first node in Active List
                    if (this.pActive != null && this.pActive.pCNext != null)
                    {
                        this.pActive = this.pActive.pCNext;
                        this.pActive.pCPrev = null;
                    }
                    else
                    {   // Removing last Active node
                        this.pActive = null;
                    }
                }
                else if (node.pCNext == null)
                {   // Remove last node in Active list
                    node.pCPrev.pCNext = null;
                }
                else
                {   // Remove node somewhere in the middle
                    node.pCPrev.pCNext = node.pCNext;
                    node.pCNext.pCPrev = node.pCPrev;
                }
                // Clear foundNode and add to front of Reserve List
                node.Clear();
                node.status = CLinkStatus.Reserve;
                node.pCNext = this.pReserve;
                this.pReserve.pCPrev = node;
                this.pReserve = node;

                --this.mNumActive;
                ++this.mNumReserve;
            }
            else
            {
                //Debug.WriteLine(String.Format("Node {0} was not found.", node.GetHashCode()));
            }
        }
        protected void BaseDump()
        {   // Print current state
            Debug.WriteLine("------BEGIN BaseDump------");
            Debug.WriteLine(String.Format("Number of Reserved:{0}", mNumReserve));
            Debug.WriteLine(String.Format("Number of Active:{0}", mNumActive));
            Debug.WriteLine(String.Format("Total:{0}", mNumTotalNodes));
            CLink curr = pReserve;
            Debug.WriteLine("------RESERVE------");
            if (curr == null)
            {
                Debug.WriteLine("------EMPTY------");
                Debug.WriteLine("------------------");
            }
            else
            {
                while (curr != null)
                {
                    Debug.WriteLine(String.Format("pPrev({0})", curr.pCPrev == null ? "null" : curr.pCPrev.GetHashCode().ToString()));
                    this.DumpNode(curr);
                    Debug.WriteLine(String.Format("pNext({0})", curr.pCNext == null ? "null" : curr.pCNext.GetHashCode().ToString()));
                    Debug.WriteLine("");
                    curr = curr.pCNext;
                }
            }
            Debug.WriteLine("------ACTIVE------");
            curr = pActive;
            if (curr == null)
            {
                Debug.WriteLine("------EMPTY------");
                Debug.WriteLine("------------------");
            }
            else
            {
                while (curr != null)
                {
                    Debug.WriteLine(String.Format("pPrev({0})", curr.pCPrev == null ? "null" : curr.pCPrev.GetHashCode().ToString()));
                    this.DumpNode(curr);
                    Debug.WriteLine(String.Format("pNext({0})", curr.pCNext == null ? "null" : curr.pCNext.GetHashCode().ToString()));
                    Debug.WriteLine("");
                    curr = curr.pCNext;
                }
            }
            Debug.WriteLine("------END BaseDump------");
        }
        #endregion
        #region Abstract Methods
        abstract protected Boolean Compare(CLink dLink1, CLink dLink2);
        abstract protected CLink CreateNode();
        abstract protected void DumpNode(CLink node);
        #endregion
        #region Private Methods
        protected void FillReserve(int reserveSize)
        {   // Fill reserve with derived class node
            Debug.Assert(reserveSize > 0);
            // Only ever need to fill reserve list
            CLink curr = pReserve;
            for (int j = 0; j < reserveSize; j++)
            {
                if (curr == null)
                {
                    curr = this.CreateNode();
                    curr.status = CLinkStatus.Reserve;
                    ++this.mNumTotalNodes;
                    ++this.mNumReserve;
                }
                else
                {
                    CLink newNode = this.CreateNode();
                    newNode.status = CLinkStatus.Reserve;
                    newNode.pCNext = curr;
                    curr.pCPrev = newNode;
                    curr = newNode;
                    ++this.mNumTotalNodes;
                    ++this.mNumReserve;
                }
            }
            this.pReserve = curr;
        }
        private void CheckReserve()
        {   // Checks Reserve List and fills if necessary, ensures this.pReserve is never null
            if (this.pReserve == null)
            {
                this.FillReserve(this.mNumDeltaGrow);
            }
        }
        #endregion
    }
}
