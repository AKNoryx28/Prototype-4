using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private
    [SerializeField] private float speed = 3.5f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupSrenght = 15.0f;

    private string VERTICAL = "Vertical",
        HORIZONTAL = "Horizontal",
        POWERUP_TAG = "Powerup",
        ENEMY_TAG = "Enemy",
        FOCAL_POINT = "Focal point";

    // Public
    public bool hasPoweup = false;
    public GameObject powerupIndicator;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find(FOCAL_POINT);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw(VERTICAL);
        float horizontalInput = Input.GetAxisRaw(HORIZONTAL);

        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(POWERUP_TAG))
        {
            hasPoweup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPoweup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG) && hasPoweup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            // Debug.Log("Collided with: " + collision.gameObject.name + "\nHas Powerup: " + hasPoweup);

            enemyRb.AddForce(awayFromPlayer * powerupSrenght, ForceMode.Impulse);
        }
    }
}
