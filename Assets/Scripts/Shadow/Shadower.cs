using System;
using UnityEngine;

namespace Shadow
{
    public class Shadower : MonoBehaviour
    {
        private GameObject _hero;
        public Material ShadowerMaterial;

        private void Awake()
        {
            _hero = GameObject.FindWithTag("Hero");
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
        }
    }
}