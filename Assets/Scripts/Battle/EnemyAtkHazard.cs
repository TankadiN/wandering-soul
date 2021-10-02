using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkHazard : MonoBehaviour
{
    public float damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<PlayerController>().Damage(damage);
        }
    }
}
