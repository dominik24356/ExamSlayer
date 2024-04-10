using UnityEngine;

public class SpecialBoxSpawner : MonoBehaviour
{
    public float spawnRate = 15.0f;
    public float spawnDistance = 15.0f;
    public SpecialBox specialBoxPrefab;
    public float trajectoryVariance = 15.0f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
        Vector3 spawnPoint = transform.position + spawnDirection;

        float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        SpecialBox box = Instantiate(specialBoxPrefab, spawnPoint, rotation);
        box.size = 0.5f;
 
        box.SetTrajectory(rotation * -spawnDirection);
        box.speed += 10.0f;

    }


}
