using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interaction : MonoBehaviour
{
    public Flowchart flow;
    public string blockName;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(collision.gameObject.name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(collision.gameObject.name + " interacted with " + gameObject.name);
                if (!flow.HasExecutingBlocks())
                {
                    flow.ExecuteBlock(blockName);
                }
            }
        }
    }
}
