using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    #region PlayerShip_Variables

    [SerializeField] private Transform target;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Material flickMat;

    private Rigidbody2D body;
    private Material mat;

    // For taking damage
    private bool invencibility;
    private float invencibilityTime = 1;
    private float invencibilityStep;

    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // For ship rotation
        LookAt();

        // For ship movement
        float Y = Input.GetAxis("Vertical");
        float X = Input.GetAxis("Horizontal");

        Vector2 dir = new Vector2(X, Y);
        body.velocity += dir * movementSpeed * Time.deltaTime;

        // For particles
        if (!particles.isPlaying && dir.magnitude > 0)
        {
            particles.Play();
        }

        if (dir.magnitude <= 0)
        {
            particles.Stop();
        }
       
        // For Flickering
        if (invencibility)
        {
            gameObject.GetComponent<MeshRenderer>().material = flickMat;
            Flickering();
        }
    }

    private void LookAt()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(float bulletDamage)
    {
        if (!invencibility)
        {
            GameManager.Instance.SetHealth(bulletDamage);
        }
        invencibility = true; 
    }

    private void Flickering()
    {
        float currentTime = Mathf.Round(Time.time * 10);
        bool timeStep = currentTime % 2 == 0;
        float alpha;

        if (timeStep)
        {
            alpha = 0;
        }
        else
        {
            alpha = 1;
        }

        invencibilityStep += Time.deltaTime;

        if (invencibilityStep >= invencibilityTime)
        {
            gameObject.GetComponent<MeshRenderer>().material = mat;
            invencibilityStep = 0;
            invencibility = false;
            alpha = 1;
        }

        flickMat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
    }
}
