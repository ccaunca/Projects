using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Manager
    {
        #region Properties
        private int mNumActive;
        private int mNumReserve;
        private int mNumDeltaGrow;
        private int mNumTotalNodes;
        private DLink pReserve;
        public DLink pActive;   // only making this public for SpriteBatchManager.Draw();
        #endregion
        #region Protected Methods
        protected Manager(int reserveSize = 5, int reserveIncrement = 1)
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
        protected DLink BaseAdd()
        {   // take off Reserve and move to Active
            CheckReserve();
            DLink pReserveNode = pReserve;
            if (pActive == null)
            {
                this.pReserve = this.pReserve.pDNext;
                CheckReserve();
                this.pReserve.pDPrev = null;
                pReserveNode.pDNext = null;
                pReserveNode.pDPrev = null;
                this.pActive = pReserveNode;
                this.pActive.status = DLinkStatus.Active;
            }
            else
            {
                this.pReserve = this.pReserve.pDNext;
                CheckReserve();
                this.pReserve.pDPrev = null;
                this.pActive.pDPrev = pReserveNode;
                pReserveNode.pDNext = this.pActive;
                pReserveNode.pDPrev = null;
                this.pActive = pReserveNode;
                this.pActive.status = DLinkStatus.Active;
            }
            --this.mNumReserve;
            ++this.mNumActive;
            return this.pActive;
        }
        protected void BaseDestroy()
        {
            DLink pNode;
            DLink tmpNode;

            pNode = this.pActive;
            while(pNode != null)
            {
                tmpNode = pNode;
                pNode = pNode.pDNext;
                RemoveFromList(this.pActive, tmpNode);
                tmpNode = null;
                this.mNumTotalNodes--;
            }
            pNode = this.pReserve;
            while(pNode != null)
            {
                tmpNode = pNode;
                pNode = pNode.pDNext;
                RemoveFromList(this.pReserve, tmpNode);
                tmpNode = null;
                this.mNumTotalNodes--;
            }
        }
        protected DLink BaseFind(DLink node)
        {   // find node in Active list
            DLink foundNode = null;
            if (pActive == null)
            {
                foundNode = null;
            }
            else
            {
                DLink curr = pActive;
                while (curr != null)
                {
                    if (Compare(curr, node))
                    {
                        foundNode = curr;
                        break;
                    }
                    curr = curr.pDNext;
                }
            }
            return foundNode;
        }
        protected void BaseInsertSorted(DLink node)
        {   // only used in TimerManager
            Debug.Assert(node != null);
            DLink secondNode = pActive.pDNext;   // pActive should be pointing to passed in node
            if (secondNode == null)
            {   // only 1 node, so no need to rearrange dlinks
                return;
            }
            else
            {
                node.pDNext = null; // remove first node
                pActive = secondNode;
                pActive.pDPrev = null;
                DLink curr = pActive;
                while (curr != null)
                {
                    TimerEvent teCurr = (TimerEvent)curr;
                    TimerEvent teNewNode = (TimerEvent)node;
                    if (teNewNode.triggerTime <= teCurr.triggerTime)
                    {
                        node.pDNext = curr;
                        node.pDPrev = curr.pDPrev;
                        curr.pDPrev = node;
                        if (node.pDPrev == null)
                        {
                            pActive = node;
                            break;
                        }
                        else
                        {
                            node.pDPrev.pDNext = node;
                            // set new pActive
                            DLink tmp = node;
                            while (tmp != null)
                            {
                                if (tmp.pDPrev == null)
                                {
                                    break;
                                }
                                tmp = tmp.pDPrev;
                            }
                            pActive = tmp;
                            break;
                        }
                    }
                    else
                    {
                        if (curr.pDNext == null)
                        {
                            curr.pDNext = node;
                            node.pDPrev = curr;
                            node.pDNext = null;
                            break;
                        }
                    }
                    curr = curr.pDNext;
                }
            }
        }
        protected void BaseRemove(DLink node)
        {   // remove from Active, and add to Reserve
            DLink foundNode = BaseFind(node);
            if (foundNode != null)
            {
                if (foundNode.pDPrev == null)
                {   // Remove first node in source List
                    this.pActive = this.pActive.pDNext;
                    if (this.pActive != null)
                    {
                        this.pActive.pDPrev = null;
                    }
                }
                else if (foundNode.pDNext == null)
                {   // Remove last node in source list
                    foundNode.pDPrev.pDNext = null;
                }
                else
                {   // Remove node somewhere in the middle
                    foundNode.pDPrev.pDNext = foundNode.pDNext;
                    foundNode.pDNext.pDPrev = foundNode.pDPrev;
                }
                // Clear foundNode and add to front of Reserve List
                foundNode.Clear();
                foundNode.status = DLinkStatus.Reserve;
                foundNode.pDNext = this.pReserve;
                this.pReserve.pDPrev = foundNode;
                this.pReserve = foundNode;

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
            DLink curr = pReserve;
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
                    Debug.WriteLine(String.Format("pPrev({0})", curr.pDPrev == null ? "null" : curr.pDPrev.GetHashCode().ToString()));
                    this.DumpNode(curr);
                    Debug.WriteLine(String.Format("pNext({0})", curr.pDNext == null ? "null" : curr.pDNext.GetHashCode().ToString()));
                    Debug.WriteLine("");
                    curr = curr.pDNext;
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
                    Debug.WriteLine(String.Format("pPrev({0})", curr.pDPrev == null ? "null" : curr.pDPrev.GetHashCode().ToString()));
                    this.DumpNode(curr);
                    Debug.WriteLine(String.Format("pNext({0})", curr.pDNext == null ? "null" : curr.pDNext.GetHashCode().ToString()));
                    Debug.WriteLine("");
                    curr = curr.pDNext;
                }
            }
            Debug.WriteLine("------END BaseDump------");
        }
        #endregion
        #region Abstract Methods
        abstract protected Boolean Compare(DLink dLink1, DLink dLink2);
        abstract protected DLink CreateNode();
        abstract protected void DumpNode(DLink node);
        #endregion
        #region Private Methods
        private void FillReserve(int reserveSize)
        {   // Fill reserve with derived class node
            Debug.Assert(reserveSize > 0);
            // Only ever need to fill reserve list
            DLink curr = pReserve;
            for (int j = 0; j < reserveSize; j++)
            {
                if (curr == null)
                {
                    curr = this.CreateNode();
                    curr.status = DLinkStatus.Reserve;
                    ++this.mNumTotalNodes;
                    ++this.mNumReserve;
                }
                else
                {
                    DLink newNode = this.CreateNode();
                    newNode.status = DLinkStatus.Reserve;
                    newNode.pDNext = curr;
                    curr.pDPrev = newNode;
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
        private void RemoveFromList(DLink source, DLink pNode)
        {
            if (pNode.pDPrev == null)
            {   // Remove first node in source List
                source = source.pDNext;
                source.pDPrev = null;
            }
            else if (pNode.pDNext == null)
            {   // Remove last node in source list
                pNode.pDPrev.pDNext = null;
            }
            else
            {   // Remove node somewhere in the middle
                pNode.pDPrev.pDNext = pNode.pDNext;
                pNode.pDNext.pDPrev = pNode.pDPrev;
            }
        }
        #endregion
    }
}
