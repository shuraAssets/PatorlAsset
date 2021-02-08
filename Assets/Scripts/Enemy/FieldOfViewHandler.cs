using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewHandler : MonoBehaviour
{
    [Header("Mesh Settings")]
    [SerializeField] private MeshFilter _fovMesh = null;

    [Header("Raycast Settings")]
    [SerializeField] private int _rayCount = 10;
    [SerializeField] private int _angle = 90;
    [SerializeField] private float _rayLenght = 2f;
    // Debug.DrawRay(transform.position, rotation * direction * _rayLenght, Color.red);
    [SerializeField] private int _runawayLayerNumber;

    [SerializeField] private Vector3[] _verticles;
    [SerializeField] private int[] _triangles;
    [SerializeField] private Vector2[] _uv;


    public delegate void PlayerDetected();
    public static PlayerDetected LoseGame;

    void Update()
    {
        FOVActive();
    }

    private void FOVActive()
    {
        #region MeshInfo
        var mesh = new Mesh();
        mesh.name = "FuckingFov";
        _fovMesh.mesh = mesh;


        _verticles = new Vector3[_rayCount + 3];
        _triangles = new int[_rayCount * 3];
        _verticles[0] = Vector3.zero;

        _uv = new Vector2[_verticles.Length];
        _uv[0] = Vector2.zero;

        var vertexPosition = Vector3.zero;

        #endregion

        var rotation = transform.rotation;
        int localAngle = _angle / _rayCount;
        var currentAngle = (_angle / 2) * -1;

        var vertexIndex = 1;
        var triangleIndex = 0;


        for (var i = 0; i <= _rayCount; i++)
        {
            var rotationMod = Quaternion.AngleAxis(currentAngle, transform.up);
            var direction = rotationMod * Vector3.forward;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, rotation * direction, out hit, _rayLenght))
            {
                vertexPosition = direction * hit.distance;

                if (hit.transform.gameObject.layer == _runawayLayerNumber)
                {
                    LoseGame?.Invoke();
                }
            }
            else
            {
                vertexPosition = direction * _rayLenght;

            }

            Debug.DrawRay(transform.position, rotation * direction * _rayLenght, Color.blue);

            _uv[vertexIndex] = new Vector2((float)vertexPosition.x / _rayLenght, (float)vertexPosition.z / _rayLenght);

            _verticles[vertexIndex] = vertexPosition + Vector3.zero;

            if (i > 0)
            {

                _triangles[triangleIndex + 0] = 0;
                _triangles[triangleIndex + 1] = vertexIndex - 1;
                _triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            currentAngle += localAngle;
            vertexIndex++;
        }



        mesh.vertices = _verticles;
        mesh.uv = _uv;
        mesh.triangles = _triangles;
        mesh.RecalculateNormals();
    }
}
