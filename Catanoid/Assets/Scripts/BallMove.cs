using System;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public static event Action BallBehindMap;

    [SerializeField] private AudioSource _ballHit;
    [SerializeField] private Rigidbody _rigidbody;

    // ТОЛЬКО ДЛЯ ТЕСТОВ!!! <<<===========================================
    /**/[SerializeField] [Range(0.0f, 1000.0f)] private float _startSpeed = 50.0f;
    
    private float _moveSpeed;
    private Vector3 _moveDirection;
    private Vector3 _previousPosition;
    private Vector3 _previousDirection;
    private int _damage = 1;

    private void Awake()
    {
        // ТОЛЬКО ДЛЯ ТЕСТОВ!!! <<<===========================================
        /**/_moveSpeed = _startSpeed;
        /**/_moveDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f));
        /**/ChangeDirection(_moveDirection);


        _previousPosition = transform.position;
        _previousDirection = _moveDirection;
    }

    private void FixedUpdate()
    {
        // Движение шара
        _rigidbody.velocity = _moveDirection * _moveSpeed;
    }

    private void LateUpdate()
    {
        // Фиксируем положение
        _previousPosition = transform.position;
        _previousDirection = _moveDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            BallBehindMap?.Invoke();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Arena Borders"))
        {
            _ballHit.Play();
        }

        // Шар меняет направление движения при коллизии
        ChangeDirection(Vector3.Reflect(_moveDirection, collision.contacts[0].normal));

        // Угол отражения
        float reflectionAngle = Vector3.Angle(_moveDirection, collision.contacts[0].normal);

        if (collision.collider.GetComponent<Brick>() is Brick brick)
        {
            brick.Hit(_damage, collision.contacts[0].point);
        }

        // Если угол отражения больше 90 градусов
        if (reflectionAngle > 90.0f)
        {
            // dotCross положительный, если нормаль коллизии справа
            // от _previousDirection, и отрицательный, если слева
            float dotCross = Vector3.Dot(_previousDirection, Vector3.Cross(collision.contacts[0].normal, Vector3.up));

            // Угол коррекции
            float correctionAngle = (reflectionAngle - 90f) * 2f;
            // Корректируем движение в сторону нормали коллизии
            DirectionCorrection(dotCross, correctionAngle);
        }
    }

    private void ChangeDirection(Vector3 direction, Vector3 upwards)
    {
        _previousDirection = _moveDirection;
        _moveDirection = direction.normalized;
        transform.rotation = Quaternion.LookRotation(_moveDirection, upwards);
    }

    private void ChangeDirection(Vector3 direction)
    {
        ChangeDirection(direction, Vector3.up);
    }

    private void DirectionCorrection(float dotCross, float angle)
    {
        // Откат к точке удара
        transform.position = Vector3.Lerp(_previousPosition, transform.position, 0.25f);

        // Если нормаль смотрит влево
        if (dotCross < 0)
        {
            angle *= -1;
        }

        ChangeDirection(Quaternion.Euler(0f, angle, 0f) * _previousDirection);
    }
}
