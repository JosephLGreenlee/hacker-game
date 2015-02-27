using UnityEngine;

public class Util
{
	public static Vector3 ZDelta(Vector3 position, float z) {
		return new Vector3(position.x, position.y, position.z + z);
	}
}

