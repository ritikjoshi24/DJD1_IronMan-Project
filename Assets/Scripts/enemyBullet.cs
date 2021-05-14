using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{

    public float dieTime, damage;
    private object hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    // Update is called once per frame
  private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        Die();
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
