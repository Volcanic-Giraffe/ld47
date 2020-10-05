using System;
using UnityEngine;

public class CoreUI : MonoBehaviour
{
    public GameObject Health;
    public TextMesh LoopsText;
    public TextMesh ResText;
    int health = 0;

    private float _logoVisibilityTime = 2.0f;

    private static bool _logoShown;

    private SpriteRenderer _logo;
    private bool _anyKeyDown = false;

    private void Awake()
    {
        _logo = transform.Find("GameName").gameObject.GetComponent<SpriteRenderer>();
        if (_logoShown)
        {
            _logo.gameObject.SetActive(false);
            _logoVisibilityTime = 0.0f;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            _anyKeyDown = true;
        }
        if (_logoVisibilityTime > 0 && _anyKeyDown)
        {
            _logoVisibilityTime -= Time.deltaTime;
            _logoVisibilityTime = Mathf.Max(0, _logoVisibilityTime);
            _logo.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, _logoVisibilityTime/2));
            if (_logoVisibilityTime <= 0)
            {
                _logoShown = true;
            }
            
        }
        var actualHealth = (int)Mathf.Round(GameState.GameState.GetInstance().HeroHealth / 5);
        if (health != actualHealth)
        {
            health = actualHealth;
            for (int i = 0; i < Health.transform.childCount; i++)
            {
                if (i < Health.transform.childCount)
                {
                    Health.transform.GetChild(i).gameObject.SetActive(i < actualHealth);
                }
            }

        }

        if (LoopsText.gameObject.activeSelf) LoopsText.text = GameState.GameState.GetInstance().CurrentLoop.ToString();
        if (ResText.gameObject.activeSelf) ResText.text = GameState.GameState.GetInstance().Resources.ToString();
    }

    public void HideTextLabels()
    {
        LoopsText.gameObject.SetActive(false);
        ResText.gameObject.SetActive(false);
    }

    public void ShowTextLabels()
    {
        LoopsText.gameObject.SetActive(true);
        ResText.gameObject.SetActive(true);
    }
}
