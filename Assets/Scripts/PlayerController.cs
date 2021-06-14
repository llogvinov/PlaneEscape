using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float cpt = 0f;
    private int score = 0;

    public bool isGameOver;

    private Rigidbody2D playerRigidbody;
    private Camera mainCamera;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            SetScore();

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.forward * (-rotationSpeed) * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            playerRigidbody.AddRelativeForce(new Vector3(moveSpeed * Time.fixedDeltaTime, 0f, 0f));
        }
    }

    //camera follow
    private void LateUpdate()
    {
        if (!isGameOver)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.gameObject.transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Play();

            //restart game after 1.5 sec
            Invoke("ReloadScene", 1.5f);
        }
    }

    private void SetScore()
    {
        cpt += Time.deltaTime;
        if (cpt >= 0.5f)
        {
            cpt = 0f;
            score++;
            scoreText.text = score.ToString();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
