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
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreColissions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnColissions), this.respawnTime);

    }


    private void TurnOnColissions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()
    {
      
        FindObjectOfType<GameOverManager>().ShowGameOver();

        scoreText.text = "Score: " + this.score.ToString();
        livesText.text = "Lives: " + this.lives.ToString();
        this.SaveScore();
    }

    public void SaveScore()
    {
        int newScore = this.score;
        string nickname = PlayerPrefs.GetString("PlayerNickname", "Player");

        for (int i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.HasKey($"Score_{i}"))
            {
                int existingScore = PlayerPrefs.GetInt($"Score_{i}");

                if (newScore > existingScore)
                {
                    for (int j = 10; j > i; j--)
                    {
                        PlayerPrefs.SetInt($"Score_{j}", PlayerPrefs.GetInt($"Score_{j - 1}"));
                        PlayerPrefs.SetString($"Nickname_{j}", PlayerPrefs.GetString($"Nickname_{j - 1}"));
                    }

                    PlayerPrefs.SetInt($"Score_{i}", newScore);
                    PlayerPrefs.SetString($"Nickname_{i}", nickname);
                    break;
                }
            }
            else
            {
                PlayerPrefs.SetInt($"Score_{i}", newScore);
                PlayerPrefs.SetString($"Nickname_{i}", nickname);
                break;
            }
        }
    }


}
