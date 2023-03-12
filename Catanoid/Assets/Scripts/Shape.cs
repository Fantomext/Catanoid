using System.Collections;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private MeshRenderer[] _renderers;
    private Collider[] _colliders;
    private Collider _ball;

    private void Start()
    {
        
        Destroy(gameObject, 1f);
    }

    public void Initializing(Material material, Vector3 position, Vector3 scale)
    {
        transform.localScale = scale;

        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _renderers = GetComponentsInChildren<MeshRenderer>();
        _colliders = GetComponentsInChildren<Collider>();

        Ignore();

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _renderers[i].material = material;
            _rigidbodies[i].AddExplosionForce(50f, position, 10f, 0.5f, ForceMode.VelocityChange);
        }
    }

    private void Ignore()
    {
        _ball = FindObjectOfType<BallMove>().GetComponent<Collider>();
        for (int i = 0; i < _colliders.Length; i++)
        {
            Physics.IgnoreCollision(_colliders[i], _ball);
        }
    }
}
