using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fungus;

public class FungusTrigger : MonoBehaviour
{
    public string onCollEnterBlock;
    public string onCollExitBlock;
    public Flowchart flow;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            flow.ExecuteBlock(onCollEnterBlock);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            flow.ExecuteBlock(onCollExitBlock);
        }
    }
}