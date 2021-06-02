using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject bulletPrefab;
    [SerializeField]private float fireRate = 0.5f;
    private float nextFire = 0f;
    private float fireBoost = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
            
        }
    }

    void Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("fireUpgrade"))
        {
            fireRate = 0.2f;
            StartCoroutine(fireUpgrade());
            Destroy(col.gameObject);
        }
    }

    IEnumerator fireUpgrade()
    {
        yield return new WaitForSeconds(fireBoost);
        fireRate = 0.5f;
    }

}
