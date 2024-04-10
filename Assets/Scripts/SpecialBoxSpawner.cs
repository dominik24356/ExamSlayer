using UnityEngine;

public class SpecialBoxSpawner : MonoBehaviour
{
    public float spawnRate = 5.0f;
    public int spawnDistance = 10;
    public SpecialBox specialBoxPrefab;
    public float trajectoryVariance = 15.0f;
    public float speedIncreaseAmount = 1.0f;
    private float speedIncreaseStep = 0.05f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPoint = transform.position + spawnDirection;

        float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        SpecialBox box = Instantiate(specialBoxPrefab, spawnPoint, rotation);
        box.SetTrajectory(rotation * -spawnDirection.normalized);
        box.speed += speedIncreaseAmount;

        speedIncreaseAmount += speedIncreaseStep;
    }
}
