using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Interaction : MonoBehaviour
{
    public bool isInteracting;
    public string objectName;
    public ObjectType type;

    [SerializeField]
    private Flowchart mainFlow = null;
    [SerializeField]
    private Flowchart saveFlow = null;
    private PlayerMovement pMov;

    private float pPosX;
    private float pPosY;

    public enum ObjectType
    {
        Nothing,
        NPC,
        Item,
        Save
    }

    private void Start()
    {
        pMov = GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        type = ObjectType.Nothing;

        if (collision.tag == "NPC")
        {
            Debug.Log(collision.gameObject.name);
            objectName = collision.gameObject.name;
            type = ObjectType.NPC;
        }
        if (collision.tag == "Item")
        {
            objectName = collision.gameObject.name;
            type = ObjectType.Item;
        }
        if (collision.tag == "Savepoint")
        {
            objectName = collision.gameObject.name;
            pPosX = collision.transform.GetChild(0).transform.position.x;
            pPosY = collision.transform.GetChild(0).transform.position.y;
            type = ObjectType.Save;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectName = null;
        type = ObjectType.Nothing;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!isInteracting && type != ObjectType.Nothing)
            {
                Debug.Log(gameObject.name + " interacted with " + objectName);
                if (type == ObjectType.Item)
                {
                    mainFlow.SetStringVariable("ItemName", objectName);
                    mainFlow.ExecuteBlock("PickupItem");
                }
                if (type == ObjectType.Save)
                {
                    saveFlow.ExecuteBlock(objectName);
                    GameObject.Find("GameManager").GetComponent<Savepoint>().PlayerPositionX = pPosX;
                    GameObject.Find("GameManager").GetComponent<Savepoint>().PlayerPositionY = pPosY;
                }
                if(type == ObjectType.NPC)
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
        pMov.rb.velocity = Vector2.zero;
        pMov.movement = Vector2.zero;
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
