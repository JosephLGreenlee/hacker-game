using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform nodeTypesParent;

	// Use this for initialization
	void Start () {
		//TODO: Let NodeTypes handle this behavior?
		Node[] nodeTypes = nodeTypesParent.GetComponentsInChildren<Node>();
		Node startNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		startNode.transform.position = PlayerStart.instance.transform.position;
		startNode.transform.parent = this.transform;

		Node firstNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		firstNode.transform.position = new Vector3(startNode.transform.position.x + .1f,
		                                           startNode.transform.position.y + 2f,
		                                           startNode.transform.position.z);
		firstNode.transform.parent = startNode.transform;
		LineRenderer firstLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		firstLine.SetPosition (0,startNode.transform.position);
		firstLine.SetPosition (1, firstNode.transform.position);

		Node secondNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		secondNode.transform.position = new Vector3(startNode.transform.position.x + 2f,
		                                           startNode.transform.position.y + .1f,
		                                            startNode.transform.position.z);
		secondNode.transform.parent = startNode.transform;
		LineRenderer secondLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		secondLine.SetPosition (0, startNode.transform.position);
		secondLine.SetPosition (1, secondNode.transform.position);
		
		Node thirdNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		thirdNode.transform.position = new Vector3(startNode.transform.position.x + 1.5f,
		                                           startNode.transform.position.y + 1.5f,
		                                           startNode.transform.position.z);
		thirdNode.transform.parent = startNode.transform;
		LineRenderer thirdLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		thirdLine.SetPosition (0, startNode.transform.position);
		thirdLine.SetPosition (1, thirdNode.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
