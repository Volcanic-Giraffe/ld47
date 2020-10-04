using System;
using UnityEngine;
using GameState;
using Random = UnityEngine.Random;

public class LoopsGenerator : MonoBehaviour
{
    public ItemPrefab[] Prefabs;

    public int LevelsToGenerate = 20;
    
    private void Awake()
    {
        if (GetComponent<LoopsInitializer>() == null)
        {
            // Если есть LoopInitializer, то пусть он разбирается
            Generate();
        }
    }

    public void Generate()
    {
        var state = GameState.GameState.GetInstance();
        for (int i = 0; i < LevelsToGenerate; i++)
        {
            GenerateLevel(state);
        }    
    }

    private void GenerateLevel(GameState.GameState state)
    {
        var realLevelNr = state.loops.Count;

        var loopState = new LoopState();
        for (int room = 0; room < 4; room++)
        {
            var roomVariantNr = Random.Range(0, RoomsConfig.RoomVariants.GetLength(0));
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    int item = RoomsConfig.RoomVariants[roomVariantNr, y, x];
                    if (item >= Prefabs.Length) continue;
                    var prefabInfo = Prefabs[item];
                    if (prefabInfo.Prefab == null) continue;
                    if (prefabInfo.Probability_100_Percent == false && Random.value > (0.02 * realLevelNr)) continue;
                    
                    // 0-15, 0-15
                    int realTileX=0, realTileY=0;
                    /*
                     *   3330
                     *   2--0
                     *   2--0
                     *   2111
                     */
                    switch (room)
                    {
                        case 0:
                            realTileX = 15 - y;
                            realTileY = x;
                            break;
                        case 1:
                            realTileX = 15 - x;
                            realTileY = 15 - y;
                            break;
                        case 2:
                            realTileX = y;
                            realTileY = 15 - x;
                            break;
                        case 3:
                            realTileX = x;
                            realTileY = y;
                            break;
                    }

                    var position = new Vector3(
                        ((float)realTileX - 7.5f),
                        0,
                        -((float)realTileY - 7.5f));
                    var prefabInLoop = new PrefabInLoop()
                    {
                        ID = LoopState.NextId++,
                        Prefab = prefabInfo.Prefab,
                        IsInitialOne = true,
                        Position = position,
                        SectorIdx = SectorUtils.PositionToSectorIdx(position),
                        Rotation = Quaternion.identity,
                        State = null
                    };
                    loopState.Prefabs.Add(prefabInLoop);
                }
            }
        }
        
        state.loops.Add(loopState);
        
    }
}

[Serializable]
public class ItemPrefab
{
    public GameObject Prefab;

    public bool Probability_100_Percent;
}