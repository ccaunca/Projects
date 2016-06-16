using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Visitor : PCSNode
    {
        public virtual void VisitCrab(Crab pCrab)
        {
            Debug.WriteLine("Visit by Crab not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitSquid(Squid pSquid)
        {
            Debug.WriteLine("Visit by Squid not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitBomb(Bomb pBomb)
        {
            Debug.WriteLine("Visit by Bomb not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitBombRoot(BombRoot pBombRoot)
        {
            Debug.WriteLine("Visit by BombRoot not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitMissile(Missile pMissile)
        {
            Debug.WriteLine("Visit by Missile not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitMissileRoot(MissileRoot pMissileRoot)
        {
            Debug.WriteLine("Visit by MissileRoot not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitOctopus(Octopus pOctopus)
        {
            Debug.WriteLine("Visit by Octopus not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitShip(Ship pShip)
        {
            Debug.WriteLine("Visit by Ship not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitShipRoot(ShipRoot pShipRoot)
        {
            Debug.WriteLine("Visit by ShipRoot not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWall(Wall pWall)
        {
            Debug.WriteLine("Visit by Wall not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWall(Grid pGrid)
        {
            Debug.WriteLine("Visit by Grid not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWall(Ship pShip)
        {
            Debug.WriteLine("Visit by Ship not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWallRoot(WallRoot pWallRoot)
        {
            Debug.WriteLine("Visit by WallRoot not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWallRight(WallRight pWallRight)
        {
            Debug.WriteLine("Visit by WallRight not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWallLeft(WallLeft pWallLeft)
        {
            Debug.WriteLine("Visit by WallRight not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWallTop(WallTop pWallTop)
        {
            Debug.WriteLine("Visit by WallTop not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitWallBottom(WallBottom pWallBottom)
        {
            Debug.WriteLine("Visit by WallBottom not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitShieldRoot(ShieldRoot pShieldRoot)
        {
            Debug.WriteLine("Visit by ShieldRoot not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitShieldColumn(ShieldColumn pShieldColumn)
        {
            Debug.WriteLine("Visit by ShieldColumn not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitShieldBrick(ShieldBrick pShieldBrick)
        {
            Debug.WriteLine("Visit by ShieldBrick not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitShieldGrid(ShieldGrid pShieldGrid)
        {
            Debug.WriteLine("Visit by ShieldGrid not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitColumn(Column pColumn)
        {
            Debug.WriteLine("Visit by Column not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitGrid(Grid pGrid)
        {
            Debug.WriteLine("Visit by Grid not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitUFO(UFO pUFO)
        {
            Debug.WriteLine("Visit by UFO not implemented.");
            Debug.Assert(false);
        }
        public virtual void VisitUFORoot(UFORoot pUFORoot)
        {
            Debug.WriteLine("Visit by UFORoot not implemented.");
            Debug.Assert(false);
        }
        abstract public void Accept(Visitor other);
    }
}
