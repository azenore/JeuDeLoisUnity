using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private GameObject _navpoint;
    [SerializeField] private float _attackDistance = 1.5f;

    private NavMeshAgent _agent;
    private SightPerception _sightPerception;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sightPerception = GetComponent<SightPerception>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _nextState = StateType.Patrol;
        ChangeState();
    }

    private void Update()
    {
        if (TestChangeState())
            ChangeState();

        BehaviorAction();
    }

 
    private bool TestChangeState()
    {
        switch (_state)
        {
            case StateType.Patrol:
                if (_sightPerception.isDetected)
                {
                    _nextState = Vector3.Distance(_target.transform.position, transform.position) <= _attackDistance
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
                if (Vector3.Distance(_target.transform.position, transform.position) <= _attackDistance)
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
                if (Vector3.Distance(_target.transform.position, transform.position) > _attackDistance)
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
                _agent.stoppingDistance = _attackDistance;  
                _agent.speed = 4f;
                break;

            case StateType.Catch:
                _agent.isStopped = true;
                _agent.SetDestination(transform.position);
                _animator.SetTrigger("Catch");
               
                break;
        }
    }


    private void BehaviorAction()
    {
        switch (_state)
        {
            case StateType.Patrol:
                _agent.SetDestination(_navpoint.transform.position);
                break;

            case StateType.Follow:
                _agent.SetDestination(_target.transform.position);
                break;
        }
    }
}
