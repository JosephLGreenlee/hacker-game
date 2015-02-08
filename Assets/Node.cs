using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Node : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		Node[] all = Object.FindObjectsOfType (typeof(Node)) as Node[];
		foreach (Node obj in all) {
			if (obj.transform.parent != GameController.nodeTypesParentStatic) {
				obj.transform.renderer.enabled = false;
				obj.collider2D.enabled = false;
			}
		}

		this.transform.renderer.enabled = true;
		Node[] nodeTypes = GameController.nodeTypesParentStatic.GetComponentsInChildren<Node>();
		this.transform.position = PlayerStart.instance.transform.position;
		
		Node firstNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		firstNode.transform.position = new Vector3(this.transform.position.x + .1f,
		                                           this.transform.position.y + 2f,
		                                           this.transform.position.z);
		firstNode.transform.parent = this.transform;
		/*LineRenderer firstLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		firstLine.SetPosition (0,this.transform.position);
		firstLine.SetPosition (1, firstNode.transform.position);*/
		
		Node secondNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		secondNode.transform.position = new Vector3(this.transform.position.x + 2f,
		                                            this.transform.position.y + .1f,
		                                            this.transform.position.z);
		secondNode.transform.parent = this.transform;
		/*LineRenderer secondLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		secondLine.SetPosition (0, this.transform.position);
		secondLine.SetPosition (1, secondNode.transform.position);*/
		
		Node thirdNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		thirdNode.transform.position = new Vector3(this.transform.position.x + 1.5f,
		                                           this.transform.position.y + 1.5f,
		                                           this.transform.position.z);
		thirdNode.transform.parent = this.transform;
		/*LineRenderer thirdLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		thirdLine.SetPosition (0, this.transform.position);
		thirdLine.SetPosition (1, thirdNode.transform.position);*/
	}
}
