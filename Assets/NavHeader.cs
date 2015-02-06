using UnityEngine;
using System.Collections;

public class NavHeader : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Resize ();
	}

	void Resize() {
		float screenWidth = 2 * Camera.main.orthographicSize * Screen.width / Screen.height;
		float width = renderer.bounds.size.x;
		transform.localScale = transform.localScale * (screenWidth / width);
		
		float screenHeight = 2 * Camera.main.orthographicSize;
		float height = renderer.bounds.size.y;
		float yPos = screenHeight / 2 - height / 2;
		transform.position = new Vector3 (0, yPos, -10);
	}
}
