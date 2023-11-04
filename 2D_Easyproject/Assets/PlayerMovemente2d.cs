using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemente2d : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector2 xLimits;
    void Start()
    {
        xLimits = ResponsiveManager.GetXLimitsAtZ(this.transform.position.z);
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //the phase correct 
            /*    
            if (touch.phase == TouchPhase.Began)
            {             
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(
                    touch.position);
                touchPosition.z = 0f;
                transform.position = touchPosition;
            }
            */
            int half = Screen.width / 2;
            if (touch.position.x > half)
            {   //right
                if (this.transform.position.x < xLimits.y)
                {
                    transform.Translate(new Vector3(
                    movementSpeed * Time.deltaTime, 0, 0));
                }
            }
            else
            {
                //left
                if (this.transform.position.x > xLimits.x)
                {
                    transform.Translate(new Vector3(
                   -movementSpeed * Time.deltaTime, 0, 0));
                }
            }
            
            Vector3 newScale = this.transform.localScale;
            newScale += Vector3.one * 0.5f * Time.deltaTime;
            transform.localScale = newScale;
            
            if (touch.phase == TouchPhase.Ended)
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}
