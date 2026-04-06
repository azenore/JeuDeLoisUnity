using UnityEngine;

public class SightPerception : MonoBehaviour
{
    public bool isDetected = false;

    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float eyeHeight = 1.6f;
    [SerializeField] private GameObject detectionObject;

    private void Update()
    {
        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;
        Vector3 targetCenter = detectionObject.transform.position + Vector3.up * 0.9f;
        Vector3 targetDirection = targetCenter - eyePosition;

        if (Vector3.Dot(transform.forward, targetDirection.normalized) > 0f)
        {
            if (Physics.Raycast(eyePosition, targetDirection, out RaycastHit hit, detectionRadius))
            {
                if (hit.collider.gameObject == detectionObject
                    || hit.collider.transform.IsChildOf(detectionObject.transform))
                {
                    isDetected = true;
                    return;
                }
            }
        }

        isDetected = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;
        Gizmos.color = isDetected ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(eyePosition, detectionRadius);
        if (detectionObject != null)
            Gizmos.DrawLine(eyePosition, detectionObject.transform.position + Vector3.up * 0.9f);
    }
}
