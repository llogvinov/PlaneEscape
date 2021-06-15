using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    [SerializeField] private float _moveSpeed = 300f;
    [SerializeField] private float _rotationSpeed = 100f;
    
    public bool IsGameOver;

    private float _cpt = 0f;
    private int _score = 0;

    private Rigidbody2D _playerRigidbody;
    private Camera _mainCamera;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsGameOver)
        {
            SetScore();

            if (Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(Vector3.forward * (-_rotationSpeed) * Time.deltaTime);
            else if (Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!IsGameOver)
            _playerRigidbody.AddRelativeForce(new Vector3(_moveSpeed * Time.fixedDeltaTime, 0f, 0f));
    }

    //camera follow
    private void LateUpdate()
    {
        if (!IsGameOver)
            _mainCamera.transform.position = new Vector3(
                transform.position.x, 
                transform.position.y, 
                _mainCamera.gameObject.transform.position.z
            );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsGameOver)
        {
            IsGameOver = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Play();

            //restart game after 1.5 sec
            Invoke("ReloadScene", 1.5f);
        }
    }

    private void SetScore()
    {
        _cpt += Time.deltaTime;
        if (_cpt >= 0.5f)
        {
            _cpt = 0f;
            _score++;
            _scoreText.text = _score.ToString();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
