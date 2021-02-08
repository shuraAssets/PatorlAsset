using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[RequireComponent(typeof(MovePatrolPath))]
public class WayPointsPathBuilder : MonoBehaviour
{
    [SerializeField] private bool _loopPath = false;
    [SerializeField] private List<Transform> _wayPoints = new List<Transform>();

    private Transform _parentTransform = null;

    public bool LoopPath { get { return _loopPath; } }
    public List<Transform> WayPoints
    {
        get
        {
            return _wayPoints;
        }
    }

    public void CreateWaypoint()
    {

        if (!_parentTransform)
        {
            _parentTransform = new GameObject("WaypointContainer").transform;
        }

        var point = new GameObject("WayPount_" + _wayPoints.Count.ToString());
        point.transform.SetParent(_parentTransform);
        point.transform.position = transform.position;

        _wayPoints.Add(point.transform);

        if (PrefabUtility.GetPrefabInstanceStatus(gameObject) == PrefabInstanceStatus.Connected)
        {
            PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        }

        Debug.Log(_wayPoints.Count);
    }

    public void DeletePonit()
    {
        if (_wayPoints.Count - 1 < 0) return;
        DestroyImmediate(_wayPoints[_wayPoints.Count - 1].gameObject);
        _wayPoints.RemoveAt(_wayPoints.Count - 1);
    }

    private void OnDrawGizmos()
    {
        if (_wayPoints.Count - 1 < 0) return;
        Vector3 startPosition = _wayPoints[0].position;
        Vector3 previousPosition = startPosition;

        foreach (var point in _wayPoints)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(point.transform.position, 0.3f);
            Gizmos.DrawLine(previousPosition, point.transform.position);
            previousPosition = point.transform.position;
        }

        if (_loopPath)
        {
            Gizmos.DrawLine(previousPosition, startPosition);
        }

    }
}
