using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer outline;
    public GameObject soul;
    public Color normalColor;
    public Color blackColor;
    private Color currentOutlineColor;

    private void Start()
    {
        normalColor = outline.color;
        currentOutlineColor = normalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            soul.SetActive(true);
            currentOutlineColor = blackColor;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            currentOutlineColor = normalColor;
            soul.SetActive(false);
        }
    }

    private void Update()
    {
        outline.color = Color.Lerp(outline.color, currentOutlineColor, 0.1f);
    }

}