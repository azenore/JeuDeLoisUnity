using UnityEngine;

public class MiniGamePlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerVisual;
    [SerializeField] private float _moveSpeed = 5f;

    private Vector3 _target;
    private bool _isMoving;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        if (_playerVisual != null)
            _target = _playerVisual.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryMoveToWaypoint();

        if (_isMoving)
        {
            _playerVisual.position = Vector3.MoveTowards(_playerVisual.position, _target, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(_playerVisual.position, _target) < 0.01f)
            {
                _playerVisual.position = _target;
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
    /// Slides the player visual to the given world position.
    /// </summary>
    public void MoveTo(Vector3 destination)
    {
        _target = destination;
        _isMoving = true;
    }
}

