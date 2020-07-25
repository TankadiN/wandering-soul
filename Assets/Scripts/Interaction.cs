using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interaction : MonoBehaviour
{
    public bool isInteracting;
    public string objectName;
    public bool isNothing;
    public bool isItem;
    public bool isSave;

    [SerializeField]
    private Flowchart mainFlow;
    [SerializeField]
    private Flowchart saveFlow;
    private PlayerMovement pMov;

    private float pPosX;
    private float pPosY;

    private void Start()
    {
        pMov = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isNothing = false;

        if (collision.tag == "NPC")
        {
            Debug.Log(collision.gameObject.name);
            objectName = collision.gameObject.name;
        }
        if (collision.tag == "Item")
        {
            objectName = collision.gameObject.name;
            isItem = true;
        }
        if (collision.tag == "Savepoint")
        {
            objectName = collision.gameObject.name;
            pPosX = collision.transform.GetChild(0).transform.position.x;
            pPosY = collision.transform.GetChild(0).transform.position.y;
            isSave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectName = null;
        isItem = false;
        isSave = false;
        isNothing = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!isInteracting && !isNothing)
            {
                Debug.Log(gameObject.name + " interacted with " + objectName);
                isInteracting = true;
                if (isItem)
                {
                    mainFlow.SetStringVariable("ItemName", objectName);
                    mainFlow.ExecuteBlock("PickupItem");
                }
                if (isSave)
                {
                    saveFlow.ExecuteBlock(objectName);
                    GameObject.Find("GameManager").GetComponent<Savepoint>().PlayerPositionX = pPosX;
                    GameObject.Find("GameManager").GetComponent<Savepoint>().PlayerPositionY = pPosY;
                }
                if(!isItem && !isSave)
                {
                    mainFlow.ExecuteBlock(objectName);
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
        if (isInteracting)
        {
            isInteracting = false;
        }
        else
        {
            isInteracting = true;
        }
    }
}
