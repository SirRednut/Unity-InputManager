///// InputManager by Alex Bryant //////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InputManager : MonoBehaviour {

    //Uncomment this if not wanting an extra list in inspector
    /*public InputData 	ipt_Forwards, ipt_Backwards, ipt_Left, ipt_Right,
						ipt_Escape, ipt_jump, ipt_sprint, ipt_interact,
						ipt_inventory, ipt_click;*/

    [SerializeField]
    [Tooltip("Disable this manager when cursor is enabled")]
    private bool CursorLocked = false;
    private bool canInput = true;

    [SerializeField]
	private List<InputData> inputList;

	void Start () {
		//Uncomment this if not wanting an extra list in inspector
		/*inputList = new List<InputData>{
											ipt_Forwards, ipt_Backwards, ipt_Left, ipt_Right,
											ipt_Escape, ipt_jump, ipt_sprint, ipt_interact,
											ipt_inventory, ipt_click
										};*/
	}
	
	void Update ()
	{
        if (CursorLocked && Cursor.lockState != CursorLockMode.Locked)
        {
            canInput = false;
        }
        else
        {
            canInput = true;
        }

        if (canInput)
        {
            //Check every input and every function run
            foreach (InputData ipt in inputList)
            {
                if (ipt.HoldEvents.Length > 0)
                {
                    if (Input.GetKey(ipt.Key1) || Input.GetKey(ipt.Key2))
                        foreach (UnityEvent iptEvent in ipt.HoldEvents)
                        {
                            iptEvent.Invoke();
                        }
                }

                if (ipt.TapEvents.Length > 0)
                {
                    if (Input.GetKeyDown(ipt.Key1) || Input.GetKeyDown(ipt.Key2))
                        foreach (UnityEvent iptEvent in ipt.TapEvents)
                        {
                            iptEvent.Invoke();
                        }
                }

                if (ipt.ReleaseEvents.Length > 0)
                {
                    if (Input.GetKeyUp(ipt.Key1) || Input.GetKeyUp(ipt.Key2))
                        foreach (UnityEvent iptEvent in ipt.ReleaseEvents)
                        {
                            iptEvent.Invoke();
                        }
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
                    foreach (UnityEvent iptEvent in ipt.HoldEvents)
                    {
                        iptEvent.Invoke();
                    }

                    foreach (UnityEvent iptEvent in ipt.TapEvents)
                    {
                        iptEvent.Invoke();
                    }

                    foreach (UnityEvent iptEvent in ipt.ReleaseEvents)
                    {
                        iptEvent.Invoke();
                    }

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
                foreach (UnityEvent iptEvent in ipt.HoldEvents)
                {
                    iptEvent.Invoke();
                }

                foreach (UnityEvent iptEvent in ipt.TapEvents)
                {
                    iptEvent.Invoke();
                }

                foreach (UnityEvent iptEvent in ipt.ReleaseEvents)
                {
                    iptEvent.Invoke();
                }

                return;
            }
        }
    }
}

[System.Serializable]
public class InputData{
	
	[Tooltip("A name so the list displays the name instead of 'Element #'")]
	public string InputName;

	[TextArea]
	public string Description;

	[Tooltip("Select what keycodes to use for this input")]
	public KeyCode Key1;
	[Tooltip("Select what keycodes to use for this input")]
	public KeyCode Key2;

	[Tooltip("Function Calls Are Done Here When Button Is Held")]
	public UnityEvent[] HoldEvents;

	[Tooltip("Function Calls Are Done Here When Button Is Pressed Once")]
	public UnityEvent[] TapEvents;

	[Tooltip("Function Calls Are Done Here When Button Is Released")]
	public UnityEvent[] ReleaseEvents;

}
