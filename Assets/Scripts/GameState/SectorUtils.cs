using UnityEngine;
using UnityEngine.Assertions;

namespace GameState
{
    public class SectorUtils
    {
        public static int SectorsInCircle = 90;

        private static int AngleToIdxDegrees(float degrees)
        {
            while (degrees < 0) degrees += 360;
            return (int)(degrees * SectorsInCircle / 360) % SectorsInCircle;
        }

        private static int AngleToIdx(float radians)
        {
            return AngleToIdxDegrees((Mathf.Rad2Deg * radians));
        }

        public static int PositionToSectorIdx(Vector3 position)
        {
            return AngleToIdx(Mathf.Atan2(-position.z, position.x));
        }

        public static bool MatchSector(Vector3 position, int sectorIdx)
        {
            //Assert.AreEqual(position.y, 0);
            return PositionToSectorIdx(position) == sectorIdx;
        }
        
    }
}