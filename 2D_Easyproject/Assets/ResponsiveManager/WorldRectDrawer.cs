using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRectDrawer : MonoBehaviour
{
    [SerializeField] private float z;

    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (ResponsiveManager.GetMainCamera().orthographic)
        {
            Gizmos.DrawWireCube(ResponsiveManager.GetWorldCenter(z),
              ResponsiveManager.GetWorldLimits().size);
        }
        else 
        {
            Gizmos.DrawWireCube(ResponsiveManager.GetWorldCenter(z),
                 ResponsiveManager.GetWorldLimits(z).size);
        }
    }

}
