using UnityEngine;

public class TmpCrystal : MonoBehaviour
{
    private LineRenderer lr;
    public GameObject Tank;
    private Vector3 _lastPos = Vector3.zero;
    public FloatVariable Time;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
    }

    Vector3 LastTankRelativePos
    {
        get { return _lastPos - transform.position; }
    }

    Vector3 CurrentTankRelativePos
    {
        get { return Tank.transform.position - transform.position; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Tank != null)
        {
            if (Vector3.Distance(Tank.transform.position, transform.position) < 6)
            {
                // hurr durr start condishun
                if (_lastPos == Vector3.zero) _lastPos = CurrentTankRelativePos;

                lr.SetPosition(1, Tank.transform.position);
                Time.RuntimeValue += Vector3.SignedAngle(LastTankRelativePos, CurrentTankRelativePos, Vector3.up) * 0.6f;
                Time.RuntimeValue = Mathf.Clamp(Time.RuntimeValue, 0, 50000);
                _lastPos = Tank.transform.position;
            }
            else
            {
                lr.SetPosition(1, transform.position);
            }
        }
    }
}
