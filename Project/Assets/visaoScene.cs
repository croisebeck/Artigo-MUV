using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class visaoScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
			
		
	}
	
	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR
		UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
		#endif
		
	}
}
