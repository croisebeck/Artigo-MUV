using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSpaw : MonoBehaviour {

	// Use this for initialization
	public float tempo=5;
	public GameObject[] objs;
	public int randomico=0;
    public int lenghtofSpaw=0;

	void Start () {
		objs = GameObject.FindGameObjectsWithTag ("spawing");
      
		
	}
	
	// Update is called once per frame
	void Update () {
        
		tempo -= Time.deltaTime;


		if (tempo <= 0) 
		{
			randomico = Random.Range (0, objs.Length);

            tempo = 5;
			objs [randomico].SendMessage ("atira");
		}
	}
}
