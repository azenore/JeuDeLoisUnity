using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniGamePlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveSpeed = 5f;

    /// <summary>Distance threshold to consider a waypoint as reached.</summary>
    private const float WaypointReachedDistance = 0.2f;

    private NavMeshAgent _agent;
    private bool _isFrozen;

    // Queue of intermediate waypoint positions to travel through before the final destination.
    private readonly Queue<Vector3> _pathQueue = new();
    private Vector3 _finalDestination;
    private bool _isMoving;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
        _agent.enabled = true;
    }

    private void Update()
    {
        if (_isFrozen)
            return;

        if (Input.GetMouseButtonDown(0))
            TryMoveToWaypoint();

        AdvancePath();
    }

    private void TryMoveToWaypoint()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out MiniGameWaypoint _))
            MoveTo(hit.transform.position);
    }

    /// <summary>
    /// Moves the player to the destination using NavMesh pathfinding.
    /// If the destination is unreachable directly, the player travels through
    /// the nearest reachable waypoints first.
    /// </summary>
    public void MoveTo(Vector3 destination)
    {
        if (_isFrozen)
            return;

        _pathQueue.Clear();
        _finalDestination = destination;

        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(destination, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            // Direct path exists — go straight there.
            _agent.SetDestination(destination);
        }
        else
        {
            // No direct path — build a chain through the nearest reachable waypoints.
            BuildIntermediatePath(destination);
        }

        _isMoving = true;
    }

    /// <summary>
    /// Freezes the player, preventing any movement input.
    /// </summary>
    public void Freeze()
    {
        _isFrozen = true;
        _isMoving = false;
        _pathQueue.Clear();
        _agent.isStopped = true;
    }

    // Advance to the next queued waypoint once the current one is reached.
    private void AdvancePath()
    {
        if (!_isMoving || _agent.pathPending)
            return;

        if (_agent.remainingDistance <= WaypointReachedDistance)
        {
            if (_pathQueue.Count > 0)
            {
                _agent.SetDestination(_pathQueue.Dequeue());
            }
            else
            {
                _isMoving = false;
            }
        }
    }

    // Finds intermediate waypoints that form a reachable chain to the destination.
    private void BuildIntermediatePath(Vector3 destination)
    {
        MiniGameWaypoint[] allWaypoints = FindObjectsByType<MiniGameWaypoint>(FindObjectsSortMode.None);

        // Find the waypoint closest to the destination that has a complete path from current position.
        Vector3 bestIntermediate = Vector3.zero;
        float bestScore = float.MaxValue;
        bool foundIntermediate = false;

        foreach (MiniGameWaypoint wp in allWaypoints)
        {
            Vector3 wpPos = wp.transform.position;
            NavMeshPath testPath = new NavMeshPath();
            _agent.CalculatePath(wpPos, testPath);

            if (testPath.status != NavMeshPathStatus.PathComplete)
                continue;

            // Score = distance from waypoint to the final destination (prefer waypoints closer to goal).
            float score = Vector3.Distance(wpPos, destination);
            if (score < bestScore)
            {
                bestScore = score;
                bestIntermediate = wpPos;
                foundIntermediate = true;
            }
        }

        if (foundIntermediate)
        {
            // Enqueue the final destination after the intermediate stop.
            _pathQueue.Enqueue(destination);
            _agent.SetDestination(bestIntermediate);
        }
        else
        {
            // No reachable waypoint at all — stay put.
            _isMoving = false;
            Debug.LogWarning("[MiniGamePlayerController] No reachable waypoint found for destination.");
        }
    }
}



