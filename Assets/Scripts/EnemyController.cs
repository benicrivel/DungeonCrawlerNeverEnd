using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float attackRange = 3.0f;
    public float attackCooldown = 2.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float timeSinceLastAttack = 0f;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance <= attackRange)
        {
            if (timeSinceLastAttack == 0) 
            {
                Shoot();
                timeSinceLastAttack = 1;
                StartCoroutine(TimeToShootEnemy());
            }            
        }
       
    }  
    void Shoot()
    {
        Vector2 direction = playerTransform.position - bulletSpawnPoint.position;
        direction.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 10.0f;       
    }
    IEnumerator TimeToShootEnemy()
    {
        yield return new WaitForSeconds(10);
        timeSinceLastAttack = 0;
        Debug.Log("Reset Atack Enemy");
    }
}