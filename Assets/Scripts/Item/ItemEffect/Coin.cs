using UnityEngine;
using UnityEngine.UI.Extensions.CasualGame;

public class Coin : MonoBehaviour
{
    public float rotationSpeed;
    public UiManager uiManager;
    private Player player;
    private float magnetSpeed = 40f;
    private bool startMagnet = false;
    private bool isCollected = false;
    public ParticleSystem coinParticle;

    private readonly float obstacleDeleteTime = 1f;
    private float obstacleDeleteCurrentTime = 0f;

    private bool endObstacleDelete = false;

    private void Start()
    {
        rotationSpeed = 60f;
        endObstacleDelete = false;
        obstacleDeleteCurrentTime = 0f;
        uiManager = FindObjectOfType<UiManager>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (startMagnet && !isCollected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, magnetSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player2 = other.gameObject.GetComponent<Player>();
        if (player2 && !isCollected)
        {
            isCollected = true;
            AudioSource getCoinSound = GetComponent<AudioSource>();
            getCoinSound.Play();
            var particle = Instantiate(coinParticle, transform.position, transform.rotation);
            particle.Play();
            Destroy(particle.gameObject, 2f);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, getCoinSound.clip.length);

            if (player.coinDouble.gameObject.activeSelf)
            {
                uiManager.AddCoin(2);
            }
            else
            {
                uiManager.AddCoin(1);
            }
        }

        obstacleDeleteCurrentTime += Time.deltaTime;
        if (obstacleDeleteCurrentTime > obstacleDeleteTime)
        {
            endObstacleDelete = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var trap = other.gameObject.GetComponent<Trap>();
        if (trap && !endObstacleDelete)
        {
            Destroy(trap.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        var trap = collision.gameObject.GetComponent<Trap>();
        if (trap && !endObstacleDelete)
        {
            Destroy(trap.gameObject);
        }
    }
    public void GetMagnet()
    {
        startMagnet = true;
    }
    private void OnTransformParentChanged()
    {
        if (transform.parent != null && !transform.parent.gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }
}