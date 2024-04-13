using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public Material normalMaterial;
    public Material glowMaterial;

    public void ActivateGlowEffect()
    {
        GetComponent<SpriteRenderer>().material = glowMaterial;
    }

    public void DeactivateGlowEffect()
    {
        GetComponent<SpriteRenderer>().material = normalMaterial;
    }
}
