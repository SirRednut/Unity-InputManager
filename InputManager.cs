///// InputManager by Alex Bryant //////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InputManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Disable this manager when cursor is enabled")]
    private bool CursorLocked = false;
    private bool canInput = true;

    [SerializeField]
    private List<InputData> inputList;

    void Update()
    {
        if (CursorLocked && Cursor.lockState != CursorLockMode.Locked)
            canInput = false;
        else
            canInput = true;

        if (canInput)
        {
            //Check every input and every function run
            foreach (InputData ipt in inputList)
            {
                if (Input.GetKey(ipt.standardInputs.Key1) || Input.GetKey(ipt.standardInputs.Key2))
                    ipt.standardInputs.HoldEvents.Invoke();

                if (Input.GetKeyDown(ipt.standardInputs.Key1) || Input.GetKeyDown(ipt.standardInputs.Key2))
                    ipt.standardInputs.TapEvents.Invoke();

                if (Input.GetKeyUp(ipt.standardInputs.Key1) || Input.GetKeyUp(ipt.standardInputs.Key2))
                    ipt.standardInputs.ReleaseEvents.Invoke();



                if (ipt.axisInputs.axisName != "")
                {
                    for (int i = 0; i < ipt.axisInputs.AxisEvent.GetPersistentEventCount(); i++)
                    {
                        string type = ipt.axisInputs.AxisEvent.GetPersistentTarget(i).GetType().ToString();

                        if (ipt.axisInputs.isRaw)
                        {
                            GameObject.Find(ipt.axisInputs.AxisEvent.GetPersistentTarget(i).name)
                                        .GetComponent(type)
                                        .SendMessage(ipt.axisInputs.AxisEvent.GetPersistentMethodName(i), Input.GetAxisRaw(ipt.axisInputs.axisName));
                        }
                        else
                        {
                            GameObject.Find(ipt.axisInputs.AxisEvent.GetPersistentTarget(i).name)
                                        .GetComponent(type)
                                        .SendMessage(ipt.axisInputs.AxisEvent.GetPersistentMethodName(i), Input.GetAxis(ipt.axisInputs.axisName));
                        }
                    }
                }
                else if (ipt.axisInputs.AxisEvent.GetPersistentEventCount() > 0)
                {
                    Debug.LogError("Axis Must Have A Name");
                }
            }
        }
    }

    public void ExternalEventCalls(string[] eventNames)
    {
        foreach (InputData ipt in inputList)
        {
            foreach (string eventName in eventNames)
            {
                if (ipt.InputName == eventName)
                {
                    ipt.standardInputs.HoldEvents.Invoke();
                    ipt.standardInputs.TapEvents.Invoke();
                    ipt.standardInputs.ReleaseEvents.Invoke();

                    break;
                }
            }
        }
    }

    public void ExternalEventCalls(string eventName)
    {
        foreach (InputData ipt in inputList)
        {
            if (ipt.InputName == eventName)
            {
                ipt.standardInputs.HoldEvents.Invoke();
                ipt.standardInputs.HoldEvents.Invoke();
                ipt.standardInputs.HoldEvents.Invoke();

                return;
            }
        }
    }
}

[System.Serializable]
public class InputData
{

    [Tooltip("A name so the list displays the name instead of 'Element #'")]
    public string InputName;

    [TextArea]
    public string Description;

    public StandardInputs standardInputs;

    public AxisInputs axisInputs;

}

[System.Serializable]
public class StandardInputs
{
    [Tooltip("Select what keycodes to use for this input")]
    public KeyCode Key1;
    [Tooltip("Select what keycodes to use for this input")]
    public KeyCode Key2;

    [Tooltip("Function Calls Are Done Here When Button Is Held")]
    public UnityEvent HoldEvents;

    [Tooltip("Function Calls Are Done Here When Button Is Pressed Once")]
    public UnityEvent TapEvents;

    [Tooltip("Function Calls Are Done Here When Button Is Released")]
    public UnityEvent ReleaseEvents;
}


[System.Serializable]
public class AxisInputs
{
    [Tooltip("Name of axis - Axis must be first setup in the Unity input system")]
    public string axisName;

    [Tooltip("Raw means no smoothing. -1, 0 and 1 only - Int")]
    public bool isRaw;

    [Tooltip("Function Calls Are Done Here When Axis Is Applied, NOTE - ARGUMENTS ARE IGNORED AS PER NATURE OF AXIS EVENTS")]
    public UnityEvent AxisEvent;
}