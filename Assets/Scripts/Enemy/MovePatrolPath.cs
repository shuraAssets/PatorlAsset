using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePatrolPath : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 30;
    [SerializeField] private float _delayComback = 3f;
    [SerializeField] private float _waitTime = 0.5f;

    private WayPointsPathBuilder _waypointsBuilder;
    private bool _onLoopPath = false;

    private void Awake()
    {
        _waypointsBuilder = GetComponent<WayPointsPathBuilder>();
    }

    private void OnEnable()
    {
        _onLoopPath = _waypointsBuilder.LoopPath;
        var waypoints = _waypointsBuilder.WayPoints;

        StartCoroutine(FollowPath(waypoints));
    }

    private IEnumerator FollowPath(List<Transform> points)
    {

        transform.position = points[0].position;

        var targetWaypointIndex = 1;
        var targetWaypoint = points[targetWaypointIndex];

        while (true)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime * GlobalSettings.worldTime);
            RotationInTarget(targetWaypoint);

            if (transform.position == targetWaypoint.position)
            {

                if (_onLoopPath)
                {
                    targetWaypointIndex = (targetWaypointIndex + 1) % points.Count;
                }
                else
                {
                    if (targetWaypointIndex == points.Count - 1)
                    {
                        points.Reverse();
                        targetWaypointIndex = 0;
                        yield return new WaitForSeconds(_delayComback);
                    }
                    targetWaypointIndex = (targetWaypointIndex + 1);
                }

                targetWaypoint = points[targetWaypointIndex];

                yield return new WaitForSeconds(_waitTime);
            }
            yield return null;
        }
    }

    private void RotationInTarget(Transform target)
    {
        var direction = target.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if (rotation.eulerAngles == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _turnSpeed * Time.fixedDeltaTime);
    }
}

