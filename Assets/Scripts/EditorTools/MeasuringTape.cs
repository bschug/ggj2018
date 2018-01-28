using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class MeasuringTape : MonoBehaviour {

    public float Size = 0.1f;
    public float WalkSpeed = 2;
    public float Length = 0;

    private List<MeasuringTape> Path;

    void Awake()
    {
        var paths = FindObjectsOfType<MeasuringTapePath>();
        if (paths.Length == 0)
        {
            var pathObj = gameObject.AddComponent<MeasuringTapePath>() as MeasuringTapePath;
            Path = pathObj.Points;
        }
        else
        {
            Path = paths[0].Points;
        }

        Path.Add(this);
    }

    private void OnDestroy()
    {
        Path.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, Size);

        if (!IsFirst)
        {
            var idx = PathPos;
            var prev = Path[idx - 1];
            var mypos = Path[idx].transform.position;
            var lastpos = prev.transform.position;

            Length = prev.Length + (lastpos - transform.position).magnitude;

            Gizmos.DrawLine(lastpos, mypos);
            Handles.Label(mypos, string.Format("{0:0.00}", Length / WalkSpeed));
        }
    }

    bool IsFirst { get { return Path[0] == this; } }
    int PathPos { get { return Path.IndexOf(this); } }

}
