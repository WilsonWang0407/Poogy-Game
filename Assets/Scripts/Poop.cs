using UnityEngine;

public class Poop : MonoBehaviour {
    private float lifetime = 5f;
    private bool isSettled = false;
    private float dropTime;

    void Start() {
        dropTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (isSettled) return;
        if (Time.time - dropTime < 0.1f) return;

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Poop")) {
            isSettled = true;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            Invoke("Disappear", lifetime);
        }
    }

    void Disappear() {
        for (float yOffset = 0.3f; yOffset < 5f; yOffset += 0.25f) {
            Vector2 checkAbove = transform.position + Vector3.up * yOffset;
            Collider2D[] hits = Physics2D.OverlapCircleAll(checkAbove, 0.1f);

            foreach (var hit in hits) {
                if (hit.CompareTag("Poop")) {
                    Rigidbody2D rbAbove = hit.GetComponent<Rigidbody2D>();
                    if (rbAbove != null && rbAbove.bodyType == RigidbodyType2D.Kinematic) {
                        rbAbove.bodyType = RigidbodyType2D.Dynamic;
                        rbAbove.constraints = RigidbodyConstraints2D.FreezeRotation;
                        rbAbove.WakeUp();
                        hit.transform.position += new Vector3(0f, 0.01f, 0f);
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}