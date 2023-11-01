using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResponsiveManager
{
    private static Camera mainCam;

    public static Camera GetMainCamera()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        return mainCam;
    }

    public static Rect GetWorldLimits()
    {
        Camera cam = GetMainCamera();
        
        return ViewportRect(cam);
    }

    public static Rect GetWorldLimits(float distance)
    {
        Camera cam = GetMainCamera();

        return ScreenRect(cam,distance);
    }

    private static Rect ViewportRect(Camera cam)
    {
        Vector3 botomLeft = new Vector3(0, 0, 0);
        Vector3 topRight = new Vector3(1, 1, 1);

        Rect worldRect = new Rect();
        worldRect.xMin = cam.ViewportToWorldPoint(botomLeft).x;
        worldRect.xMax = cam.ViewportToWorldPoint(topRight).x;
        worldRect.yMin = cam.ViewportToWorldPoint(botomLeft).y;
        worldRect.yMax = cam.ViewportToWorldPoint(topRight).y;

        return worldRect;
    }

    //https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
    private static Rect ScreenRect(Camera cam, float distance)
    {
        float distanceFromCamera = distance - cam.transform.position.z;
        var frustumHeight = 2.0f * distanceFromCamera * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);

        Rect worldRect = new Rect();
        worldRect.center = cam.transform.position;
        worldRect.height = frustumHeight;
        worldRect.width = (frustumHeight * cam.aspect);

        return worldRect;
    }

    public static Vector3 GetWorldCenter(float zCoord)
    {
        Camera cam = GetMainCamera();

        if (cam.orthographic)
        {
            return ViewportCenter(cam, zCoord);
        }
        else
        {
            return ScreenCenter(cam, zCoord);
        }
    }

    private static Vector3 ViewportCenter(Camera cam, float zCoord)
    {
        Vector3 center = new Vector3(0.5f, 0.5f, 0);

        Vector3 worldCenter = new Vector3();
        worldCenter.x = cam.ViewportToWorldPoint(center).x;
        worldCenter.y = cam.ViewportToWorldPoint(center).y;
        worldCenter.z = zCoord;

        return worldCenter;
    }

    private static Vector3 ScreenCenter(Camera cam, float zCoord)
    {
        Vector3 center = new Vector3(0.5f, 0.5f, cam.nearClipPlane);

        Vector3 worldCenter = new Vector3();
        worldCenter.x = cam.ScreenToWorldPoint(center).x;
        worldCenter.y = cam.ScreenToWorldPoint(center).y;
        worldCenter.z = zCoord;

        return worldCenter;
    }

    public static Vector2 GetXLimitsAtZ(float zCoord)
    {
        Camera cam = GetMainCamera();

        if (cam.orthographic)
        {
            Rect rect = GetWorldLimits();
            rect.center = GetWorldCenter(zCoord);
            return new Vector2(rect.xMin, rect.xMax);
        }
        else
        {
            Rect rect = GetWorldLimits(zCoord);
            rect.center = GetWorldCenter(zCoord);
            return new Vector2(rect.xMin, rect.xMax);
        }
    }

    public static Vector2 GetYLimitsAtZ(float zCoord)
    {
        Camera cam = GetMainCamera();

        if (cam.orthographic)
        {
            Rect rect = GetWorldLimits();
            rect.center = GetWorldCenter(zCoord);
            return new Vector2(rect.yMin, rect.yMax);
        }
        else
        {
            Rect rect = GetWorldLimits(zCoord);
            rect.center = GetWorldCenter(zCoord);
            return new Vector2(rect.yMin, rect.yMax);
        }
    }

}
