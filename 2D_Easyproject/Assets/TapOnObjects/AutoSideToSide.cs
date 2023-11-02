using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSideToSide : MonoBehaviour
{
    [SerializeField] private bool usePingPong;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float bodyWidth;
    private Vector2 xLimits;
    private int direction = 1;

    void Start()
    {
        xLimits = ResponsiveManager.GetXLimitsAtZ(this.transform.position.z);
    }

    void Update()
    {
        if (usePingPong)
        {
            Vector3 newPos = this.transform.position;
            float screenWidth = xLimits.y - xLimits.x;
            newPos.x = Mathf.PingPong(Time.time, screenWidth) - screenWidth/2;

            this.transform.position = newPos;
            return;
        }
        this.transform.Translate(this.transform.right * speed * Time.deltaTime * direction);

        if (this.transform.position.x - bodyWidth < xLimits.x
            ||
        this.transform.position.x + bodyWidth > xLimits.y)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        direction *= -1;
    }
}
