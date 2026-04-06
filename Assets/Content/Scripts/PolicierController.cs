using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum StateType
{
    None,
    Patrol,
    Follow,
    Catch
}

public class PolicierController : MonoBehaviour
{
    [SerializeField] private StateType _state = StateType.None;
    [SerializeField] private StateType _nextState = StateType.None;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _patrolSpeed = 4f;
    [SerializeField] private float _followSpeed = 6f;
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private float _navpointReachedDistance = 0.5f;

    /// <summary>
    /// How many of the closest waypoints to consider when picking the next patrol destination.
    /// A lower value keeps the policier moving locally; a higher value allows longer jumps.
    /// </summary>
    [SerializeField] private int _nearbyWaypointCandidates = 4;

    public UnityEvent OnPlayerCaught;

    private NavMeshAgent _agent;
    private SightPerception _sightPerception;
    private Animator _animator;

    private MiniGameWaypoint[] _allWaypoints;
    private int _currentWaypointIndex = 0;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sightPerception = GetComponent<SightPerception>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Auto-discover all waypoints — no manual Inspector assignment needed.
        _allWaypoints = FindObjectsByType<MiniGameWaypoint>(FindObjectsSortMode.None);

        _currentWaypointIndex = Random.Range(0, _allWaypoints.Length);
        _nextState = StateType.Patrol;
        ChangeState();
    }

    private void Update()
    {
        if (TestChangeState())
            ChangeState();

        BehaviorAction();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Blend", _agent.velocity.magnitude);
    }

    private bool TestChangeState()
    {
        switch (_state)
        {
            case StateType.Patrol:
                if (_sightPerception.isDetected)
                {
                    _nextState = HorizontalDistance(_target.transform.position, transform.position) <= _attackDistance
                        ? StateType.Catch
                        : StateType.Follow;
                    return true;
                }
                break;

            case StateType.Follow:
                if (!_sightPerception.isDetected)
                {
                    _nextState = StateType.Patrol;
                    return true;
                }
                if (HorizontalDistance(_target.transform.position, transform.position) <= _attackDistance)
                {
                    _nextState = StateType.Catch;
                    return true;
                }
                break;

            case StateType.Catch:
                if (!_sightPerception.isDetected)
                {
                    _nextState = StateType.Patrol;
                    return true;
                }
                if (HorizontalDistance(_target.transform.position, transform.position) > _attackDistance)
                {
                    _nextState = StateType.Follow;
                    return true;
                }
                break;
        }
        return false;
    }

    private void ChangeState()
    {
        EndState();
        _state = _nextState;
        StartState();
    }

    private void EndState()
    {
        switch (_state)
        {
            case StateType.Patrol:
            case StateType.Follow:
                _agent.SetDestination(transform.position);
                break;
        }
    }

    private void StartState()
    {
        switch (_state)
        {
            case StateType.Patrol:
                _agent.isStopped = false;
                _agent.stoppingDistance = 0f;
                _agent.speed = _patrolSpeed;
                break;

            case StateType.Follow:
                _agent.isStopped = false;
                _agent.stoppingDistance = 0f;
                _agent.speed = _followSpeed;
                break;

            case StateType.Catch:
                _agent.isStopped = true;
                _agent.SetDestination(transform.position);
                _animator.SetTrigger("Catch");
                OnPlayerCaught?.Invoke();
                break;
        }
    }

    private void BehaviorAction()
    {
        switch (_state)
        {
            case StateType.Patrol:
                if (_allWaypoints.Length == 0)
                    break;

                _agent.SetDestination(_allWaypoints[_currentWaypointIndex].transform.position);

                if (!_agent.pathPending && _agent.remainingDistance <= _navpointReachedDistance)
                    PickNearbyRandomWaypoint();
                break;

            case StateType.Follow:
                _agent.SetDestination(_target.transform.position);
                break;
        }
    }

    // Picks a random waypoint from the N closest to the current position,
    // excluding the waypoint just visited to avoid immediate back-tracking.
    private void PickNearbyRandomWaypoint()
    {
        if (_allWaypoints.Length <= 1)
            return;

        Vector3 currentPos = transform.position;

        // Sort all waypoints by distance, skip the one we're standing on.
        List<(int index, float distance)> candidates = new();
        for (int i = 0; i < _allWaypoints.Length; i++)
        {
            if (i == _currentWaypointIndex)
                continue;

            float dist = HorizontalDistance(_allWaypoints[i].transform.position, currentPos);
            candidates.Add((i, dist));
        }

        candidates.Sort((a, b) => a.distance.CompareTo(b.distance));

        // Pick randomly among the N nearest candidates.
        int poolSize = Mathf.Min(_nearbyWaypointCandidates, candidates.Count);
        _currentWaypointIndex = candidates[Random.Range(0, poolSize)].index;
    }

    private float HorizontalDistance(Vector3 a, Vector3 b)
    {
        a.y = 0f;
        b.y = 0f;
        return Vector3.Distance(a, b);
    }
}
