using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{
    [SerializeField] private GameObject _tile;

    [SerializeField] private GameObject[] _emenies;

    [SerializeField] private Animator _anim;
    
    private int _cycle;
    private int _gx;
    private int _gz;
    private float _noise;
    private float _tiles;
    private float _radius;

    private bool _hidden;
    private static readonly int Show = Animator.StringToHash("Show");
    private static readonly int Hide = Animator.StringToHash("Hide");


    // Start is called before the first frame update
    void Start()
    {
        _hidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnVaporVisited()
    {
        float sample = Mathf.PerlinNoise((_gx + _radius * _cycle) / _tiles * _noise, (_gz + _radius * _cycle) / _tiles * _noise);

        if (sample > 0.5 || sample < 0.2)
        {
            DoHide();
        }
        else
        {
            DoShow();

            if (Random.value < 0.001f)
            {
                var enemyToSpawn = _emenies[Random.Range(0, _emenies.Length)];

                // Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }
        }
        

        _cycle += 1;
        if (_cycle >= 4) _cycle = 0;
    }

    public void DisableCell()
    {
        DoHide();
    }

    private void DoHide()
    {
        if (_hidden) return;
        _hidden = true;
        
        _anim.ResetTrigger(Show);
        _anim.SetTrigger(Hide);
    }

    private void DoShow()
    {
        if (!_hidden) return;
        _hidden = false;
        
        _anim.ResetTrigger(Hide);
        _anim.SetTrigger(Show);
    }

    public void SetLocation(int x, int z, float amount, float noiseResolution, float radius)
    {
        _gx = x;
        _gz = z;

        _tiles = amount;
        _noise = noiseResolution;
        _radius = radius;
    }
}
