using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner_Script : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletPos;
    [SerializeField] private float timeLimt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLimt += Time.deltaTime;
        if (timeLimt >= 2)
        {
            SpawnProjectile();
            timeLimt = 0;
        }
    }

    public void SpawnProjectile()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletPos.position;
    }
}
