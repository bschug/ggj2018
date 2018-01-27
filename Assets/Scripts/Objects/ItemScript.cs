using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour, IObject {

	[TextArea]
	public string[] description;
	public int index;

	public bool response()
	{
		if (index == description.Length) {
			index = 0;
			Debug.LogWarning (this.name + " added to inventory");
			Destroy (this.gameObject);
			return false;
		}

		Debug.Log (this.name + " says " + description[index]);
		index++;
		return true;
	}
}
