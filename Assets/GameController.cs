using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public const float CONNECTION_ZDELTA = 1f;
	public const float TRACE_ZDELTA = 0.5f;

	public Transform nodeTypesParent;
	public static Transform nodeTypesParentStatic;
	public int numberOfLevels;
	public static Node playerNode;

	// Use this for initialization
	void Start () {
		//TODO: Let NodeTypes handle this behavior?
		Node[] nodeTypes = nodeTypesParent.GetComponentsInChildren<Node>();
		Node playerNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		GameController.playerNode = playerNode;

		playerNode.transform.position = PlayerStart.instance.transform.position;
		playerNode.transform.parent = this.transform;

		//Add a child
		Node firstNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		//Position the child
		firstNode.transform.position = new Vector3(playerNode.transform.position.x + .1f,
		                                           playerNode.transform.position.y + 2f,
		                                           playerNode.transform.position.z);
		firstNode.transform.parent = playerNode.transform;
		//Draw a line to the child
		LineRenderer firstLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		firstLine.SetPosition (0, Util.ZDelta(playerNode.transform.position,CONNECTION_ZDELTA));
		firstLine.SetPosition (1, Util.ZDelta(firstNode.transform.position,CONNECTION_ZDELTA));
		firstNode.connection = firstLine;

		Node secondNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		secondNode.transform.position = new Vector3(playerNode.transform.position.x + 2f,
		                                            playerNode.transform.position.y + .1f,
		                                            playerNode.transform.position.z);
		secondNode.transform.parent = playerNode.transform;
		LineRenderer secondLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		secondLine.SetPosition (0, Util.ZDelta(playerNode.transform.position,CONNECTION_ZDELTA));
		secondLine.SetPosition (1, Util.ZDelta(secondNode.transform.position,CONNECTION_ZDELTA));
		secondNode.connection = secondLine;
		
		Node thirdNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		thirdNode.transform.position = new Vector3(playerNode.transform.position.x + 1.5f,
		                                           playerNode.transform.position.y + 1.5f,
		                                           playerNode.transform.position.z);
		thirdNode.transform.parent = playerNode.transform;
		LineRenderer thirdLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		thirdLine.SetPosition (0, Util.ZDelta(playerNode.transform.position,CONNECTION_ZDELTA));
		thirdLine.SetPosition (1, Util.ZDelta(thirdNode.transform.position,CONNECTION_ZDELTA));
		thirdNode.connection = thirdLine;

		nodeTypesParentStatic = nodeTypesParent;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateConnections ();
	}

	void UpdateConnections() {
		List<Node> children = GetNodes ();
		foreach (Node child in children) {
			if (child.connection) {
				child.connection.SetPosition(0,Util.ZDelta(playerNode.transform.position,CONNECTION_ZDELTA));
				child.connection.SetPosition(1,Util.ZDelta(child.transform.position,CONNECTION_ZDELTA));
			}
			if (child.trace) {
				child.trace.SetPosition(0,Util.ZDelta(playerNode.transform.position,CONNECTION_ZDELTA));
				child.trace.SetPosition(1,Util.ZDelta(child.transform.position,CONNECTION_ZDELTA));
			}
		}
	}

	public void TrimNodes() {
		
	}

	public List<Node> GetNodes() {
		List<Node> ilist = new List<Node> ();
		Node[] children = GetComponentsInChildren<Node>();
		foreach (Node child in children) {
			ilist.Add(child);
		}
		return ilist;
	}

	/*void GetChildrenHelper(Node parent, List<Node> ilist) {
		ilist.Add (parent);
		Node[] children = GetComponentsInChildren<Node>();
		foreach (Node child in children) {
			GetChildrenHelper(child,ilist);
		}
	}*/

	public void AddNode(Node child) {
		
	}
}
