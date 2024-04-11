using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D _rigidbody;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public Pencil pencilPrefab;
    AudioManager audioManager;

    public int bulletsLeft = 10;
    public bool isReloading = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !FindObjectOfType<PauseMenuManager>().isGamePausedChecker())
        {
            if (bulletsLeft > 0 && !isReloading)
            {
                Shoot();
                this.bulletsLeft--;
                if (bulletsLeft == 0)
                {
                    StartCoroutine(Reload());
                }
            }


            
        }

        FindObjectOfType<GameManager>().updateBulletsText(this.bulletsLeft);
    }

    private IEnumerator Reload()
    {
        
        this.isReloading = true;
        yield return new WaitForSeconds(2.0f);
        this.bulletsLeft = 10;
        this.isReloading = false;
        audioManager.PlaySFX(audioManager.reload);
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        audioManager.PlaySFX(audioManager.shoot);
        Pencil pencil = Instantiate(this.pencilPrefab, this.transform.position, this.transform.rotation);
        pencil.Project(this.transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Book")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            // slow very costly to use function
            FindObjectOfType<GameManager>().PlayerDied();
        }
      
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Box" && thrustSpeed == 1.0f)
        {
            StartCoroutine(this.doubleThrustSpeed());
            audioManager.PlaySFX(audioManager.buff);
        }
    }


    public IEnumerator doubleThrustSpeed()
    {
        this.thrustSpeed = thrustSpeed * 2;
        this.turnSpeed = turnSpeed * 2;

        yield return new WaitForSeconds(5);

        // Reset power effect here, e.g., halve the thrust speed
        this.thrustSpeed /= 2;
        this.turnSpeed /= 2;
    }

}
