using UnityEngine;


public static class Bezier {

	public static Vector3 GetPoint(Vector3 P0, Vector3 P1, Vector3 P2, float Value)
    {
        return Vector3.Lerp(Vector3.Lerp(P0, P1, Value), Vector3.Lerp(P1, P2, Value), Value);
    }
}
