using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera camera;
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //assuming we're only using the single camera:
        camera = Camera.main;
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        float facing = camera.transform.rotation.y;
        float DistanceFromNeutral = 0;
        float transformZ = 0;
        float transformX = 0;
        float finalZ = 0;
        float finalX = 0;

        if (facing > -90 && facing <= 90)
        { //facing forward
            if (facing >= 0)
            {
                DistanceFromNeutral = (90 - facing);
            }
            else
            {
                if (facing < 0)
                {
                    DistanceFromNeutral = (90 + facing);
                };
            };


            transformX = (1 / 90) * (DistanceFromNeutral);
            transformZ = 90 - transformX;
        };


        finalX = (transformX * verticalAxis) + (transformZ * horizontalAxis);


        finalZ = (transformZ * verticalAxis) + (transformX * horizontalAxis);


        transform.Translate((new Vector3(finalX * 0.01f, 0f, finalZ * 0.01f)) * speed * Time.deltaTime);
    }


}
