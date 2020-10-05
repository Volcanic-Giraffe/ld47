using System.Collections;
using System.Collections.Generic;
using GameState;
using Shadow;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private Animator _anim;

    public GameStateBehaviour gameStateBeh;
    public Shadower shadower;

    private bool _triggeredOnce;
    
    private static readonly int IsLifting = Animator.StringToHash("IsLifting");
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Hero")
        {
            if (_triggeredOnce) return;
            _triggeredOnce = true;
            
            StartCoroutine(LiftTank());
            _anim.SetBool(IsLifting, true);

            if (shadower != null) shadower.DecreaseBeam = false;
            if (gameStateBeh != null) gameStateBeh.Paused = true;
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

        var moveInput = tank.GetComponent<PlayerRBController>();
        if (moveInput != null) moveInput.DisableInput();
        
        
        var towerInput = tank.GetComponent<TurretPicker>();
        if (towerInput != null) towerInput.DisableInput();
        
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
