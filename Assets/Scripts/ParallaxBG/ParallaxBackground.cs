using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float length;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }
    private void Update()
    {
        float distanceMoved = cam.transform.position.x * (1-parallaxEffect);
        float distanceToMove = parallaxEffect * cam.transform.position.x;
        transform.position = new Vector2(xPosition + distanceToMove, cam.transform.position.y);
        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if(distanceMoved <xPosition-length)
            xPosition = xPosition - length;
    }
}
