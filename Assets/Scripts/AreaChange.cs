using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AreaChange : MonoBehaviour
{
    public GameObject cameraContainer;

    public CinemachineVirtualCamera[] vCams;

    public CinemachineVirtualCamera destinatedCamera;
    public GameObject locationTeleport;
    private Animator anim;

    void Start()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        vCams = cameraContainer.GetComponentsInChildren<CinemachineVirtualCamera>();
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
        foreach (CinemachineVirtualCamera Cam in vCams)
        {
            Cam.m_Priority = 0;
        }
        destinatedCamera.m_Priority = 1;
        GameObject.Find("Player").transform.position = locationTeleport.transform.position;
        anim.SetTrigger("FadeIn");
    }
}
