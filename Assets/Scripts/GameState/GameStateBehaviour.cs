﻿using System;
using UnityEditor;
using UnityEngine;

namespace GameState
{
    public class GameStateBehaviour : MonoBehaviour
    {
        private GameObject _hero;
        private GameState _state;
        private Vector3 _prevHeroPosition;

        private void Awake()
        {
            _hero = GameObject.FindGameObjectWithTag("Hero");
            _state = GameState.GetInstance();
            _prevHeroPosition = _hero.transform.position;
            _state.Initialize(_prevHeroPosition, 1);
        }

        private void Update()
        {
            var newPisition = _hero.transform.position;
            _state.OnHeroMove(_prevHeroPosition, newPisition);
            _prevHeroPosition = newPisition;
        }

        private void OnDrawGizmos()
        {
            if (_state == null || _hero == null) return;
            
            Handles.Label(_hero.transform.position, "" + SectorUtils.PositionToSectorIdx(_hero.transform.position));
            
            var colors = new Color[]
            {
                Color.green, Color.blue, Color.red, Color.magenta
            };
            var sectorAngle = 30;
            
            for (int i = 0; i < 360; i += sectorAngle)
            {
                var point = new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * i),
                    Mathf.Sin(Mathf.Deg2Rad * i),
                    0
                    ) * 6f;
                var loop = _state.GetLoopByIdx(SectorUtils.PositionToSectorIdx(point));
                var color = colors[loop % colors.Length];
                Handles.color = new Color(color.r, color.g, color.b, 0.1f);
                Handles.DrawSolidArc(Vector3.zero, Vector3.forward,  point, sectorAngle, 6f);
                Handles.color = new Color(0, 0, 0, 0.5f);

                Handles.Label(point, "Idx " + SectorUtils.PositionToSectorIdx(point) + " Loop " + loop);// + " ang = " + Mathf.Rad2Deg *Mathf.Atan2(-point.y, point.x) + "/" + i);
            }
        }
    }
}