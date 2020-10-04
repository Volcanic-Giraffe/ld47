using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PpVolumeController : MonoBehaviour
{

    public Volume ppVolume;
    private ChromaticAberration _chromatic;
    private Vignette _vignette;

    public AnimationCurve onHitCurve;
    
    private float _onHitLerp;

    private void Start()
    {
        if (ppVolume != null)
        {
            ppVolume.profile.TryGet<ChromaticAberration>(out _chromatic);
            ppVolume.profile.TryGet<Vignette>(out _vignette);
        }

        _onHitLerp = 1f;
    }

    private void Update()
    {
        if (_onHitLerp < 1f && _chromatic != null)
        {
            _onHitLerp += Time.deltaTime;
            float value = onHitCurve.Evaluate(_onHitLerp);
            
            _chromatic.intensity.value = Mathf.Clamp(value, 0f, 1f);
        }
    }

    public void AnimateOnHit()
    {
        _onHitLerp = 0;
    }
}
