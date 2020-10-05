using System.Collections;
using GameState;
using UnityEngine;

public class CoreMain : MonoBehaviour
{
    public CoreUI coreUi;
    public ShopUi shopUi;
    public GameObject WinUi;
    public ShopDoors doors;

    public GameStateBehaviour GameStateBeh;

    public int rerollShopOnLoop;

    public GameObject bossPrefab;
    public int bossLoop;

    private int _loopReached;
    private bool bossSpawned;

    private float _originalY;
    
    void Start()
    {
        _loopReached = 0;

        ShowInfoUi();
        doors.LockEntrance();

        _originalY = transform.position.y;
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

            if (_loopReached >= bossLoop)
            {
                SpawnBoss();
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

    public void ShowWin()
    {
        StopAllCoroutines();
        StartCoroutine(ShowWinCr());
    }
    private IEnumerator ShowWinCr()
    {
        yield return new WaitForSeconds(3);
        yield return MoveDown(shopUi.gameObject, 1f);
        yield return MoveDown(coreUi.gameObject, 1f);
        coreUi.gameObject.SetActive(false);
        shopUi.gameObject.SetActive(false);
        yield return MoveUp(WinUi, 2f);
    }

    private IEnumerator MoveDown(GameObject obj, float desiredY)
    {
        while (obj.transform.position.y >= desiredY)
        {
            obj.transform.position += Vector3.down * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        var pos = obj.transform.position;
        obj.transform.position = new Vector3(pos.x, desiredY, pos.z);
    }

    private IEnumerator MoveUp(GameObject obj, float desiredY)
    {
        while (obj.transform.position.y <= desiredY)
        {
            obj.transform.position += Vector3.up * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        var pos = obj.transform.position;
        obj.transform.position = new Vector3(pos.x, desiredY, pos.z);
    }

    void SpawnBoss()
    {
        if (bossSpawned) return;
        bossSpawned = true;
        
        var boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        boss.GetComponent<Boss>().SetCore(this);
        
        StartCoroutine(MoveDown(gameObject, -2f)); // -3f if need to hide completely

        GameStateBeh.Paused = true;
    }

    public void OnBossDestroyed()
    {
        GameStateBeh.Paused = false;
        StartCoroutine(MoveUp(gameObject, _originalY));
    }
    
}
