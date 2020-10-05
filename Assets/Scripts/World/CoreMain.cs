using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMain : MonoBehaviour
{
    public CoreUI coreUi;
    public ShopUi shopUi;
    public ShopDoors doors;

    public int rerollShopOnLoop;
    
    private int _loopReached;

    void Start()
    {
        _loopReached = 0;
        
        ShowInfoUi();
        doors.LockEntrance();
    }

    private void Update()
    {
        var currentLoop = GameState.GameState.GetInstance().CurrentLoop;

        if (currentLoop > _loopReached)
        {
            _loopReached = currentLoop;

            if (_loopReached > 0 && _loopReached % rerollShopOnLoop == 0)
            {
                shopUi.RandomizeLots();
                doors.UnlockEntrance();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            ShowShopUi();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            ShowInfoUi();
        }
    }


    public void ShowShopUi()
    {
        // todo: animate
        coreUi.gameObject.SetActive(false);
        shopUi.gameObject.SetActive(true);
    }

    public void ShowInfoUi()
    {
        // todo: animate
        shopUi.gameObject.SetActive(false);
        coreUi.gameObject.SetActive(true);
    }
    
}
