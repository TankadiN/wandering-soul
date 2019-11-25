using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interaction : MonoBehaviour
{
    public bool isInteracting;
    public string NPCName;

    private Flowchart flow;
    private PlayerMovement pMov;

    private void Start()
    {
        flow = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        pMov = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            Debug.Log(collision.gameObject.name);
            NPCName = collision.gameObject.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCName = null;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!isInteracting)
            {
                if (flow.HasBlock(NPCName))
                {
                    Debug.Log(gameObject.name + " interacted with " + NPCName);
                    isInteracting = true;
                    flow.ExecuteBlock(NPCName);
                }
            }
        }

        if(isInteracting)
        {
            pMov.enabled = false;
        }
        else
        {
            pMov.enabled = true;
        }
    }

    public void InteractionSwitch()
    {
        isInteracting = false;
    }
}
