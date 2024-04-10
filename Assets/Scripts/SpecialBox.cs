using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    public float powerDuration = 5.0f;
    public float speed = 3.0f;
    public float maxLifetime = 30.0f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Set initial movement and lifetime
        SetTrajectory(Random.insideUnitCircle.normalized);
        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerNotLayer")
        {
            Destroy(this.gameObject);
        }
    }


}
