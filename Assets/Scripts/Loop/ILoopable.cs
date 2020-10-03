using System;
using UnityEngine;

namespace Loop
{
    public interface ILoopable
    {
        object SaveState();
        
        void RestoreState(object state);

        Vector3 GetPosition();

        void RestorePosition(Vector3 position);


        GameObject GetNextLoopPrefab();

    }
}