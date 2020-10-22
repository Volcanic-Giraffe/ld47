using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tileGo;
    private float amount = 100f;
    private float step = 0.5f;

    private float noiseResolution = 10f;

    private float circle = 15f;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < amount; x++)
        {
            for (int z = 0; z < amount; z++)
            {
                if (Vector2.Distance(new Vector2(x, z), new Vector2(amount * step, amount * step)) > circle * 2) continue;
                
                
                float sample = Mathf.PerlinNoise(x / amount * noiseResolution, z / amount * noiseResolution);

                var newTile = Instantiate(tileGo, new Vector3(x * step, 0, z * step), Quaternion.identity, transform);
                
                var cell = newTile.GetComponent<TileCell>();
                cell.SetLocation(x, z,  amount, noiseResolution, circle);
                
                if (sample > 0.5 || sample < 0.2)
                {
                    cell.DisableCell();
                }

            }
        }

        transform.position = new Vector3(-amount / 2f * step, 0, -amount / 2f * step);
    }
}