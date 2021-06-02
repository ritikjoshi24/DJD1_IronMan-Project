using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHP : MonoBehaviour
{
    //public GameObject playerhealth;
     //PlayerHealth player;
    void Start()
    {
       // player = GetComponent<PlayerHealth>();
    }
    public void OnCollisionEnter2D(Collision2D collis)
    {
      
        if (collis.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            CrateScore.crateValue += 1;
        }
    }
}
