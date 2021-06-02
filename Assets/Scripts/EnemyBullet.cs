using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float dieTime;
    private object hitInfo;
    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    // Update is called once per frame
  private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        PlayerHealth player = col.GetComponent<PlayerHealth>();
        if (player != null)
        {
          player.TakeDamage(20);
            Die();
        }
        
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
