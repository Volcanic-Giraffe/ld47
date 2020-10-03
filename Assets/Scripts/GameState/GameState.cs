using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
    public class SectorChangeLoop
    {
        public int SectorIdx;
        public int NewLoop;
        public int? PrevLoop;

        public SectorChangeLoop(int sectorIdx, int newLoop, int? prevLoop)
        {
            SectorIdx = sectorIdx;
            NewLoop = newLoop;
            PrevLoop = prevLoop;
        }
    }
    
    [Serializable]
    public class GameState
    {
        private int[] sectorIdxToLoop = new int[SectorUtils.SectorsInCircle];

        public event Action<SectorChangeLoop> OnLoopChange;
     
        public List<LoopState> loops = new List<LoopState>();
        
        
        

        public int GetLoopByIdx(int idx)
        {
            return sectorIdxToLoop[idx % SectorUtils.SectorsInCircle];
        }

        public void Initialize(Vector3 heroPosition, int loop)
        {
            int idx = SectorUtils.PositionToSectorIdx(heroPosition);

            for (int i = idx; i < idx + SectorUtils.SectorsInCircle/2+1; i++)
            {
                sectorIdxToLoop[i % SectorUtils.SectorsInCircle] = loop;
                OnLoopChange?.Invoke(new SectorChangeLoop(i % SectorUtils.SectorsInCircle, loop, null));
            }

            for (int i = idx + SectorUtils.SectorsInCircle / 2+1; i < idx + SectorUtils.SectorsInCircle; i++)
            {
                sectorIdxToLoop[i % SectorUtils.SectorsInCircle] = loop - 1;
                OnLoopChange?.Invoke(new SectorChangeLoop(i % SectorUtils.SectorsInCircle, loop-1, null));
            }
            
        }

        public void OnHeroMove(Vector3 prevPosition, Vector3 nextPosition)
        {
            var idxFrom = SectorUtils.PositionToSectorIdx(prevPosition);
            var idxTo = SectorUtils.PositionToSectorIdx(nextPosition);

            if (idxFrom == idxTo) return;
            bool clockwise = !(idxFrom == 0 && idxTo == SectorUtils.SectorsInCircle - 1) &&
                             ((idxTo > idxFrom) || (idxTo == 0 && idxFrom == SectorUtils.SectorsInCircle - 1));

            //Debug.Log($"Move {idxFrom} -> {idxTo}. clckws = {clockwise}");
            if (clockwise)
            {
                var oppositeIdx = (idxTo + SectorUtils.SectorsInCircle / 2) % SectorUtils.SectorsInCircle;
                sectorIdxToLoop[oppositeIdx]++;
                OnLoopChange?.Invoke(new SectorChangeLoop(oppositeIdx, sectorIdxToLoop[oppositeIdx], sectorIdxToLoop[oppositeIdx]-1));
            }
            else
            {
                var oppositeIdx = (idxFrom + SectorUtils.SectorsInCircle / 2) % SectorUtils.SectorsInCircle;
                sectorIdxToLoop[oppositeIdx]--;
                OnLoopChange?.Invoke(new SectorChangeLoop(oppositeIdx, sectorIdxToLoop[oppositeIdx], sectorIdxToLoop[oppositeIdx]+1));
            }
        }

        private static GameState _instance;

        public static GameState GetInstance()
        {
            if (_instance == null) _instance = new GameState();
            return _instance;
        }

        private GameState()
        {
            OnLoopChange += OnOnLoopChange;
        }

        private void OnOnLoopChange(SectorChangeLoop change)
        {
            if (change.NewLoop >= loops.Count || change.PrevLoop >= loops.Count) return;
            var nextLoopState = loops[change.NewLoop];
            if (change.PrevLoop == null)
            {
                nextLoopState.Enter(change.SectorIdx);
                return;
            }
            var prevLoopState = loops[change.PrevLoop.Value];
            if (change.NewLoop > change.PrevLoop)
            {
                nextLoopState.EnterForward(change.SectorIdx, prevLoopState);
            }
            else
            {
                nextLoopState.EnterBackward(change.SectorIdx, prevLoopState);
            }
        }
    }
}