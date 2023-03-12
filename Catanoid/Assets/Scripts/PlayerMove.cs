using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;
    [SerializeField] private float _moveForce = 30.0f;
    [SerializeField] private float _maxXSpeed = 16.0f;

    private float _leftBorder = -12f;
    private float _rightBorder = 12f;

    private float _inputHorizontal = 0.0f;

    private void FixedUpdate()
    {
        // Движение: лево <-> право
        _inputHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_inputHorizontal) > 0.0f)
        {
            float force;
            if (Mathf.Abs(_rigidbody.velocity.x) < _maxXSpeed)
            {
                force = _inputHorizontal * _moveForce * Time.fixedDeltaTime;
            }
            else
            {
                force = 0.0f;
            }

            float distanceToLeftEdge = Vector3.Distance(_rigidbody.position, _leftEdge.position);
            float distanceToRightEdge = Vector3.Distance(_rigidbody.position, _rightEdge.position);

            Vector3 newPosition = _rigidbody.position;
            newPosition.x += force;

            bool notMove = false;
            if ((newPosition.x - distanceToLeftEdge) < _leftBorder)
            {
                notMove = true;
            }
            if ((newPosition.x + distanceToRightEdge) > _rightBorder)
            {
                notMove = true;
            }

            if ((force != 0.0f) && !notMove)
            {
                _rigidbody.MovePosition(newPosition);
            }
        }
    }
}
