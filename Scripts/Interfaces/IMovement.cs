using UnityEngine;

namespace Interfaces
{
    public interface IMovement
    {
        static Collider2D Boundary;
        
        static float MinX;
        static float MaxX;
        static float MinY;
        static float MaxY;
        
        void ProcessBassBoundary();
    }
}