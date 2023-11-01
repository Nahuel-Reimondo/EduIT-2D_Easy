using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingWorldLimitsExample : MonoBehaviour
{
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private float speed = 5f;

    private Vector2 xLimits;

    void Start()
    {
        xLimits = ResponsiveManager.GetXLimitsAtZ(this.transform.position.z);
    }

    
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        this.transform.Translate(this.transform.right * xMove);

        if(this.transform.position.x < xLimits.x 
            ||
        this.transform.position.x > xLimits.y)
        {
            myRenderer.material.color = Color.red;
        }
        else
        {
            myRenderer.material.color = Color.green;
        }
    }
}
