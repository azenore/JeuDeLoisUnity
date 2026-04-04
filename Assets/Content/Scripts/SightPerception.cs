using UnityEngine;

public class SightPerception : MonoBehaviour
{
    public bool isDetected = false;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private GameObject detectionObject;
    private Vector3 targetDirection;
    private void Update()
    {
        targetDirection = detectionObject.transform.position - transform.position;
      if  (Vector3.Dot(lhs:transform.forward, rhs:Vector3.Normalize(targetDirection)) > 0 )
        {
            RaycastHit hit;
            if (Physics.Raycast(origin: transform.position, targetDirection, out hit, detectionRadius))
            {
                if (hit.collider.gameObject == detectionObject)
                {
                    isDetected = true;
                    return;
                }
               
            }
            
        }
      isDetected = false;
    }
}
