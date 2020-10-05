using System;
using GameState;
using UnityEditor;
using UnityEngine;

namespace Loop
{
    public class SimpleGrowBehaviour : MonoBehaviour, ILoopable
    {
        public GameObject NextPrefab;
        public GameObject OnDeathPrefab;

        private Vector3 _initPosition;
        private void Awake()
        {
            _initPosition = transform.position;
        }

        public void ReplaceWithCorpse()
        {
            if (OnDeathPrefab == null) return;
            var corpse = Instantiate(OnDeathPrefab);
            corpse.transform.position = transform.position;
            if (!GameState.GameState.GetInstance().OnObjectDestroy(gameObject, corpse))
            {
                Destroy(corpse);
            }
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Handles.Label(transform.position, $"idx={SectorUtils.PositionToSectorIdx(_initPosition)} loop={GameState.GameState.GetInstance().GetLoopByIdx(SectorUtils.PositionToSectorIdx(_initPosition))}");
#endif
        }

        public object SaveState()
        {
            return null;
        }

        public void RestoreState(object state)
        {
            //ignore
        }

        public Vector3 GetPosition()
        {
            return _initPosition;
        }

        public void RestorePosition(Vector3 position)
        {
            transform.position = position;
            _initPosition = position;
        }

        public GameObject GetNextLoopPrefab()
        {
            return NextPrefab;
        }
    }
}