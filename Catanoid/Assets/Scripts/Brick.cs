using System;
using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private AudioSource _brickHit;
    [SerializeField] private Shape _shapePrefab;
    public static event Action<Brick, int> OnBrickBroken;
    [SerializeField] [Range(1, 3)] private int _health = 1;
    [SerializeField] private MeshRenderer _shapeRenderer;
    [SerializeField] private Collider _shapeCollider;
    [SerializeField] private int _score;

    public void Hit(int damage, Vector3 position)
    {
        _health -= damage;
        _brickHit.Play();

        if (_health <= 0)
        {
            Destroy(_shapeCollider);
            Destroy(_shapeRenderer);

            StartCoroutine(Init(_shapeRenderer.material, position));

            OnBrickBroken?.Invoke(this, _score);
            
            return;
        }

        _shapeRenderer.material.color = SelectColor();
    }

    private Color SelectColor()
    {
        if (_health == 2)
        {
            return new Color(1f, 0.8f, 0.8f);
        }

        if (_health == 1)
        {
            return new Color(1f, 0.6f, 0.6f);
        }

        return Color.white;
    }

    private IEnumerator Init(Material material, Vector3 position)
    {
        Shape shape = Instantiate(_shapePrefab, transform.position, transform.rotation);

        shape.Initializing(material, position, transform.localScale);

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
