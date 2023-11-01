using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreSystem : MonoBehaviour
{
    private Action OnAddPoint;
    private Action OnLoosePoint;

    public LayerMask goodLayerMask;
    public LayerMask badLayerMask;

    private void Start()
    {
        OnAddPoint += ScoreManager.instance.AddPoints;
        OnLoosePoint += ScoreManager.instance.RemovePoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((goodLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            EarnPoint();
        }
        else if ((badLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            LoosePoint();
        }

    }

    private void LoosePoint()
    {
        OnLoosePoint?.Invoke();
    }

    private void EarnPoint()
    {
        OnAddPoint?.Invoke();
    }

}
