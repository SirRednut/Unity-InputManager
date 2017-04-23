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
		//Check every input and every function run
		foreach(InputData ipt in inputList)
			if (Input.GetKey(ipt.Key1) || Input.GetKey(ipt.Key2))
				foreach (UnityEvent iptEvent in ipt.events)
				{
					iptEvent.Invoke();
				}

	}
}

[System.SerializableAttribute]
public class InputData{
	
	[TooltipAttribute("A name so the list displays the name instead of 'Element #'")]
	public string InputName;

	[TextAreaAttribute]
	public string Description;

	[TooltipAttribute("Select what keycodes to use for this input")]
	public KeyCode Key1;
	[TooltipAttribute("Select what keycodes to use for this input")]
	public KeyCode Key2;

	[TooltipAttribute("Function Calls Are Done Here")]
	public UnityEvent[] events;
}