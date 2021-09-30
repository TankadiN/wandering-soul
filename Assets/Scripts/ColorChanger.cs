using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer outline;
    public GameObject soul;
    public Tilemap tilemap;
    public Color normalColor;
    public Color blackColor;
    public float timeToLerp;
    private Color currentOutlineColor;
    private Color currentTilemapColor;
    private Color currentSoulColor;
    private Color transparent;
    private void Start()
    {
        normalColor = outline.color;
        currentOutlineColor = normalColor;
        transparent = new Color(outline.color.r, outline.color.g, outline.color.b, 0);
        currentSoulColor = transparent;
        soul.GetComponent<SpriteRenderer>().color = transparent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            soul.GetComponent<PolygonCollider2D>().enabled = true;
            currentTilemapColor = new Color(1, 1, 1, 0.5f);
            currentOutlineColor = blackColor;
            currentSoulColor = normalColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            currentOutlineColor = normalColor;
            currentSoulColor = transparent;
            currentTilemapColor = new Color(1, 1, 1, 0);
            soul.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        outline.color = Color.Lerp(outline.color, currentOutlineColor, timeToLerp * Time.deltaTime);
        soul.GetComponent<SpriteRenderer>().color = Color.Lerp(soul.GetComponent<SpriteRenderer>().color, currentSoulColor, timeToLerp * Time.deltaTime);
        tilemap.color = Color.Lerp(tilemap.color, currentTilemapColor, timeToLerp * Time.deltaTime);
    }

}