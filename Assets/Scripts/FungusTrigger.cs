using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FungusTrigger : MonoBehaviour
{
    public SpriteRenderer outline;
    public SpriteRenderer soul;
    public Color normalColor;
    public Color blackColor;
    public Color invisibleSoulColor;
    private Color currentOutlineColor;
    private Color currentSoulColor;

    private void Start()
    {
        normalColor = outline.color;
        currentOutlineColor = normalColor;
        invisibleSoulColor = new Color(normalColor.r, normalColor.g, normalColor.b, 0);
        currentSoulColor = invisibleSoulColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currentOutlineColor = blackColor;
            currentSoulColor = normalColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            currentOutlineColor = normalColor;
            currentSoulColor = invisibleSoulColor;
        }
    }

    private void Update()
    {
        //outline.color = Color.Lerp(fromColor, toColor, Mathf.PingPong(Time.time, 1));
        outline.color = Color.Lerp(outline.color, currentOutlineColor, 0.1f);
        soul.color = Color.Lerp(soul.color, currentSoulColor, 0.1f);
    }
}