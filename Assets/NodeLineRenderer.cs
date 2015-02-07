using UnityEngine;
using System.Collections;

public class NodeLineRenderer : MonoBehaviour {

	//Here is a private reference only this class can access
	private static LineRenderer _instance;
	
	//This is the public reference that other classes will use
	public static LineRenderer instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = (LineRenderer)GameObject.FindObjectOfType<NodeLineRenderer>().renderer;
			return _instance;
		}
	}
}