using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChange : MonoBehaviour
{
    public static AreaChange instance;
    public GameObject activeCamera;
    public GameObject destinatedCamera;
    public GameObject locationTeleport;
    private Animator anim;

    void Start()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(ChangeArea());
        }
    }

    public IEnumerator ChangeArea()
    {
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        activeCamera.SetActive(false);
        destinatedCamera.SetActive(true);
        GameObject.Find("Player").transform.position = locationTeleport.transform.position;
        anim.SetTrigger("FadeIn");
    }
}
