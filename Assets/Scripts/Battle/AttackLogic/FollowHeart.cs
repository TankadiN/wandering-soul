using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeart : MonoBehaviour
{
    public float speed;

    public Transform target;
    void Start()
    {
        target = GameObject.Find("Player_Battle").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
