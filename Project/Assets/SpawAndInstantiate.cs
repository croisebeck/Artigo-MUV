using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawAndInstantiate : MonoBehaviour {

	// Use this for initialization
	public GameObject obj;
    int num = 0;
	void atira()
	{
        
		GameObject instant= Instantiate (obj,new Vector3(transform.position.x+2,transform.position.y,transform.position.z),Quaternion.Euler(new Vector3(-90,90,0))) as GameObject;
        instant.name = "aviao ";
        instant.name += num.ToString();
        num++;
		
	}
}
