using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    Rigidbody2D rb;
    
    float throwSpeed;
    public Transform shotPoint;
   // GameObject Burst;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 bulletPosition = transform.position;
      //  Vector2 Direction =
     //   transform.right = Direction;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
