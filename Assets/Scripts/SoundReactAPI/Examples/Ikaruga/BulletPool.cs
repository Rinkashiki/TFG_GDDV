using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private Queue<GameObject> bulletPool;

    [SerializeField] Object bullet;
    [SerializeField] int poolSize;

    void Start()
    {
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bulletPool.Enqueue(obj);
        }
    }

    public GameObject SpawnBullet(Vector3 position, Quaternion rotation, Vector3 bulletDirection)
    {
        GameObject objToSpawn = bulletPool.Dequeue();

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        objToSpawn.GetComponent<Bullet>().SetDirection(bulletDirection);

        bulletPool.Enqueue(objToSpawn);

        return objToSpawn;
    }
}