using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{
    public Slider slider;

    public float timeSpeed;

    public bool active;
    public bool decrease;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if(decrease)
            {
                slider.value -= Time.deltaTime * timeSpeed;
            }
            else
            {
                slider.value += Time.deltaTime * timeSpeed;
            }
        }

        if(slider.value == slider.maxValue)
        {
            decrease = true;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
        }
    }
}
