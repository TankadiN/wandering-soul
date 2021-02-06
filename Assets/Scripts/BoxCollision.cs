using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    private SpriteRenderer spr;
    //private PolygonCollider2D poly;
    private EdgeCollider2D edge;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        //poly = GetComponent<PolygonCollider2D>();
        edge = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        Vector2[] myPoints = edge.points;
        float calcX = (spr.size.x - 0.16f) / 2;
        float calcY = (spr.size.y - 0.16f) / 2;

        myPoints[0] = new Vector2(-calcX, calcY);
        myPoints[1] = new Vector2(-calcX, -calcY);
        myPoints[2] = new Vector2(calcX, -calcY);
        myPoints[3] = new Vector2(calcX, calcY);
        myPoints[4] = new Vector2(-calcX, calcY);

        //poly.points = myPoints;

        edge.points = myPoints;
    }
}
