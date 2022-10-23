using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Days.Util.Infra
{
    public class Grid
    {
        public int x { get; set; }
        public int y { get; set; }

        public Grid(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public Grid(float x, float y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        public Vector3 GetVector()
        {
            return new Vector3(x, y, 0);
        }
    }
}