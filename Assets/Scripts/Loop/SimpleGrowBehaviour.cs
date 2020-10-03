using System;
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
            GameState.GameState.GetInstance().ReplaceObject(gameObject, corpse);
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