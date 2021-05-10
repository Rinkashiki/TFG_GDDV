using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private AudioInput audioInput;
    private AmplitudeReact ampReact;

    // Bullet Variables
    private Vector3 bulletDirection;
    private float bulletDamage = 1f;
    private float bulletSpeed = 1.5f;
    private float amplitudeFactor = 0.025f;
    private float speedOffset;

    // Start is called before the first frame update
    void Start()
    {
        audioInput = GameObject.FindGameObjectWithTag("AudioInput").GetComponent<AudioInput>();
        ampReact = GetComponent<AmplitudeReact>();
        ampReact.audioInput = audioInput;

        StartCoroutine(SetInactiveDelayed());
    }

    // Update is called once per frame
    void Update()
    {
        // Bullet movement
        transform.position += bulletDirection * (bulletSpeed * Time.deltaTime + audioInput.GetAmplitudeBuffer() * amplitudeFactor + speedOffset);

        // Bullet Scaling 
        ampReact.AmplitudeScale(gameObject, Vector3.one, 0.15f, 0.3f);
    }

    public void SetDirection(Vector3 bulletDirection)
    {
        this.bulletDirection = bulletDirection;
    }

    public void SetSpeedOffset(float speedOffset)
    {
        this.speedOffset = speedOffset;
    }

    private IEnumerator SetInactiveDelayed()
    {
        yield return new WaitForSeconds(5);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerShip>().TakeDamage(bulletDamage);
        }
    }
}
