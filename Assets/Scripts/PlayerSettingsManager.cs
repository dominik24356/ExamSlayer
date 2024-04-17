using UnityEngine;

public class PlayerSettingsManager : MonoBehaviour
{
    public static PlayerSettingsManager Instance { get; private set; }

    public int TotalBullets { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerSettingsManager initialized and set to persist across scenes.");
            this.TotalBullets = 5;
        }
        else if (Instance != this)
        {
            Debug.Log("Duplicate PlayerSettingsManager instance found, destroying.");
            Destroy(gameObject);
        }
    }

    public void SetTotalBullets(int bullets)
    {
        TotalBullets = bullets;
        Debug.Log("Total Bullets Set to: " + TotalBullets);
    }
}
