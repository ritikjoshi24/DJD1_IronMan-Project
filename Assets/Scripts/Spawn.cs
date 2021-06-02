using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    
    [SerializeField]private GameObject bullet;
    public Vector2 size;
    float countTimer = 3.0f;
    float outOfTime;
    

    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(2,0);
        outOfTime = countTimer;

        SpawnBullet();
    }

    // Update is called once per frame
    void Update()
    {
        outOfTime = outOfTime - Time.deltaTime;
        if (outOfTime <= 0)
        {
            SpawnBullet();
            outOfTime = countTimer;
        }
    }

  
    public void SpawnBullet()
    {

       Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
            Instantiate(bullet, pos, Quaternion.identity);
          
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }

    
}
