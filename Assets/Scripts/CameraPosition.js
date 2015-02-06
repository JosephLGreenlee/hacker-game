#pragma strict

function Start () {
	var xRatio = 3.199469 / Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))[0];
	var yRatio = 4.800411 / Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))[1];
	camera.orthographicSize = Camera.main.orthographicSize * Mathf.Max(xRatio,yRatio);
	print(yRatio);
	print(xRatio);
}

function Update () {

}