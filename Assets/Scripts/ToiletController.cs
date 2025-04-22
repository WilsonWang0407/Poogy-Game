using UnityEngine;

public class ToiletController : MonoBehaviour {
    public float moveSpeed = 2f;
    private int direction = 1;
    private Rigidbody2D rb;

    public float leftBoundary = -3.3f;
    public float rightBoundary = 3.3f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector3 pos = transform.position;
        pos.x += direction * moveSpeed * Time.deltaTime;

        if (pos.x < leftBoundary || pos.x > rightBoundary) {
            direction *= -1;
            pos.x = Mathf.Clamp(pos.x, leftBoundary, rightBoundary);
        }

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Poop")) {
            Destroy(other.gameObject);
            ScoreManager.Instance.AddScore(1);
        }
    }
}