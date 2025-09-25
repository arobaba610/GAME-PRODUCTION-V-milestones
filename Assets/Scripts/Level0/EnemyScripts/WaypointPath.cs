using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform GetWaypoint(int index)
    {
        if (index >= 0 && index < waypoints.Length)
            return waypoints[index];
        return null;
    }

    public int WaypointCount => waypoints.Length;
}
