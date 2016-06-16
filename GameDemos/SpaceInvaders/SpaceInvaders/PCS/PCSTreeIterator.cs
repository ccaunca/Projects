using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class PCSTreeIterator
    {
        private static GameObject root;
        private static GameObject current;
        private static GameObject wrongParent;

        public static void CalculateIterators(GameObject pRootNode)
        {
            Debug.Assert(pRootNode != null);
            PCSTreeIterator.root = pRootNode;
            PCSTreeIterator.wrongParent = (GameObject)pRootNode.pParent;
            PCSTreeIterator.current = PCSTreeIterator.root;
            GameObject pPrevGameObj = (GameObject)pRootNode;
            GameObject pGameObj = (GameObject)pRootNode;
            while (pGameObj != null)
            {
                pPrevGameObj = pGameObj;
                pGameObj = PCSTreeIterator.privSecretNext();
                pPrevGameObj.pForward = pGameObj;
                if (pGameObj != null)
                {
                    pGameObj.pReverse = pPrevGameObj;
                }
            }
            pRootNode.pReverse = pPrevGameObj;
        }

        private static GameObject privSecretNext()
        {
            PCSTreeIterator.current = privGetNext(PCSTreeIterator.current);
            return (GameObject)PCSTreeIterator.current;
        }

        private static GameObject privGetNext(GameObject node, bool UseChild = true)
        {
            GameObject tmp = null;
            Debug.Assert(node != null);
            if ((node.pChild != null) && UseChild)
            {
                tmp = (GameObject)node.pChild;
            }
            else if (node.pSibling != null)
            {
                tmp = (GameObject)node.pSibling;
            }
            else if (node.pParent != PCSTreeIterator.root && node.pParent != PCSTreeIterator.wrongParent)
            {
                tmp = PCSTreeIterator.privGetNext((GameObject)node.pParent, false);
            }
            else
            {
                tmp = null;
            }
            return tmp;
        }
    }
}
