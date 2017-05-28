using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BezierSpline : MonoBehaviour {

    [SerializeField]
    private List<Vector3> nodes = new List<Vector3>();

    public List<Vector3> Nodes { get { return nodes; } }
    public int SubcurveCount { get { return (nodes.Count - 1) / 3; } }

	private void Start () {
		if(nodes.Count < 4)
        {
            Debug.LogError("Eine Bezier-Kurve muss aus mindestens 4 Punkten bestehen! Es werden " + (4 - nodes.Count) + " Knotenpunkte bei (0, 0, 0) hinzugefügt.");
            do
            {
                nodes.Add(Vector3.zero);
            } while (nodes.Count < 4);
        }
	}

    /// <summary>
    /// Gibt die Position auf der <see cref="BezierSpline"/> mit dem aktuellen fortlaufszähler tParam zurück
    /// </summary>
    public Vector3 GetPoint(float tParam)
    {
        int i;
        if (tParam >= 1f)
        {
            tParam = 1f;
            i = nodes.Count - 4;
        }
        else
        {
            tParam = Mathf.Clamp01(tParam) * SubcurveCount;
            i = (int)tParam;
            tParam -= i;
            i *= 3;
        }
        return GetPoint(nodes[i], nodes[i + 1], nodes[i + 2], nodes[i + 3], tParam);
    }

    /// <summary>
    /// Gibt eine Position auf der <see cref="BezierSpline"/> in abhängigkeit der Punkte p0 bis p3 und des fortlaufzählers tParam zurück
    /// </summary>
    public Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float tParam)
    {
        tParam = Mathf.Clamp01(tParam);
        float tTmp = 1.0f - tParam;
        return   Mathf.Pow(tTmp, 3) * p0 
               + 3.0f * Mathf.Pow(tTmp, 2) * tParam * p1
               + 3.0f * tTmp * Mathf.Pow(tParam, 2) * p2
               + Mathf.Pow(tParam, 3) * p3;
    }

    /// <summary>
    /// Die <see cref="BezierSpline"/> wird um 4, wenn keine Punkte enthalten, sonst 3 Punkte erweitert
    /// </summary>
    public void AddCurve()
    {
        if(nodes.Count == 0)
        {
            nodes.Add(Vector3.zero);
        }
        Vector3 node = nodes[nodes.Count - 1];
        for(int i = 0; i < 3; i++)
        {
            nodes.Add(node + Vector3.right * (i + 1));
        }
    }

#region Operator-Overloadings

    public static BezierSpline operator + (BezierSpline splineA, BezierSpline splineB)
    {
        splineA.nodes.AddRange(splineB.nodes);
        return splineA;
    }

    public static BezierSpline operator * (BezierSpline spline, float factor)
    {
        BezierSpline output = (BezierSpline)((object)spline.MemberwiseClone());
        Vector3[] nodeOutput = new Vector3[spline.nodes.Count];
        spline.nodes.CopyTo(nodeOutput);
        for(int i = 0; i < nodeOutput.Length; i++)
        {
            nodeOutput[i] = nodeOutput[i] * factor;
        }
        output.nodes = nodeOutput.ToList();
        return output;
    }

#endregion //Operator-Overloadings
}