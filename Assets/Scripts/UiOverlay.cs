using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiOverlay : MonoBehaviour
{
    public Image image;
    public float fadeTime = 0.3f;
    
    private float fadeInTimer = 0;
    private float fadeOutTimer = 0;
    private float preFadeTimer = 0f;

    private void Start()
    {
        SetAlpha(1);
    }

    void Update()
    {
        if (preFadeTimer > 0)
        {
            preFadeTimer -= Time.deltaTime;
            return;
        }

        if (fadeInTimer > 0) {
            fadeInTimer -= Time.deltaTime;

            if (fadeInTimer <= 0) fadeInTimer = 0;

            SetAlpha(fadeInTimer / fadeTime);
        }

        if (fadeOutTimer > 0)
        {
            fadeOutTimer -= Time.deltaTime;

            if (fadeOutTimer <= 0) fadeOutTimer = 0;

            SetAlpha(1 - (fadeOutTimer / fadeTime));
        }
    }

    public void FadeIn (float preFade)
    {
        fadeInTimer = fadeTime;

        preFadeTimer = preFade;
    }

    public void FadeOut()
    {
        fadeOutTimer = fadeTime;
    }

    public bool FadeDone()
    {
        return fadeInTimer == 0 && fadeOutTimer == 0;
    }

    void SetAlpha(float alpha)
    {
        var c = image.color;
        c.a = alpha;
        image.color = c;
    }
}