using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGroundTriggerCollision : MonoBehaviour
{
    public LayerMask groundLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((groundLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            Destroy(this.gameObject);
        }

    }
}
