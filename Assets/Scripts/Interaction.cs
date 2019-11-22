using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            Debug.Log(collision.gameObject.name);
            if (Input.GetButtonDown("Submit"))
            {
                Debug.Log(gameObject.name + " interacted with " + collision.gameObject.name);
                PlayerMovement.inst.SwitchEnabled();
            }
        }

    }
}
