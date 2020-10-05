using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private Animator _anim;

    private static readonly int IsLifting = Animator.StringToHash("IsLifting");
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Hero")
        {
            StartCoroutine(LiftTank());
            _anim.SetBool(IsLifting, true);

        }
    }

    private IEnumerator LiftTank()
    {
        yield return new WaitForSeconds(2);
        GameObject tank = null;
        var hs = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var he in hs)
        {
            if (he.GetComponent<TankUpgrades>() != null)
            {
                tank = he;
                break;
            }
        }
        if (tank == null) yield break;
        var rb = tank.GetComponent<Rigidbody>();
        var core = GameObject.FindGameObjectWithTag("CoreMain");
        core.GetComponent<CoreMain>().ShowWin();

        while (true)
        {
            rb.AddForce(Vector3.up * 10 * Time.deltaTime, ForceMode.VelocityChange);
            yield return new WaitForEndOfFrame();
        }
    }
}
