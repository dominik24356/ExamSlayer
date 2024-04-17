using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D _rigidbody;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public Pencil pencilPrefab;
    public TextMeshProUGUI buffInfo;
    AudioManager audioManager;

    public int bulletsLeft = 10;
    public int totalBullets = 10;

    public bool isReloading = false;
    public bool isInvincible = false;

    public bool isSpeedBoostActive = false;

    public ParticleSystem boostEffect;


    private PlayerEffects playerEffects;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerEffects = GetComponent<PlayerEffects>(); 
    }

    private void Update()
    {
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bulletsLeft < totalBullets && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }

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

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!isSpeedBoostActive)
            {
                StartCoroutine(ApplySpeedBoost());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !FindObjectOfType<PauseMenuManager>().isGamePausedChecker())
        {
            if (bulletsLeft > 0 && !isReloading)
            {
                Shoot();
                bulletsLeft--;
                if (bulletsLeft == 0)
                {
                    StartCoroutine(Reload());
                }
            }
        }

        FindObjectOfType<GameManager>().updateBulletsText(bulletsLeft);
    }

    public IEnumerator ApplySpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            // Ustaw pozycjê efektu boosta na pozycjê postaci gracza
            //boostEffect.transform.position = transform.position;
            boostEffect.Play();

            audioManager.PlaySFX(audioManager.speed_boost);

            isSpeedBoostActive = true;
            thrustSpeed *= 2;
            turnSpeed *= 2;

            yield return new WaitForSeconds(3.0f);

            // Dezaktywuj efekt i przywróæ pierwotn¹ prêdkoœæ po 3 sekundach
            boostEffect.Stop();
            boostEffect.Clear();
            thrustSpeed /= 2;
            turnSpeed /= 2;

            yield return new WaitForSeconds(9.0f);

            isSpeedBoostActive = false; // Ustaw zmienn¹ na false po zakoñczeniu ca³ego cyklu boosta
        }
    }

    private IEnumerator Reload()
    {
        audioManager.PlaySFX(audioManager.reload_start);
        this.isReloading = true;
        yield return new WaitForSeconds(2.0f);
        this.bulletsLeft = this.totalBullets;
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

        // Aktualizuj pozycjê efektu boosta prêdkoœci na pozycjê gracza
        if (isSpeedBoostActive)
        {
            boostEffect.transform.position = transform.position;
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
        if (collision.gameObject.tag == "Book" && !isInvincible)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Box")
        {
            int buffChoice = Random.Range(0, 3);
            if (buffChoice == 0)
            {
                StartCoroutine(BlinkText(0.25f, 6, "Double Speed!"));
                StartCoroutine(DoubleThrustSpeed());
            }
            else if (buffChoice == 1)
            {
                StartCoroutine(BlinkText(0.25f, 6, "Book Protection!"));
                StartCoroutine(Invincibility());
            }
            else if (buffChoice == 2)
            {
                StartCoroutine(BlinkText(0.25f, 6, "New magazine achieved!"));
                StartCoroutine(NewMagazineUpdate());
            }

            audioManager.PlaySFX(audioManager.buff);
        }
    }

    private IEnumerator NewMagazineUpdate()
    {
        this.totalBullets += 3;
        yield return new WaitForSeconds(0);

    }


    private IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(10);  
        isInvincible = false;
        playerEffects.DeactivateGlowEffect();
    }

    private IEnumerator BlinkText(float interval, int blinks, string buffText)
    {
        playerEffects.ActivateGlowEffect();
        buffInfo.gameObject.SetActive(true);
        for (int i = 0; i < blinks; i++)
        {
            buffInfo.text = buffText;
            yield return new WaitForSeconds(interval);
            buffInfo.text = "";
            yield return new WaitForSeconds(interval);
        }
        buffInfo.gameObject.SetActive(false);
    }

    public IEnumerator DoubleThrustSpeed()
    {
        thrustSpeed *= 2;
        turnSpeed *= 2;

        yield return new WaitForSeconds(10);

        thrustSpeed /= 2;
        turnSpeed /= 2;

        playerEffects.DeactivateGlowEffect(); 
    }

}
