using System;
using System.Collections;
using GameState;
using UnityEngine;


public class Musician : MonoBehaviour
{
    private AudioSource[] _sources;

    private GameState.GameState _state;

    public float ChangeEveryLoop = 1;

    private int _startSector;
    private int _startLoop;
    private int _playingSource = 0;

    private GameObject _hero;

    private int _lastChangeSector;
    private int _lastChangeLoop;

    private void Start()
    {
        _sources = transform.GetComponentsInChildren<AudioSource>();
        _state = GameState.GameState.GetInstance();
        _state.OnLoopChange += StateOnOnLoopChange;
        _hero = GameObject.FindWithTag("Hero");
        _lastChangeSector = _startSector = SectorUtils.PositionToSectorIdx(_hero.transform.position);
        _lastChangeLoop = _startLoop = _state.GetLoopByIdx(_startSector);

        if (_startLoop <= 1)
        {
            _playingSource = 0; //intro
        }
        else
        {
            _playingSource = _startLoop;
        }

        _sources[_playingSource].Play();
    }

    private int _nextChangeSector = 0;
    private int _nextChangeLoop = 0;

    private void StateOnOnLoopChange(SectorChangeLoop obj)
    {
        _nextChangeSector = SectorUtils.PositionToSectorIdx(_hero.transform.position);
        _nextChangeLoop = _state.GetLoopByIdx(_nextChangeSector);

    }

    private bool _nextScheduled = false;

    private void Update()
    {
        var length = _sources[_playingSource].clip.length;
        var remainig = length - _sources[_playingSource].time;
        if (remainig > length/2)
        {
            _nextScheduled = false;
        }
        
        if (remainig > 0.5) return;

        if (_nextScheduled) return;
        if (_isChanging) return;
        _loopsDiff = (_nextChangeLoop + (float) _nextChangeSector / SectorUtils.SectorsInCircle) -
                     (_lastChangeLoop + (float) _lastChangeSector / SectorUtils.SectorsInCircle);
        if (Mathf.Abs(_loopsDiff) > ChangeEveryLoop)
        {
            var nextId = (_loopsDiff > 0) ? _playingSource + 1 : _playingSource - 1;
            if (nextId < 1 || nextId >= _sources.Length) return;
            StartCoroutine("DoFade", nextId);
        }
        else
        {
            //just continue playing. exception is intro
            if (_playingSource == 0)
            {
                _playingSource = 1;
                _sources[_playingSource].PlayScheduled(AudioSettings.dspTime + remainig);
            }
        }

        _nextScheduled = true;
    }

    private IEnumerator DoFade(int nextId)
    {
        _isChanging = true;
        var length = _sources[_playingSource].clip.length;
        var remainig = length - _sources[_playingSource].time;
        yield return new WaitForSeconds(remainig);
        
        Debug.Log("DoFade - fade out");
        var fadeOut = StartCoroutine(FadeOut(_sources[_playingSource], 4));
        Debug.Log("DoFade - fade in");
        var fadeIn = StartCoroutine(FadeIn(_sources[nextId], 4));
        Debug.Log("DoFade - wait fade in");
        yield return fadeIn;
        Debug.Log("DoFade - wait fade out");
        yield return fadeOut;
        Debug.Log("DoFade - doen");

        var loopNr = (_lastChangeLoop + (float) _lastChangeSector / SectorUtils.SectorsInCircle);
        var nextLoopNr = loopNr + Math.Sign(nextId - _playingSource)*ChangeEveryLoop;
        _lastChangeLoop = (int)nextLoopNr;
        _lastChangeSector = (int)((nextLoopNr % 1) * SectorUtils.SectorsInCircle);
        _playingSource = nextId;

        _isChanging = false;
    }

    private bool _isChanging;
    private float _loopsDiff;


    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.volume = 0;

        audioSource.Play();
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.volume = 1;
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}