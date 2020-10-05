using UnityEngine;

public class CoreUI : MonoBehaviour
{
    public GameObject Health;
    public TextMesh LoopsText;
    public TextMesh ResText;
    int health = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        LoopsText.text = GameState.GameState.GetInstance().CurrentLoop.ToString();
        ResText.text = GameState.GameState.GetInstance().Resources.ToString();
    }
}
