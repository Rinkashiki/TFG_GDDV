using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    private AmplitudeReact ampReact;

    // Enemy Post-Processing
    [Header("Post-Processing Variables")]
    [SerializeField] Volume vol;
    [SerializeField] float bloomFactor;
    [SerializeField] float chromAbFactor;

    private Bloom bloom;
    private ChromaticAberration ca;

    // Enemy Bright
    private Color initialColor;

    // Bullet Spawn
    [Header("Spawn Variables")]
    [SerializeField] float patternTime;
    [Header("Spawn Spiral Variables")]
    [SerializeField] int waveSizeSpiral;
    [SerializeField] float spawnRatioSpiral;
    [SerializeField] float angleOffset;
    [Header("Spawn Circle Variables")]
    [SerializeField] int waveSizeCircle;
    [SerializeField] float spawnRatioCircle;
    [Header(" Spawn Flower Variables")]
    [SerializeField] int waveSizeFlower;
    [SerializeField] float spawnRatioFlower;
    [SerializeField] int maxCorners;

    private BulletPool bulletPool;
    private Object bullet;
    private float elapsedTime;

    // Bezier Follow
    private BezierFollow bezier;

    #region Scene_Grid

    [Header("Scene Grid")]
    [SerializeField] Color lineColor;
    [SerializeField] float drawSpeedFactor;
    private float verticalGridGap = 20.0f / 9;
    private float horizontalGridGap = 12.0f / 9;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        ampReact = GetComponent<AmplitudeReact>();

        // Enemy Post-Processing
        bloom = (Bloom)vol.profile.components[0];
        ca = (ChromaticAberration)vol.profile.components[1];

        // Enemy Bright (Control with MIDI Play)
        initialColor = gameObject.GetComponent<MeshRenderer>().material.color;

        // Bullet Spawn
        bulletPool = GetComponent<BulletPool>();
        StartCoroutine(BulletSpawnSpiral());

        // Bezier Follow
        bezier = GetComponent<BezierFollow>();
        bezier.enabled = false;

        #region Scene_Grid

        // Verticals
        Vector3[] vertical0 = { new Vector3(-3.5f * verticalGridGap, -5), new Vector3(-3.5f * verticalGridGap, 7) };
        Vector3[] vertical1 = { new Vector3(-2.5f * verticalGridGap, 7), new Vector3(-2.5f * verticalGridGap, -5) };
        Vector3[] vertical2 = { new Vector3(-1.5f * verticalGridGap, -5), new Vector3(-1.5f * verticalGridGap, 7) };
        Vector3[] vertical3 = { new Vector3(-0.5f  * verticalGridGap, 7), new Vector3(-0.5f * verticalGridGap, -5) };
        Vector3[] vertical4 = { new Vector3(0.5f * verticalGridGap, -5), new Vector3(0.5f * verticalGridGap, 7) };
        Vector3[] vertical5 = { new Vector3(1.5f * verticalGridGap, 7), new Vector3(1.5f * verticalGridGap, -5), };
        Vector3[] vertical6 = { new Vector3(2.5f * verticalGridGap, -5), new Vector3(2.5f * verticalGridGap, 7) };
        Vector3[] vertical7 = { new Vector3(3.5f * verticalGridGap, 7), new Vector3(3.5f * verticalGridGap, -5) };

        // Horizontals
        Vector3[] horizontal0 = { new Vector3(-11, -4), new Vector3(11, -4) };
        Vector3[] horizontal1 = { new Vector3(11, -2), new Vector3(-11, -2) };
        Vector3[] horizontal2 = { new Vector3(-11, 0), new Vector3(11, 0) };
        Vector3[] horizontal3 = { new Vector3(11, 2), new Vector3(-11, 2) };
        Vector3[] horizontal4 = { new Vector3(-11, 4), new Vector3(11, 4) };
        Vector3[] horizontal5 = { new Vector3(11, 6), new Vector3(-11, 6) };

        //Verticals
        ampReact.AmplitudeDrawPolygon(vertical0, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(vertical1, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(vertical2, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(vertical3, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(vertical4, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(vertical5, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(vertical6, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(vertical7, lineColor, 0.05f, drawSpeedFactor);

        // Horizontals
        ampReact.AmplitudeDrawPolygon(horizontal0, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(horizontal1, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(horizontal2, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(horizontal3, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(horizontal4, lineColor, 0.05f, drawSpeedFactor);
        ampReact.AmplitudeDrawPolygon(horizontal5, lineColor, 0.05f, drawSpeedFactor);

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy Scaling 
        ampReact.AmplitudeScale(gameObject.transform, Vector3.one, 0.3f, Vector3.one);

        // Enemy Post-Processing
        ampReact.AmplitudeBloom(bloom, bloomFactor);
        ampReact.AmplitudeChromaticAberration(ca, chromAbFactor);

        // Enemy Bright
        ampReact.AmplitudeBright(gameObject.GetComponent<MeshRenderer>(), 0.5f, initialColor);

        // Change Pattern
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= patternTime)
        {
            elapsedTime = 0;
            ChangeBulletPattern();
        }

        // Finish Level
        if (!GameObject.FindGameObjectWithTag("AudioInput").GetComponent<AudioSource>().isPlaying)
        {
            StopAllCoroutines();
            bulletPool.ClearPool();
            Debug.Log("Has ganado");
        }
    }

    private void ChangeBulletPattern()
    {
        int pattern = Random.Range(0, 3);

        StopAllCoroutines();

        switch (pattern)
        {
            case 0:
                bezier.enabled = false;
                StartCoroutine(BulletSpawnSpiral());
                break;
            case 1:
                StartCoroutine(BulletSpawnCircle());
                bezier.enabled = true;
                break;
            case 2:
                bezier.enabled = false;
                StartCoroutine(BulletSpawnFlower());
                break;
        }
    }

    private IEnumerator BulletSpawnSpiral()
    {
        yield return new WaitForSeconds(2);

        float angleStep = 360.0f / waveSizeSpiral;
        float angle;
        float offset = 0;

        while (true)
        {
            yield return new WaitForSeconds(spawnRatioSpiral);

            angle = offset;

            for (int i = 0; i < waveSizeSpiral; i++)
            {
                float dirX = Mathf.Cos((angle * Mathf.PI) / 180);
                float dirY = Mathf.Sin((angle * Mathf.PI) / 180);
                Vector3 bulletDirection = new Vector3(dirX, dirY, 0).normalized;

                bulletPool.SpawnBullet(transform.position, transform.rotation, bulletDirection);

                angle += angleStep;
            }

            offset += angleOffset;
        }
    }

    private IEnumerator BulletSpawnCircle()
    {
        float angleStep = 360.0f / waveSizeCircle;
        float angle;

        while (true)
        {
            yield return new WaitForSeconds(spawnRatioCircle);

            angle = 0;

            for (int i = 0; i < waveSizeCircle; i++)
            {
                float dirX = Mathf.Cos((angle * Mathf.PI) / 180);
                float dirY = Mathf.Sin((angle * Mathf.PI) / 180);
                Vector3 bulletDirection = new Vector3(dirX, dirY, 0).normalized;

                bulletPool.SpawnBullet(transform.position, transform.rotation, bulletDirection);

                angle += angleStep;
            }
        }
    }

    private IEnumerator BulletSpawnFlower()
    {
        float angleStep = 360.0f / waveSizeFlower;
        float angle;
        int cornerCount;

        while (true)
        {
            yield return new WaitForSeconds(spawnRatioFlower);

            angle = Random.Range(-15, 15);
            cornerCount = Random.Range(4, maxCorners);

            for (int i = 0; i < waveSizeFlower; i++)
            {
                float dirX = Mathf.Cos((angle * Mathf.PI) / 180);
                float dirY = Mathf.Sin((angle * Mathf.PI) / 180);
                float speedOffset = Mathf.Abs(Mathf.Sin(cornerCount * angle * (Mathf.PI / 360))) * 0.02f;
                Vector3 bulletDirection = new Vector3(dirX, dirY, 0);

                bulletPool.SpawnBullet(transform.position, transform.rotation, bulletDirection, speedOffset);

                angle += angleStep;
            }
        }
    }
}
