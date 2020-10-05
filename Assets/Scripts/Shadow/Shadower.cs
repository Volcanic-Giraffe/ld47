using System;
using GameState;
using UnityEngine;

namespace Shadow
{
    public class Shadower : MonoBehaviour
    {
        private GameObject _hero;
        public Material ShadowerMaterial;
        private GameState.GameState _state;

        public bool DecreaseBeam = true;

        public float DefaultBeamSize = 0.1f;

        public float MaxBeamSize = 20f;

        private void Awake()
        {
            _hero = GameObject.FindWithTag("Hero");
            _state = GameState.GameState.GetInstance();
            
            _state.OnLoopChange += StateOnOnLoopChange;
            ShadowerMaterial.SetFloat("BeamSize", DefaultBeamSize);
        }

        private int _loopToMaximizeBeam = 0;
        private int _sectorToStartMaximize = 45;
        private int _sectorToStopMaximize = 65;

        private void StateOnOnLoopChange(SectorChangeLoop obj)
        {
            if (obj.PrevLoop != null 
                && ((obj.PrevLoop == _loopToMaximizeBeam && obj.NewLoop == _loopToMaximizeBeam+1)  
                    || (obj.PrevLoop == _loopToMaximizeBeam+1 && obj.NewLoop == _loopToMaximizeBeam))
                    
                && obj.SectorIdx >= _sectorToStartMaximize 
                && obj.SectorIdx <= _sectorToStopMaximize)
            {
                var size = Mathf.Pow(1 - 2 * Mathf.Abs(((float)obj.SectorIdx - _sectorToStartMaximize) / ((float)_sectorToStopMaximize - _sectorToStartMaximize) - 0.5f), 2f);
                Debug.Log($"CHANGE BEAM {size}");
                ShadowerMaterial.SetFloat("BeamSize", Mathf.Lerp(DefaultBeamSize, MaxBeamSize, size));
            }
            else
            {
                ShadowerMaterial.SetFloat("BeamSize", DefaultBeamSize);
            }
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