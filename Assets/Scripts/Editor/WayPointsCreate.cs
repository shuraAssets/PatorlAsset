using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPointsPathBuilder))]
public class WayPointsCreate : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();


        WayPointsPathBuilder builder = (WayPointsPathBuilder)target;

        if (GUILayout.Button("Create"))
        {
            builder.CreateWaypoint();
        }

        if (GUILayout.Button("Delete"))
        {
            builder.DeletePonit();
        }

    }
}
