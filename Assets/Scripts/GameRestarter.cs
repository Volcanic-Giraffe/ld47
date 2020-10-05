using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    public UiOverlay overlay;
    
    private bool _calledOnce;
    private float _timeToRestart;
    
    void Start()
    {
        if(overlay != null) overlay.FadeIn(0.1f);
    }
    
    private void Update()
    {
        if (_timeToRestart > 0)
        {
            _timeToRestart -= Time.deltaTime;
            if (_timeToRestart <= 0)
            {
                GameState.GameState.GetInstance().ResetGameState();
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
    
    public void RestartWholeGame()
    {
        if (_calledOnce) return;
        _calledOnce = true;
        
        if(overlay != null) overlay.FadeOut();
        _timeToRestart = 1f;
    }
}
