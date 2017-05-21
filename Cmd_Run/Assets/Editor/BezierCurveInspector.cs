using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor {

    private BezierSpline spline = null;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int InterpolationSteps = 20;
    private const float DirectionScale = 0.5f;

    private void OnSceneGUI()
    {
        spline = target as BezierSpline;
        handleTransform = spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowHandle(0);
        //Startwert 1, weil Node 0 nicht Startpunkt der Kurve!
        for(int i = 1; i < spline.Nodes.Count; i += 3)
        {
            Vector3 p1 = ShowHandle(i);
            Vector3 p2 = ShowHandle(i + 1);
            Vector3 p3 = ShowHandle(i + 2);

            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);

            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2.0f);
            p0 = p3;
        }

        Handles.DrawBezier(spline.Nodes[0], spline.Nodes[3], spline.Nodes[1], spline.Nodes[2], Color.white, null, 2.0f);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        spline = target as BezierSpline;

        if(GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);
        }
    }

    private Vector3 ShowHandle(int index)
    {
        Vector3 point = handleTransform.TransformPoint(spline.Nodes[index]);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Move Point");
            EditorUtility.SetDirty(spline);
            spline.Nodes[index] = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }
}
