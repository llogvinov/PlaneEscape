using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 350f;
    [SerializeField] private float _rotationSpeed = 1f;

    [SerializeField] private Transform _player;

    private Rigidbody2D _enemyRigidbody;

    private void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RotateEnemy();
    }

    private void FixedUpdate()
    {
        _enemyRigidbody.AddRelativeForce(new Vector3(_moveSpeed * Time.fixedDeltaTime, 0f, 0f));
    }

    private void RotateEnemy()
    {
        Vector2 direction = _player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion r = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, r, _rotationSpeed * Time.deltaTime);
    }

}
