using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemente2d : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector2 xLimits;

    private int halfScreen;
    private Touch touch;

    void Start()
    {
        xLimits = ResponsiveManager.GetXLimitsAtZ(this.transform.position.z);
        halfScreen = Screen.width / 2;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            transform.localScale = Vector3.one;
        }

        if (!Input.GetMouseButton(0))
            return;

        Vector2 mousePos = Input.mousePosition;

        if (mousePos.x > halfScreen)
        {
            MovePlayer(movementSpeed);
        }
        else
        {
            MovePlayer(-movementSpeed);
        }

        ScalePlayer();

#elif UNITY_ANDROID

        if (Input.touchCount == 0)
            return;


        touch = Input.GetTouch(0);

        if (touch.position.x > halfScreen)
        {
            MovePlayer(movementSpeed);
        }
        else
        {
            MovePlayer(-movementSpeed);
        }

        ScalePlayer();
 
#endif
    }

    private void ScalePlayer()
    {
        Vector3 newScale = this.transform.localScale;
        newScale += Vector3.one * 0.5f * Time.deltaTime;
        transform.localScale = newScale;

#if UNITY_ANDROID
        if (touch.phase == TouchPhase.Ended)
        {
            transform.localScale = Vector3.one;
        }
#endif
    }

    private void MovePlayer(float speed)
    {
        Vector3 movement = new Vector3 (speed * Time.deltaTime, 0, 0);

        if (!IsInsideLimits(this.transform.position + movement))
            return;

        transform.Translate(movement);

    }

    private bool IsInsideLimits(Vector3 pos)
    {
        return pos.x > xLimits.x
            &&
            pos.x < xLimits.y;
    }

}