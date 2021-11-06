using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValuePopup : MonoBehaviour
{
    public static ValuePopup Create(Vector3 pos, float amount, Type type)
    {
        Transform valuePopupTransform = Instantiate(GameAssets.i.valuePopup, pos, Quaternion.identity);
        ValuePopup valuePopup = valuePopupTransform.GetComponent<ValuePopup>();
        valuePopup.Setup(amount, type);

        return valuePopup;
    }

    private void Awake()
    {
        textbox = transform.GetComponent<TMP_Text>();
    }

    public void Setup(float amount, Type type)
    {
        if(type == Type.Miss)
        {
            textbox.SetText("MISS");
            textColor = missColor;
        }
        else if(type == Type.Damage)
        {
            textbox.SetText(amount.ToString());
            textColor = damageColor;
        }
        else if(type == Type.Mercy)
        {
            textbox.SetText("+" + amount.ToString() + "%");
            textColor = mercyColor;
        }
        textbox.color = textColor;
        disappearTimer = timeToDisappear;
    }

    public enum Type
    {
        Miss,
        Damage,
        Mercy
    }

    private TMP_Text textbox;
    public float timeToDisappear;
    private float disappearTimer;
    private Color textColor;
    public Color damageColor;
    public Color mercyColor;
    public Color missColor;
    public float moveYSpeed;

    private void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        if (disappearTimer > timeToDisappear * .5f)
        {
            float incrScale = 1f;
            transform.localScale += Vector3.one * incrScale * Time.deltaTime;
        }
        else
        {
            float decrScale = 1f;
            transform.localScale -= Vector3.one * decrScale * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textbox.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
