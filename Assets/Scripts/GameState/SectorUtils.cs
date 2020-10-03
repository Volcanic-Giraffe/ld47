using UnityEngine;

namespace GameState
{
    public class SectorUtils
    {
        public static int SectorsInCircle = 8;
        
        

        private static int AngleToIdx(int degrees)
        {
            while (degrees < 0) degrees += 360;
            return (degrees * SectorsInCircle / 360) % SectorsInCircle;
        }

        private static int AngleToIdx(float radians)
        {
            return AngleToIdx((int) (Mathf.Rad2Deg * radians));
        }

        public static int PositionToSectorIdx(Vector3 position)
        {
            return AngleToIdx(Mathf.Atan2(-position.y, position.x));
        }

        public static bool MatchSector(Vector3 position, int sectorIdx)
        {
            return PositionToSectorIdx(position) == sectorIdx;
        }
        
    }
}