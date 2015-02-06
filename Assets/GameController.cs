using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Node[] nodeTypes = gameObject.GetComponentsInChildren<Node>();
		Node startNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		startNode.transform.position = PlayerStart.instance.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
