using System;
using System.Collections;
using UnityEngine;


struct GrowState
{
    public const string Down = "down";
    public const string Grow = "grow";
    public const string Up = "up";
    public const string Shrink = "shrink";
}

[RequireComponent(typeof(TemporatorHistory))]
public class TmpMoveable : MonoBehaviour
{
    public string CurrentState = GrowState.Down;
    public float GrowSpeed = 1;
    public float DemSpeed = 1;
    private Vector3 StartPos;
    private Vector3 EndPos;
    public Vector3 Movement;
    TemporatorHistory _tmpr;

    Vector3 GrowVector
    {
        get { return (EndPos - StartPos).normalized; }
    }

    void Start()
    {
        _tmpr = GetComponent<TemporatorHistory>();
        _tmpr.TimeChanged += OnTmpUpdate;

        StartPos = transform.localPosition;
        EndPos = transform.localPosition + Movement;
    }

    void Update()
    {

    }

    void OnTmpUpdate(float delta)
    {
        var hstate = _tmpr.GetHistoryState();
        if (hstate != "" && hstate != CurrentState)
        {
            CurrentState = hstate;
            //Debug.Log($"{this.gameObject} is now {CurrentState}");
        }

        switch (CurrentState)
        {
            case GrowState.Grow:
                HandleGrow(delta);
                break;
            case GrowState.Up:
            case GrowState.Down:
                HandleKeep(delta);
                break;
            case GrowState.Shrink:
                HandleShrink(delta);
                break;
        }
    }

    public void Demolish()
    {
        // TODO need to remember position in history if we modify it.
        //StartCoroutine(FallDown());
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(3);
        while ((transform.localPosition - EndPos).magnitude < (StartPos - EndPos).magnitude)
        {
            transform.localPosition -= GrowVector * Time.deltaTime * 10;
            yield return null;
        }
        CurrentState = GrowState.Down;
        _tmpr.SetHistoryEvent(CurrentState, true);
    }

    private void HandleShrink(float delta)
    {
        transform.localPosition -= GrowVector * GrowSpeed * delta;
        if ((transform.localPosition - EndPos).magnitude >= (StartPos - EndPos).magnitude)
        {
            transform.localPosition = StartPos;
            CurrentState = GrowState.Down;
            //Debug.Log($"{this.gameObject} is now {CurrentState}");
            if (delta > 0 && !_tmpr.HasFutureEvents())
            {
                //Debug.Log($"Saving to history");
                _tmpr.SetHistoryEvent(CurrentState);
            }
        }
    }

    private void HandleKeep(float delta)
    {
        bool isUp = CurrentState == GrowState.Up;
        if (delta > 0 && !_tmpr.HasFutureEvents())
        {
            if (UnityEngine.Random.value > (isUp ? 0.997f : 0.99f))
            {
                CurrentState = isUp ? GrowState.Shrink : GrowState.Grow;
                //Debug.Log($"{this.gameObject} is now {CurrentState}");
                if (delta > 0 && !_tmpr.HasFutureEvents())
                {
                    //Debug.Log($"Saving to history");
                    _tmpr.SetHistoryEvent(CurrentState);
                }
            }
        }
    }

    private void HandleGrow(float delta)
    {
        transform.localPosition += GrowVector * GrowSpeed * delta;
        if ((transform.localPosition - StartPos).magnitude >= (EndPos - StartPos).magnitude)
        {
            transform.localPosition = EndPos;
            CurrentState = GrowState.Up;
            //Debug.Log($"{this.gameObject} is now {CurrentState}");
            if (delta > 0 && !_tmpr.HasFutureEvents())
            {
                //Debug.Log($"Saving to history");
                _tmpr.SetHistoryEvent(CurrentState);
            }
        }
    }
}
