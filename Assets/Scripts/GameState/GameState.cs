using System;
using System.Collections.Generic;
using Loop;
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
            if (change.NewLoop >= loops.Count || (change.PrevLoop != null && change.PrevLoop >= loops.Count)) return;
           // Debug.Log("!!!!");
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

        // Вызывается когда объект уничтожен. Если его надо заменить трупом - параметр corpse. 
        // Возвращает true если заменил на труп
        public bool OnObjectDestroy(GameObject gameObject, GameObject corpse = null)
        {
            var loopable = gameObject.GetComponent<ILoopable>();

            for (int loopId = 0; loopId < loops.Count; loopId++)
            {
                var loop = loops[loopId];
                var objectInLoop = loop.Objects.Find(o => o.CurrentObject == gameObject);
                if (objectInLoop == null) continue;

                if (corpse != null)
                {
                    // если труп надо положить в другой сектор, то этот сектор должен быть в том же лупе, что и оригинальный юнит
                    // иначе будут глитчи стопудово (будет робот в одном секторе и труп от него в другом одновременно)
                    // если этот сектор в другом лупе, то я труп вообще не положу
                    var actualCorpseLoopId = GetLoopByIdx(SectorUtils.PositionToSectorIdx(corpse.transform.position));

                    if (actualCorpseLoopId == loopId)
                    {
                        objectInLoop.CurrentObject = corpse;
                        (corpse.GetComponent<ILoopable>())?.RestorePosition(corpse.transform.position);
                        Debug.Log($"Replace {gameObject} #{objectInLoop.ID} with {corpse} at loop {loopId}");

                        return true;
                    }
                }
                else
                {
                    loop.Objects.Remove(objectInLoop);
                }
            }

            return false;
        }
        
    }
}