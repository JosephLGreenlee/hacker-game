using UnityEngine;
using System.Collections;

public class HeaderTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print (Camera.main.orthographicSize * Screen.width / Screen.height);
		print (Camera.main.orthographicSize);
		print (renderer.bounds.size.x);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
