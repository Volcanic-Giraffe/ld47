using System;
using UnityEngine;

namespace Shadow
{
    public class Shadower : MonoBehaviour
    {
        private GameObject _hero;
        public Material ShadowerMaterial;
        private GameState.GameState _state;

        public bool DecreaseBeam = true;

        private void Awake()
        {
            _hero = GameObject.FindWithTag("Hero");
            _state = GameState.GameState.GetInstance();
        }

        private void Update()
        {
            //todo: do it better...


            if (_hero != null)
            {
                var pos = _hero.transform.position;
                var u = (pos.x / transform.localScale.x) + 0.5f;
                var v = (pos.z / transform.localScale.y) + 0.5f;

                ShadowerMaterial.SetVector("HeroUV", new Vector4(u, v));
            }

            var BeamVisibility = 1.0f;
            const float BEAM_FADE = 2.0f;
            if ((Time.time - _state.PausedAtTime) < BEAM_FADE)
            {
                BeamVisibility = Mathf.Lerp(1, 0, (Time.time - _state.PausedAtTime) / BEAM_FADE);
                Debug.Log("PS " + BeamVisibility);
            }
            else if ((Time.time - _state.UnpausedAtTime) < BEAM_FADE)
            {
                BeamVisibility = Mathf.Lerp(0, 1, (Time.time - _state.UnpausedAtTime) / BEAM_FADE);
                Debug.Log("UPS " + BeamVisibility);
            }
            else if (_state.PausedAtTime > 0)
            {
                BeamVisibility = 0;
            }
            ShadowerMaterial.SetFloat("BeamVisibility", BeamVisibility);
            ShadowerMaterial.SetFloat("BeamVisibilityDecrease", DecreaseBeam ? 1.0f : 0.0f);
        }
    }
}