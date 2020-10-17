using System;
using System.Collections.Generic;
using UnityEngine;


/**
 * Combined History / Generative approach.
 * Temporator saves state changes. Affected entity picks up state from history if it exists, otherwise it generates its own history and saves it to temporator.
 * 
 * Any changes (i.e blow platform up) should reset history of entity starting from the change point. Depending on the implementation we might even reset history 
 * every time player exits the loop - this will require seeded RNG to preserve consistency of unaffected entities, though.
 */
public class TemporatorHistory : MonoBehaviour
{
    public FloatVariable Time;
    float oldTime;
    public event Action<float> TimeChanged;
    public SortedList<float, string> History = new SortedList<float, string>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Time.RuntimeValue - oldTime) > 0.1f)
        {
            TimeChanged(Time.RuntimeValue - oldTime);
            oldTime = Time.RuntimeValue;
        }
    }

    public void SetHistoryEvent(string state, bool changeHistory = false)
    {
        History.Add(Time.RuntimeValue, state);
        if (changeHistory)
        {
            List<float> eventsToErase = new List<float>();
            foreach (var time in History.Keys)
            {
                if (time > Time.RuntimeValue)
                    eventsToErase.Add(time);
            }
            foreach (var time in eventsToErase)
            {
                History.Remove(time);
            }
        }
    }

    public string GetHistoryState()
    {
        string previousState = "";
        foreach (var time in History.Keys)
        {
            if (time <= Time.RuntimeValue)
                previousState = History[time];
            else return previousState;
        }
        return previousState;
    }

    public bool HasFutureEvents()
    {
        return History.Count > 0 && History.Keys[History.Count - 1] >= Time.RuntimeValue;
    }
}
