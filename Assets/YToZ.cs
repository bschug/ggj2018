using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class YToZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var position = transform.position;
        position.z = position.y / 1000f;
        transform.position = position;
	}
	void OnDrawGizmos() 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (this.transform.position, 0.1f);
	}
}
