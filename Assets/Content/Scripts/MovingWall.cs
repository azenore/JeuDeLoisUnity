using UnityEngine;

/// <summary>Moves the GameObject back and forth between two offsets along a single axis, relative to its starting position.</summary>
public class MovingWall : MonoBehaviour
{
    public enum Axis { X, Y, Z }

    [SerializeField] private Axis _axis = Axis.X;
    [SerializeField] private int _offsetA = 0;
    [SerializeField] private int _offsetB = 5;
    [SerializeField] private float _speed = 3f;

    private Vector3 _pointA;
    private Vector3 _pointB;
    private Vector3 _target;

    private void Start()
    {
        Vector3 origin = transform.position;

        _pointA = origin + GetAxisVector() * _offsetA;
        _pointB = origin + GetAxisVector() * _offsetB;
        _target = _pointB;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target) < 0.01f)
            _target = _target == _pointA ? _pointB : _pointA;
    }

    /// <summary>Returns the unit vector corresponding to the selected axis.</summary>
    private Vector3 GetAxisVector()
    {
        return _axis switch
        {
            Axis.X => Vector3.right,
            Axis.Y => Vector3.up,
            Axis.Z => Vector3.forward,
            _ => Vector3.right
        };
    }
}
