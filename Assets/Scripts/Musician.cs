using System;
using System.Collections;
using GameState;
using UnityEngine;


public class Musician : MonoBehaviour
{
    private AudioSource[] _sources;

    private GameState.GameState _state;

    public float ChangeEveryLoop = 1;

    private int _lastSectorIdx;
    private int _nextSectorIdx;
    
    private int _playingSource = 0;

    private GameObject _hero;

    private void Start()
    {
        _sources = transform.GetComponentsInChildren<AudioSource>();
        _state = GameState.GameState.GetInstance();
        _state.OnLoopChange += StateOnOnLoopChange;
        _hero = GameObject.FindWithTag("Hero");
        _lastSectorIdx = _nextSectorIdx = 0;
        var startSector = SectorUtils.PositionToSectorIdx(_hero.transform.position);
        var startLoop = _state.GetLoopByIdx(startSector);

        if (startLoop <= 1)
        {
            _playingSource = 0; //intro
        }
        else
        {
            _playingSource = startLoop;
        }

        _sources[_playingSource].Play();
    }

    private void StateOnOnLoopChange(SectorChangeLoop obj)
    {
        if (obj.PrevLoop == null) return;
        var clockwise = obj.NewLoop > obj.PrevLoop.Value;
        if (clockwise)
        {
            _nextSectorIdx++;
        }
        else
        {
            _nextSectorIdx--;
        }
    }

    private bool _introSwitched = false;

    private void Update()
    {
        var length = _sources[_playingSource].clip.length;
        var remainig = length - _sources[_playingSource].time;

        if (remainig > 0.5) return;
        if (!_introSwitched)
        {
            //just continue playing. exception is intro
            if (_playingSource == 0)
            {
                _playingSource = 1;
                _sources[_playingSource].PlayScheduled(AudioSettings.dspTime + remainig);
            }

            _introSwitched = true;
        }

        if (_isChanging) return;
        _loopsDiff = _nextSectorIdx - _lastSectorIdx;
        if (Mathf.Abs(_loopsDiff) > ChangeEveryLoop*SectorUtils.SectorsInCircle)
        {
            var nextId = (_loopsDiff > 0) ? _playingSource + 1 : _playingSource - 1;
            if (nextId < 1 || nextId >= _sources.Length) return;
            StartCoroutine("DoFade", nextId);
        }

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

        _lastSectorIdx = _lastSectorIdx + (int) (ChangeEveryLoop * SectorUtils.SectorsInCircle);
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