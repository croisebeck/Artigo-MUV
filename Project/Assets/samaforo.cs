using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samaforo : MonoBehaviour {

	public GameObject[] obj;

	void Start()
	{
		obj = GameObject.FindGameObjectsWithTag ("Player");
	}

	void OnTriggerEnter(Collider cool)
	{
		if (cool.gameObject.tag == "Player") {
			foreach (GameObject interator in obj)
				interator.SendMessage ("plausivelPassagem", true);
		}
	}
	void OnTriggerExit(Collider cool)
	{
		//foreach(GameObject interator in obj)
			//interator.SendMessage ("plausivelPassagem",false);
	}
}
