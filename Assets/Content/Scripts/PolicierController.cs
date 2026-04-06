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
    [SerializeField] private GameObject[] _navpoints;
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private float _navpointReachedDistance = 0.5f;

    public UnityEvent OnPlayerCaught;

    private NavMeshAgent _agent;
    private SightPerception _sightPerception;
    private Animator _animator;
    private int _currentNavpointIndex = 0;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sightPerception = GetComponent<SightPerception>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentNavpointIndex = Random.Range(0, _navpoints.Length);
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
                _agent.speed = 2f;
                break;

            case StateType.Follow:
                _agent.isStopped = false;
                _agent.stoppingDistance = 0f;
                _agent.speed = 4f;
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
                if (_navpoints.Length == 0)
                    break;

                _agent.SetDestination(_navpoints[_currentNavpointIndex].transform.position);

                if (!_agent.pathPending && _agent.remainingDistance <= _navpointReachedDistance)
                    PickRandomNavpoint();
                break;
            case StateType.Follow:
                _agent.SetDestination(_target.transform.position);
                break;
        }
    }

    private float HorizontalDistance(Vector3 a, Vector3 b)
    {
        a.y = 0f;
        b.y = 0f;
        return Vector3.Distance(a, b);
    }
    private void PickRandomNavpoint()
    {
        if (_navpoints.Length <= 1)
            return;

        int next;
        do
        {
            next = Random.Range(0, _navpoints.Length);
        }
        while (next == _currentNavpointIndex);

        _currentNavpointIndex = next;
    }}
