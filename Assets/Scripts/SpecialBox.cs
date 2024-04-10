using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    public float powerDuration = 5.0f;
    public float speed = 10.0f;
    public float maxLifetime = 30.0f;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    public float size = 0.5f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerNotLayer")
        {
            Destroy(this.gameObject);
        }
    }



}
