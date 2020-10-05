using System;
using UnityEditor;
using UnityEngine;

namespace GameState
{
    public class GameStateBehaviour : MonoBehaviour
    {
        private GameObject _hero;
        private Damageable _heroHealth;
        private GameState _state;
        private Vector3 _prevHeroPosition;

        public bool Paused;

        private void Awake()
        {
            // might be easier but I'm lazy
            // do not remove hero tag from hero body bcs resource check it on collide!
            var hs = GameObject.FindGameObjectsWithTag("Hero");
            foreach (var he in hs)
            {
                if (he.GetComponent<Damageable>() != null)
                {
                    _hero = he;
                    break;
                }
            }
            _heroHealth = _hero.GetComponent<Damageable>();
            _state = GameState.GetInstance();
            _prevHeroPosition = _hero.transform.position;
        }

        private bool _oldPaused = false;
        private void Update()
        {
            if (_hero != null)
            {
                var newPisition = _hero.transform.position;
                
                if (Paused != _oldPaused)
                {
                    _oldPaused = Paused;
                    if (Paused)
                    {
                        _state.Pause(newPisition);
                    }
                    else
                    {
                        _state.Unpause(newPisition);
                    }
                }
                else
                {
                    _state.OnHeroMove(_prevHeroPosition, newPisition);
                }

                _prevHeroPosition = newPisition;
            }
            _state.HeroHealth = _heroHealth != null ? _heroHealth.Health : 0;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (_state == null || _hero == null) return;

            Handles.Label(_hero.transform.position, "" + SectorUtils.PositionToSectorIdx(_hero.transform.position));

            var colors = new Color[]
            {
                Color.green, Color.blue, Color.red, Color.magenta
            };
            var sectorAngle = 4;

            for (int i = 0; i < 360; i += sectorAngle)
            {
                var point = new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * i),
                    0,
                    Mathf.Sin(Mathf.Deg2Rad * i)
                ) * 4f;
                var loop = _state.GetLoopByIdx(SectorUtils.PositionToSectorIdx(point));
                var color = colors[(loop + colors.Length) % colors.Length];
                Handles.color = new Color(color.r, color.g, color.b, 0.1f);
                Handles.DrawSolidArc(Vector3.zero, Vector3.up, point, sectorAngle, 4f);
                Handles.color = new Color(0, 0, 0, 0.5f);

                Handles.Label(point, "Idx " + SectorUtils.PositionToSectorIdx(point) + " Loop " + loop);// + " ang = " + Mathf.Rad2Deg *Mathf.Atan2(-point.y, point.x) + "/" + i);
            }
#endif
        }
    }
}