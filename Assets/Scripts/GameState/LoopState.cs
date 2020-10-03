using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Loop;
using UnityEngine;

namespace GameState
{
    [Serializable]
    public class LoopState
    {
        public static int NextId = 1;
        
        public List<PrefabInLoop> Prefabs = new List<PrefabInLoop>();
        
        public List<ObjectInLoop> Objects = new List<ObjectInLoop>();

        public void Enter(int sectorIdx)
        {
            CreateObjects(sectorIdx);    
        }
        
        public void EnterForward(int sectorIdx, LoopState previousLoopState)
        {
            foreach (var obj in previousLoopState.Objects.ToList())
            {
                var loopable = obj.CurrentObject.GetComponent<ILoopable>();
                var position = (loopable == null ? obj.CurrentObject.transform.position : loopable.GetPosition());
                if (!SectorUtils.MatchSector(position, sectorIdx)) continue;
                Debug.Log($"Remove object {obj.CurrentObject} #{obj.ID} from previous loop");
                if (loopable != null)
                {
                    var nextPrefab = loopable.GetNextLoopPrefab();
                    if (nextPrefab != null)
                    {
                        //put it here
                        var newPrefab = new PrefabInLoop()
                        {
                            ID = obj.ID,
                            IsInitialOne = false,
                            Prefab = nextPrefab,
                            State = loopable.SaveState(),
                            Position = loopable.GetPosition(),
                            Rotation = obj.CurrentObject.transform.localRotation,
                            SectorIdx = SectorUtils.PositionToSectorIdx(loopable.GetPosition())
                        };
                        Prefabs.Add(newPrefab);
                        Debug.Log($"Add prefab {newPrefab.Prefab} #{obj.ID} instead (grow)");
                    }

                }
                GameObject.Destroy(obj.CurrentObject);
                previousLoopState.Objects.Remove(obj);
            }

            CreateObjects(sectorIdx);
        }

        private void CreateObjects(int sectorIdx)
        {
            // prefabs initialized. now initialize objects
            foreach (var prefab in Prefabs.ToList())
            {
                if (!SectorUtils.MatchSector(prefab.Position, sectorIdx)) continue;
                
                var go = GameObject.Instantiate(prefab.Prefab);
                go.transform.position = prefab.Position;
                go.transform.localRotation = prefab.Rotation;
                var loopable = go.GetComponent<ILoopable>();
                if (loopable != null)
                {
                    loopable.RestoreState(prefab.State);
                    loopable.RestorePosition(prefab.Position);
                }

                var suffix = prefab.IsInitialOne ? "initial" : "grown";
                Objects.Add(new ObjectInLoop()
                {
                    CurrentObject = go,
                    ID = prefab.ID
                });
                Debug.Log($"Create object {prefab.Prefab} #{prefab.ID} ({ suffix })");
            }
        }

        public void EnterBackward(int sectorIdx, LoopState nextLoopState)
        {
            nextLoopState.Prefabs.RemoveAll(p => SectorUtils.MatchSector(p.Position, sectorIdx) && !p.IsInitialOne);
            foreach (var obj in nextLoopState.Objects.ToList())
            {
                var loopable = obj.CurrentObject.GetComponent<ILoopable>();
                var position = (loopable == null ? obj.CurrentObject.transform.position : loopable.GetPosition());
                if (!SectorUtils.MatchSector(position, sectorIdx)) continue;

                Debug.Log($"Remove object {obj.CurrentObject} #{obj.ID} from previous loop");
                GameObject.Destroy(obj.CurrentObject);
                nextLoopState.Objects.Remove(obj);
            }
            CreateObjects(sectorIdx);
        }
        
    }


    [Serializable]
    public class PrefabInLoop
    {
        public bool IsInitialOne; //if not - could be deleted when go backward in time
        public int ID;
        public GameObject Prefab; // used to create object when first enter into loop

        public Vector3 Position; // where to create object
        public Quaternion Rotation; // how to rotate it
        public int SectorIdx; //for debug

        [CanBeNull] public object State;
    }


    [Serializable]
    public class ObjectInLoop
    {
        public int ID;
        public GameObject CurrentObject;
        
    }
    
}