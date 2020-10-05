using System.Collections;
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
        StopAllCoroutines();
        StartCoroutine(ShowShopCr());
    }
    private IEnumerator ShowShopCr()
    {
        yield return MoveDown(coreUi.gameObject, 1f);
        coreUi.gameObject.SetActive(false);
        shopUi.gameObject.SetActive(true);
        yield return MoveUp(shopUi.gameObject, 2f);
    }

    public void ShowInfoUi()
    {
        StopAllCoroutines();
        StartCoroutine(ShowInfoCr());
    }
    private IEnumerator ShowInfoCr()
    {
        yield return MoveDown(shopUi.gameObject, 1f);
        coreUi.gameObject.SetActive(true);
        shopUi.gameObject.SetActive(false);
        yield return MoveUp(coreUi.gameObject, 2f);
    }

    private IEnumerator MoveDown(GameObject obj, float desiredY)
    {
        while (obj.transform.position.y >= desiredY)
        {
            obj.transform.position += Vector3.down * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoveUp(GameObject obj, float desiredY)
    {
        while (obj.transform.position.y <= desiredY)
        {
            obj.transform.position += Vector3.up * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
    }

}
