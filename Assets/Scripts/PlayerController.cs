using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5f;
    public float minX = -3.35f;
    public float maxX = 3.55f;

    private AudioSource oinkSource;

    public AudioClip oink;

    public GameObject poopPrefab;
    public Transform poopSpawnPoint;
    private float angleVelocity = 0f;

    void Start() {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        oinkSource = audioSources[0];
    }

    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveX, 0f);
        transform.position += move * moveSpeed * Time.deltaTime;

        float targetAngle = Mathf.Clamp(-moveX * 30f, -30f, 30f);
        float currentZAngle = transform.eulerAngles.z;
        float smoothAngle = Mathf.SmoothDampAngle(currentZAngle, targetAngle, ref angleVelocity, 0.01f);
        transform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(poopPrefab, poopSpawnPoint.position, Quaternion.identity);
            oinkSource.PlayOneShot(oink);
        }

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Math.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
    }
}
