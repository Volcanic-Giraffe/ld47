using System;
using UnityEditor;
using UnityEngine;

namespace GameState
{
    public class LoopsInitializer : MonoBehaviour
    {
        
        private void Awake()
        {
            var state = GameState.GetInstance();

            foreach (Transform loop in transform)
            {
                AddLoop(loop.gameObject, state);
            }
        }

        private void AddLoop(GameObject loop, GameState state)
        {
            var loopState = new LoopState();
            state.loops.Add(loopState);

            foreach (Transform instantiatedPrefab in loop.transform)
            {
                if (instantiatedPrefab.gameObject.name == "Dungeon") continue;
                var prefabInLoop = new PrefabInLoop()
                {
                    ID = LoopState.NextId++,
                    IsInitialOne = true,
                    Position = instantiatedPrefab.transform.localPosition,
                    Prefab = instantiatedPrefab.gameObject,
                    State = null
                };
                loopState.Prefabs.Add(prefabInLoop);
            }
            
            loop.SetActive(false);
        }
    }
}