using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public Player player;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public Text scoreText;
    public Text livesText;
    public Text pencilsLeftText;
    public int score = 0;
    public ParticleSystem explosion;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void BookDestroyed(Book book)
    {
        audioManager.PlaySFX(audioManager.destruction);
        this.explosion.transform.position = book.transform.position;
        this.explosion.Play();

        if(book.size <= 0.6f)
        {
            score += 100;
        } else if(book.size <= 0.7f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

        scoreText.text = "Score: " + this.score.ToString();
        livesText.text = "Lives: " + this.lives.ToString();
    }


    public void PlayerDied()
    {
        audioManager.PlaySFX(audioManager.death);
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

        if(lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }

        scoreText.text = "Score: " + this.score.ToString();
        livesText.text = "Lives: " + this.lives.ToString();
        pencilsLeftText.text = "Pencils: " + player.bulletsLeft.ToString();

    }

    public void setLivesAndScore()
    {
        this.lives = 3;
        this.score = 0;
    }

    public void Respawn()
    {
        audioManager.PlaySFX(audioManager.respawn);
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        this.player.enabled = true; // Enable the Player script

        // Reset bullets count and reloading state
        this.player.bulletsLeft = 3;
        this.player.isReloading = false;

        Invoke(nameof(TurnOnCollisions), this.respawnTime);
    }



    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()
    {
      
        FindObjectOfType<GameOverManager>().ShowGameOver();

        scoreText.text = "Score: " + this.score.ToString();
        livesText.text = "Lives: " + this.lives.ToString();
        pencilsLeftText.text = "Pencils: " + player.bulletsLeft.ToString();

    }


    public void updateBulletsText(int bulletsLeft)
    {
        pencilsLeftText.text = "Pencils: " + bulletsLeft.ToString();
    }
}
