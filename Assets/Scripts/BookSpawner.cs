using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawner : MonoBehaviour
{
    public float spawnRate = 5.0f;
    public float trajectoryVariance = 15.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;
    public Book bookPrefab;

    private float speedIncreaseStep = 0.05f; 
    public float speedIncreaseAmount = 1.0f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Book book = Instantiate(this.bookPrefab, spawnPoint, rotation);
            book.size = Random.Range(book.minSize, book.maxSize);
            book.SetTrajectory(rotation * -spawnDirection);

            book.speed += speedIncreaseAmount;
        }

        speedIncreaseAmount += speedIncreaseStep;
    }
}
