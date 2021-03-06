﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (BoxCollider2D))]
public class Node : MonoBehaviour {

	public GameController gameController;
	public Node parent = null;
	public List<Node> children = new List<Node>();

	public bool hidden = false;
	public float traceStart;
	public bool runningTrace = false;

	public float hideStart;
	public bool runningHide = false;

	public float showStart;
	public bool runningShow = false;

	public float slideStart;
	public Vector3 origin;
	public Vector3 destination;
	public bool runningSlide = false;

	public LineRenderer connection;
	public LineRenderer trace;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (runningTrace) {
			TraceStep ();
		}
		if (runningHide) {
			HideStep ();
		}
		if (runningShow) {
			ShowStep ();
		}
		if (runningSlide) {
			SlideStep ();
		}
	}

	void OnMouseDown() {
		//Don't trace yourself
		if (GameController.playerNode != this) {
			Trace ();
			//GameController.playerNode.RemoveChild(this);
		}
	}

	public void Trace() {
		traceStart = Time.time;

		trace = (LineRenderer)Instantiate (TraceLineRenderer.instance);
		trace.SetPosition (0, Util.ZDelta (GameController.playerNode.transform.position,GameController.TRACE_ZDELTA));
		runningTrace = true;
	}
	void TraceStep() {
		//TODO: Move all this to a Trace line object
		float currTime = Time.time - traceStart;
		trace.SetPosition(1,Vector3.Lerp(Util.ZDelta(GameController.playerNode.transform.position,GameController.TRACE_ZDELTA),
		                                 Util.ZDelta(transform.position,GameController.TRACE_ZDELTA),currTime));
		if (currTime > 1.0f) {
			runningTrace = false;
			TraceComplete();
		}
	}
	void TraceComplete() {
		//TODO: Adjust this so that a "player camera" is what slides instead of the playerNode (conceptually)
		Node playerNode = GameController.playerNode;
		Vector3 origPos = PlayerStart.instance.transform.position;
		Vector3 delta = transform.position - playerNode.transform.position;
		gameController.SlideNodesBy (delta * 3);
		this.SlideBy (delta);

		GameController.playerNode = this;
		gameController.Invoke("SpawnNodes",0.5f);
	}

	public void Hide() {
		hideStart = Time.time;

		runningHide = true;
	}
	void HideStep() {
		float currTime = Time.time - hideStart;

		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (0, 0, 1), currTime);

		if (currTime > 0.5f) {
			transform.localScale = new Vector3(0,0,1);
			runningHide = false;
			hidden = true;
		}
	}

	public void Show() {
		showStart = Time.time;

		runningShow = true;
	}
	void ShowStep() {
		float currTime = Time.time - showStart;
		
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (1, 1, 1), currTime);
		
		if (currTime > 0.5f) {
			transform.localScale = new Vector3(1,1,1);
			runningShow = false;
			hidden = false;
		}
	}

	public void SlideTo(Vector3 slideDestination) {
		destination = slideDestination;
		origin = transform.position;
		slideStart = Time.time;

		runningSlide = true;
	}
	public void SlideBy(Vector3 slideDelta) {
		destination = transform.position - slideDelta;
		origin = transform.position;
		slideStart = Time.time;
		
		runningSlide = true;
	}
	public void SlideStep() {
		float currTime = Time.time - slideStart;
		float speed = 2.0f;
		
		transform.position = Vector3.Lerp (origin, destination, currTime * speed);

		if (currTime > 1/speed) {
			runningSlide = false;
			SlideComplete ();
		}
	}
	public void SlideComplete() {
		//Do Nothing
	}

	public void HideConnection() {
		connection.renderer.enabled = false;
		trace.renderer.enabled = false;
	}
	public void ShowConnection() {
		connection.renderer.enabled = true;
		trace.renderer.enabled = true;
	}

	public void AddChild(Node child) {
		children.Add (child);
		child.parent = this;
	}
	public void RemoveChild(Node child) {
		children.Remove (child);
		RemoveHelper (child);
	}
	public void RemoveHelper(Node node) {
		foreach (Node child in children) {
			RemoveHelper (child);
		}
		node.Remove ();
	}
	public void Remove() {
		parent.RemoveChild (this);
	}

	public Node GetParent() {
		return parent;
	}

	public List<Node> GetChildren() {
		return children;
	}
}
