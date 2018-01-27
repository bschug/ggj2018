using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour, IObject {

	[TextArea]
	public string[] description;
	public int index;

	public bool response()
	{
		if (index == description.Length) {
			index = 0;
			return false;
		}

		Debug.Log (this.name + " says " + description[index]);
		index++;
		return true;
	}
}

