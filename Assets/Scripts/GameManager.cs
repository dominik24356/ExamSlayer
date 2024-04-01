using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public Player player;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public ParticleSystem explosion;

    public int score = 0;


    public void BookDestroyed(Book book)
    {
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
    }


    public void PlayerDied()
    {
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


        
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreColissions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnColissions), this.respawnTime);
    }

    
    private void TurnOnColissions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
