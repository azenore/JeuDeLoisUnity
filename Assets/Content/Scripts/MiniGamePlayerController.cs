using UnityEngine;

public class MiniGamePlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveSpeed = 5f;

    private Vector3 _target;
    private bool _isMoving;
    private bool _isFrozen;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        _target = transform.position;
    }

    private void Update()
    {
        if (_isFrozen)
            return;

        if (Input.GetMouseButtonDown(0))
            TryMoveToWaypoint();

        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target) < 0.01f)
            {
                transform.position = _target;
                _isMoving = false;
            }
        }
    }

    private void TryMoveToWaypoint()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out MiniGameWaypoint _))
            MoveTo(hit.transform.position);
    }

    /// <summary>
    /// Slides the player to the given world position.
    /// </summary>
    public void MoveTo(Vector3 destination)
    {
        if (_isFrozen)
            return;

        _target = destination;
        _isMoving = true;
    }

    /// <summary>
    /// Freezes the player, preventing any movement input.
    /// </summary>
    public void Freeze()
    {
        _isFrozen = true;
        _isMoving = false;
    }
}



