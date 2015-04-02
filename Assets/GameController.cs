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
	public Node[] nodeTypes;

	// Use this for initialization
	void Start () {
		//TODO: Let NodeTypes handle this behavior?
		nodeTypes = nodeTypesParent.GetComponentsInChildren<Node>();
		Node playerNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		GameController.playerNode = playerNode;

		playerNode.transform.position = PlayerStart.instance.transform.position;
		playerNode.transform.parent = this.transform;

		//Add a child
		Node firstNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		AddNode (firstNode);

		Node secondNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		AddNode (secondNode);
		
		Node thirdNode = (Node)Instantiate(nodeTypes [Random.Range (0, nodeTypes.Length)]);
		AddNode (thirdNode);

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
				child.connection.SetPosition(0,Util.ZDelta(child.parent.transform.position,CONNECTION_ZDELTA));
				child.connection.SetPosition(1,Util.ZDelta(child.transform.position,CONNECTION_ZDELTA));
			}
			if (child.trace) {
				child.trace.SetPosition(0,Util.ZDelta(child.parent.transform.position,CONNECTION_ZDELTA));
				child.trace.SetPosition(1,Util.ZDelta(child.transform.position,CONNECTION_ZDELTA));
			}
		}
	}

	public void TrimNodes() {
		Node current = playerNode;
		for (int i = 0; i < 2; i++) {
			if (current.GetParent() == null) {
				return;
			}
			current = current.GetParent();
		}
		//Do something with current (kill everything but its descendants?)
	}

	public void SpawnNodes() {
		Node newNode = (Node)Instantiate (nodeTypes [Random.Range (0, nodeTypes.Length)]);
		AddNode (newNode);
		newNode = (Node)Instantiate (nodeTypes [Random.Range (0, nodeTypes.Length)]);
		AddNode (newNode);
		newNode = (Node)Instantiate (nodeTypes [Random.Range (0, nodeTypes.Length)]);
		AddNode (newNode);
	}

	public List<Node> GetNodes() {
		List<Node> ilist = new List<Node> ();
		Node[] children = GetComponentsInChildren<Node>();
		foreach (Node child in children) {
			ilist.Add(child);
		}
		return ilist;
	}

	public void AddNode(Node child) {
		int cnt = playerNode.children.Count;
		playerNode.AddChild (child);
		child.transform.parent = this.transform;

		child.transform.position = new Vector3(PlayerStart.instance.transform.position.x + cnt + Random.Range(-.2f,.5f),
		                                       PlayerStart.instance.transform.position.y + 2-cnt + Random.Range(-.2f,.5f),
		                                       PlayerStart.instance.transform.position.z);
		//Draw a line to the child
		LineRenderer firstLine = (LineRenderer)Instantiate (NodeLineRenderer.instance);
		firstLine.SetPosition (0, Util.ZDelta(playerNode.transform.position,CONNECTION_ZDELTA));
		firstLine.SetPosition (1, Util.ZDelta(child.transform.position,CONNECTION_ZDELTA));
		child.connection = firstLine;
		child.connection.transform.parent = child.transform;
	}

	public void SlideNodesBy(Vector3 slideDelta) {
		List<Node> nodes = GetNodes ();
		foreach (Node node in nodes) {
			node.SlideBy(slideDelta);		
		}
	}
}
