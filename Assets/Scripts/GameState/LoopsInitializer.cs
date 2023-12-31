﻿using System;
using UnityEditor;
using UnityEngine;

namespace GameState
{
    public class LoopsInitializer : MonoBehaviour
    {
        public int StartFrom = 1;
        public GameObject StartSectorPoint;
        
        private void Awake()
        {
            var state = GameState.GetInstance();

            foreach (Transform loop in transform)
            {
                if(loop.gameObject.activeInHierarchy)
                    AddLoop(loop.gameObject, state);
            }
            
            GetComponent<LoopsGenerator>()?.Generate();

            var pos = StartSectorPoint ? StartSectorPoint.transform.position : GameObject.FindGameObjectWithTag("Hero").transform.position;
            state.Initialize(pos, StartFrom);
        }

        private void AddLoop(GameObject loop, GameState state)
        {
            var loopState = new LoopState();
            state.loops.Add(loopState);

            foreach (Transform instantiatedPrefab in loop.transform)
            {
                if (instantiatedPrefab.gameObject.name == "Dungeon" || instantiatedPrefab.gameObject.name == "Ground") continue;
                var localPosition = instantiatedPrefab.transform.localPosition;
                var correctPosition = new Vector3(
                    localPosition.x,
                    0,
                    localPosition.y
                    );
                var prefabInLoop = new PrefabInLoop()
                {
                    ID = LoopState.NextId++,
                    IsInitialOne = true,
                    Position = correctPosition,
                    Rotation = instantiatedPrefab.transform.localRotation,
                    Prefab = instantiatedPrefab.gameObject,
                    State = null,
                    SectorIdx = SectorUtils.PositionToSectorIdx(correctPosition)
                };
                loopState.Prefabs.Add(prefabInLoop);
            }
            
            loop.SetActive(false);
        }
    }
}