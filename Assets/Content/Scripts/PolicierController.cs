using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum StateType
{
    None,
    Patrol,
    Follow,
    Catch
}
public class PolicierController : MonoBehaviour
{
    [SerializeField] private StateType state = StateType.None;
    [SerializeField] private StateType nextState = StateType.None;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject navpoint;
    [SerializeField] private float attackDistance = 1.5f;
    private void Update()
    {
        if (TestChangeState())
        {
            ChangeState();
        }
        BehaviorAction();
    }

    private bool TestChangeState()
    {
        switch (state)
        {
            case StateType.Patrol:
                if (GetComponent<SightPerception>().isDetected)
                {
                    if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance)
                    {
                        nextState = StateType.Catch;
                        return true;
                    }
                    else
                    {
                        nextState = StateType.Follow;
                        return true;
                    }
                }

                break;


            case StateType.Catch:
                if(!GetComponent<SightPerception>().isDetected)
                {
                    nextState = StateType.Patrol;
                    return true;
                }

                if (Vector3.Distance(target.transform.position, transform.position)> attackDistance)
                {
                    nextState = StateType.Follow;
                    return true;
                }
                break;


            case StateType.Follow:
                if (!GetComponent<SightPerception>().isDetected)
                {
                    nextState = StateType.Patrol;
                    return true;
                }


                if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance)
                {
                    nextState = StateType.Catch;
                    return true;
                }
                break;
        }
        return false;

    }

    private void ChangeState()
    {
        EndState();
        state = nextState;
        StartState();
    }

    private void EndState()
    { 
        switch (state)
        {
            case StateType.Follow:
                GetComponent<NavMeshAgent>().SetDestination(transform.position);   
                break;
                case StateType.Patrol:
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
                break;

        }
    }

    private void StartState()
    {
        switch (state)
        {
            case StateType.Patrol:
                GetComponent<NavMeshAgent>().speed = 2f;
                break;

            case StateType.Follow:
                GetComponent<NavMeshAgent>().speed = 4f;
                break;
        }
    }
    private void BehaviorAction()
    {
        switch (state)
        {
            case StateType.Patrol:
                PatrolBehaviour();
                break;
            case StateType.Follow:
                FollowBehaviour();
                break;
            case StateType.Catch:
                CatchBehaviour();
                break;
         
        }
    }
   private void PatrolBehaviour()
    {
        GetComponent<NavMeshAgent>().SetDestination(navpoint.transform.position);
    }
    private void FollowBehaviour()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }
    private void CatchBehaviour()
    {
        GetComponent<Animator>().SetTrigger(name:"Catch");
    }
}
