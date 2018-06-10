using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	[SerializeField]Fade fade;

	// Use this for initialization
	void Start () {
		fade.FadeIn (10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
