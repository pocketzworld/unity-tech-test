using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private NavGridPathNode[] _currentPath = Array.Empty<NavGridPathNode>();
    private int _currentPathIndex = 0;
    
    [SerializeField]
    private NavGrid _grid;
    [SerializeField]
    private float _speed = 10.0f;

    void Update()
    {
        // Check Input
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _currentPath = _grid.GetPath(transform.position, hitInfo.point);
                _currentPathIndex = 0;
            }
        }

        // Traverse
        if (_currentPathIndex < _currentPath.Length)
        {
            var currentNode = _currentPath[_currentPathIndex];
            
            var maxDistance = _speed * Time.deltaTime;
            var vectorToDestination = currentNode.Position - transform.position;
            var moveDistance = Mathf.Min(vectorToDestination.magnitude, maxDistance);

            var moveVector = vectorToDestination.normalized * moveDistance;
            moveVector.y = 0f; // Ignore Y
            transform.position += moveVector;

            if (transform.position == currentNode.Position)
                _currentPathIndex++;
        }
    }
}
