using UnityEngine;
using System.Collections;

public static class HudUtility
{
    public static float GetRotation(float x1, float y1, float x2, float y2)
    {
        float pi = 3.141593f;
        float diferenceX = x2 - x1;
        float diferenceY = y2 - y1;
        float Atan = (Mathf.Atan(diferenceY / diferenceX) * 180) / pi;
        if (diferenceX < 0)
        {
            Atan += 180;
        }
        return Atan;
    }

    public static Vector2 GetPivot(float h, float v, float size)
    {
        float horizontal = h - (mCamera.pixelWidth * 0.5f);
        float vertical = v - (mCamera.pixelHeight * 0.5f);

        float slope = vertical / horizontal;

        Vector2 vector = Vector2.zero;

        float height;

        if ((slope > GetScreenSlope) || (slope < -GetScreenSlope))
        {
            height = (MidlleHeight - HalfSize(size)) / vertical;
            if (vertical < 0)
            {
                vector.y = HalfSize(size);
                height *= -1;
            }
            else
            {
                vector.y = mCamera.pixelHeight - HalfSize(size);
            }
            vector.x = MiddleWidth + (horizontal * height);
            return vector;
        }

        height = (MiddleWidth - HalfSize(size)) / horizontal;
        if (horizontal < 0)
        {
            vector.x = HalfSize(size);
            height *= -1;
        }
        else
        {
            vector.x = mCamera.pixelWidth - HalfSize(size);
        }
        vector.y = MidlleHeight + (vertical * height);
        return vector;
    }

    public static float GetScreenSlope
    {
        get
        {
            //Fix for Unity 5 
            float h = mCamera.pixelHeight;
            float w = mCamera.pixelWidth;

            float s = h / w;

            return s;
        }
    }

    public static float MidlleHeight
    {
        get
        {
            return Camera.main.pixelHeight / 2;
        }
    }

    public static float MiddleWidth
    {
        get
        {
            return Camera.main.pixelWidth / 2;
        }
    }

    public static float HalfSize(float s)
    {
        return s / 2;
    }


    public static Camera mCamera
    {
        get
        {
            if (Camera.main != null)
            {
                return Camera.main;
            }
            else
            {
                return Camera.current;
            }
        }
    }


    public static Vector2 Marge(Vector2 v,float size)
    {
        float globalMarge = 50f;
        Vector2 m = v;

        float screenWith = Screen.width * 0.5f;
     //   m.x = (m.x / screenWith) * globalMarge;
        float screenHeight = Screen.height * 0.5f;
      //  m.y = (m.y / screenHeight) * globalMarge;

        if ((v.x + size) >= (screenWith))
        {
            m.x = m.x - screenWith;
            m.x = -m.x;
            m.x = Mathf.Clamp(m.x, -(globalMarge ), 0);
        }
        else
        {
            m.x = screenWith - m.x;
            m.x = Mathf.Clamp(m.x, 0, globalMarge);
        }

        if ((v.y + size) >= (screenHeight))
        {
            m.y = m.y - screenHeight;
            m.y = -m.y;
            m.y = Mathf.Clamp(m.y, -(globalMarge ), 0);
        }
        else
        {
            m.y = screenHeight - m.y;
            m.y = Mathf.Clamp(m.y, 0, globalMarge);
        }

        return m;
    }

    public static Rect ScalerRect(Rect _rect)
    {

        float retine = RetineAspect;

        _rect.width = _rect.width * retine;
        _rect.height = _rect.height * retine;
        _rect.x = _rect.x * retine;
        _rect.y = _rect.y * retine;

        return _rect;
    }

    public static float RetineAspect
    {
        get
        {
            Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
            float scaleFactor = 0f;

            float from = Mathf.Log(vector.x / HudManager.instance.m_ReferenceResolution.x, 2f);
            float to = Mathf.Log(vector.y / HudManager.instance.m_ReferenceResolution.y, 2f);
            float p = Mathf.Lerp(from, to, HudManager.instance.m_MatchWidthOrHeight);
            scaleFactor = Mathf.Pow(2f, p);

            return scaleFactor;
        }
    }

    public static Vector3 ScreenPosition(Transform t)
    {
        Vector3 screenPos;
        if (mCamera != null)
        {
            screenPos = mCamera.WorldToScreenPoint(t.position);
            screenPos.x = screenPos.x / mCamera.pixelWidth;
            screenPos.y = screenPos.y / mCamera.pixelHeight;
            screenPos.z = t.position.z;
        }
        else
        {
            screenPos = Vector3.one * 0.5f;
        }

        return screenPos;
    }

    public static bool isOnScreen(Vector3 pos,Transform t)
    {

        Vector3 mDirection = t.position - mCamera.transform.position;
        if (Vector3.Dot(mDirection, mCamera.transform.forward) <= 0)
        {
            return false;
        }

        float margen = 0.001f;

        if (pos.x < margen ||
            pos.x > 1 - margen ||
            pos.y < margen ||
            pos.y > 1 - margen)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}