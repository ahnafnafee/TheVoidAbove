using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

public class HudManager : MonoBehaviour
{
    [Tooltip("Hud list manager, you can add a new hud directly here.")]
    public List<HudInfo> huds = new List<HudInfo>();

    [Tooltip("You can use MainCamera or the root of your player")]
    public Transform localPlayer = null;

    public float clampBorder = 12;
    public bool useGizmos = true;
    [Header("Global Settings")]
    [Range(1, 50)] public float iconSize = 50;
    [Range(1, 50)] public float offScreenIconSize = 25;

    [Header("GUI Scaler")]
    [Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the GUI will be scaled up, and if it's smaller, the GUI will be scaled down. This is done in accordance with the Screen Match Mode.")]
    public Vector2 m_ReferenceResolution = new Vector2(800f, 600f);
    [Range(0f, 1f), Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between."), SerializeField]
    public float m_MatchWidthOrHeight;
     [Tooltip("Select Reference Resolution automatically in run time.")]
    public bool AutoScale = true;
    [Header("Style")]
    public GUIStyle TextStyle;


    private static HudManager _instance;

    //Get singleton
    public static HudManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<HudManager>();
            }
            return _instance;
        }
    }

    public void UpdateHudList(string sHud)
    {
        var hudToRemove = huds.Single(r => r.m_Text == sHud);
        huds.Remove(hudToRemove);
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void Update() { if (AutoScale) { m_ReferenceResolution.x = Screen.width; m_ReferenceResolution.y = Screen.height; } }

    void OnGUI()
    {
        if (HudUtility.mCamera == null)
            return;
        if (localPlayer == null)
            return;
        //pass test :)

        for (int i = 0; i < huds.Count; i++)
        {
            if (!huds[i].Hide)
            {
                if(huds[i].HideOnCloseDistance > 0 && GetHudDistance(i) < huds[i].HideOnCloseDistance) { continue; }
                if (huds[i].HideOnLargeDistance > 0 && GetHudDistance(i) > huds[i].HideOnLargeDistance) { continue; }
                OnScreen(i);
                OffScreen(i);
            }
        }

    }

    void OnScreen(int i)
    {
        //if transform destroy, then remove from list
        if (huds[i].m_Target == null)
        {
            huds.Remove(huds[i]);
            return;
        }

        // Debug.Log(
            // $"Hud {huds[i].m_Text}, Onscreen : {HudUtility.isOnScreen(HudUtility.ScreenPosition(huds[i].m_Target), huds[i].m_Target)}");

        //Check target if OnScreen
        if (HudUtility.isOnScreen(HudUtility.ScreenPosition(huds[i].m_Target), huds[i].m_Target))
        {
            //Calculate Position of target
            Vector3 RelativePosition = huds[i].m_Target.position + huds[i].Offset;

            // Dot product is returning false for some reason

            // Vector3 locPlayer = localPlayer.forward;
            // locPlayer = new Vector3(Mathf.Abs(locPlayer.x), Mathf.Abs(locPlayer.y), Mathf.Abs(locPlayer.z));

            // Debug.Log($"Dot: {Vector3.Dot(locPlayer, RelativePosition - localPlayer.position) > 0f}");
            // Debug.Log($"LocPlayer: {locPlayer}");

            // OLD VERSION
            // if ((Vector3.Dot(this.localPlayer.forward, RelativePosition - this.localPlayer.position) > 0f))

            // if ((!(Vector3.Dot(locPlayer, RelativePosition - this.localPlayer.position) > 0f))) return;

            //Calculate the 2D position of the position where the icon should be drawn
            Vector3 point = HudUtility.mCamera.WorldToViewportPoint(RelativePosition);

            //The viewportPoint coordinates are between 0 and 1, so we have to convert them into screen space here
            Vector2 drawPosition = new Vector2(point.x * Screen.width, Screen.height * (1 - point.y));

            if (!huds[i].Arrow.ShowArrow)
            {
                //Clamp the position to the edge of the screen in case the icon would be drawn outside the screen
                drawPosition.x = Mathf.Clamp(drawPosition.x, clampBorder, Screen.width - clampBorder);
                drawPosition.y = Mathf.Clamp(drawPosition.y, clampBorder, Screen.height - clampBorder);
            }
            //Calculate distance from player to way point
            float Distance = Vector3.Distance(this.localPlayer.position, RelativePosition);
            //Cache distance
            float CompleteDistance = Distance;

            //Max Hud Increment
            if (Distance > huds[i].m_MaxSize) // if more than "50" no increase more
            {
                Distance = 50;
            }
            float n = iconSize;
            //Calculate depend of type
            if (huds[i].m_HudType == HudType.Decreasing)
            {
                n = (((50 + Distance) / (25)) * 0.9f) + 0.1f;
            }
            else if (huds[i].m_HudType == HudType.Increasing)
            {
                n = (((50 - Distance) / (25)) * 0.9f) + 0.1f;
            }
            //Calculate Size of Hud
            float sizeX = huds[i].m_Icon.width * n;
            if (sizeX >= huds[i].m_MaxSize)
            {
                sizeX = huds[i].m_MaxSize;
            }
            float sizeY = huds[i].m_Icon.height * n;
            if (sizeY >= huds[i].m_MaxSize)
            {
                sizeY = huds[i].m_MaxSize;
            }
            float TextUperIcon = sizeY / 2 + 5;

            //palpating effect
            if (huds[i].isPalpitin)
            {
                Palpating(huds[i]);
            }

            //Draw Huds
            GUI.color = huds[i].m_Color;
            GUI.DrawTexture(new Rect(drawPosition.x - (sizeX / 2), drawPosition.y - (sizeY / 2), sizeX, sizeY), huds[i].m_Icon);
            if (!huds[i].ShowDistance)
            {
                if (!string.IsNullOrEmpty(huds[i].m_Text))
                {
                    Vector2 size = TextStyle.CalcSize(new GUIContent(huds[i].m_Text));
                    GUI.Label(new Rect(drawPosition.x - (size.x / 2) + 10, (drawPosition.y - (size.y / 2)) - TextUperIcon, size.x, size.y), huds[i].m_Text, TextStyle);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(huds[i].m_Text))
                {
                    string text = huds[i].m_Text + "\n<color=white>[" + string.Format("{0:N0}m", CompleteDistance) + "]</color>";
                    Vector2 size = TextStyle.CalcSize(new GUIContent(text));
                    GUI.Label(new Rect(drawPosition.x - (size.x / 2) + 10, (drawPosition.y - (size.y / 2)) - TextUperIcon, size.x, size.y), text, TextStyle);
                }
                else
                {
                    string text = "<color=white>[" + string.Format("{0:N0}m", CompleteDistance) + "]</color>";
                    Vector2 size = TextStyle.CalcSize(new GUIContent(text));
                    GUI.Label(new Rect(drawPosition.x - (size.x / 2) + 10, ((drawPosition.y - (size.y / 2)) - TextUperIcon), size.x, size.y), text, TextStyle);
                }
            }
        }
    }

    void OffScreen(int i)
    {
        //if transform destroy, then remove from list
        if (huds[i].m_Target == null)
        {
            huds.Remove(huds[i]);
            return;
        }

        if (huds[i].Arrow.ArrowIcon != null && huds[i].Arrow.ShowArrow)
        {

            //Check target if OnScreen
            if (!HudUtility.isOnScreen(HudUtility.ScreenPosition(huds[i].m_Target), huds[i].m_Target))
            {
                //Get the relative position of arrow
                Vector3 ArrowPosition = huds[i].m_Target.position + huds[i].Arrow.ArrowOffset;
                Vector3 pointArrow = HudUtility.mCamera.WorldToScreenPoint(ArrowPosition);

                pointArrow.x = pointArrow.x / HudUtility.mCamera.pixelWidth;
                pointArrow.y = pointArrow.y / HudUtility.mCamera.pixelHeight;

                Vector3 mForward = huds[i].m_Target.position - HudUtility.mCamera.transform.position;
                Vector3 mDir = HudUtility.mCamera.transform.InverseTransformDirection(mForward);
                mDir = mDir.normalized / 5;
                pointArrow.x = 0.5f + mDir.x * 20f / HudUtility.mCamera.aspect;
                pointArrow.y = 0.5f + mDir.y * 20f;

                if (pointArrow.z < 0)
                {
                    pointArrow *= -1f;
                    pointArrow *= -1f;
                }
                //Arrow
                GUI.color = huds[i].m_Color;

                float Xpos = HudUtility.mCamera.pixelWidth * pointArrow.x;
                float Ypos = HudUtility.mCamera.pixelHeight * (1f - pointArrow.y);

                //palpating effect
                if (huds[i].isPalpitin)
                {
                    Palpating(huds[i]);
                }

                //Calculate area to rotate guis
                float mRot = HudUtility.GetRotation(HudUtility.mCamera.pixelWidth / (2), HudUtility.mCamera.pixelHeight / (2), Xpos, Ypos);
              //Get pivot from area
                Vector2 mPivot = HudUtility.GetPivot(Xpos, Ypos, huds[i].Arrow.ArrowSize);
                //Arrow
                Matrix4x4 matrix = GUI.matrix;
                GUIUtility.RotateAroundPivot(mRot, mPivot);
                GUI.DrawTexture(new Rect(mPivot.x - HudUtility.HalfSize(huds[i].Arrow.ArrowSize), mPivot.y - HudUtility.HalfSize(huds[i].Arrow.ArrowSize), huds[i].Arrow.ArrowSize, huds[i].Arrow.ArrowSize), huds[i].Arrow.ArrowIcon);
                GUI.matrix = matrix;

                float ClampedX = Mathf.Clamp(mPivot.x, 20, (Screen.width - offScreenIconSize) - 20);
                float ClampedY = Mathf.Clamp(mPivot.y, 20, (Screen.height - offScreenIconSize) - 20);
                GUI.DrawTexture(HudUtility.ScalerRect(new Rect(ClampedX, ClampedY, offScreenIconSize, offScreenIconSize)), huds[i].m_Icon);

                Vector2 ClampedTextPosition = mPivot;
                //Icons and Text
                if (!huds[i].ShowDistance)
                {
                    if (!string.IsNullOrEmpty(huds[i].m_Text))
                    {
                        Vector2 size = TextStyle.CalcSize(new GUIContent(huds[i].m_Text));
                        ClampedTextPosition.x = Mathf.Clamp(ClampedTextPosition.x, (size.x + offScreenIconSize) + 30, ((Screen.width - offScreenIconSize)- 10) - size.x);
                        ClampedTextPosition.y = Mathf.Clamp(ClampedTextPosition.y, (size.y + offScreenIconSize) + 35, ((Screen.height - size.y) - offScreenIconSize) - 20);
                        GUI.Label(HudUtility.ScalerRect(new Rect(ClampedTextPosition.x - (size.x / 2), ClampedTextPosition.y - (size.y / 2), size.x, size.y)), huds[i].m_Text, TextStyle);
                    }
                }
                else
                {
                    float Distance = Vector3.Distance(localPlayer.position, huds[i].m_Target.position);
                    if (!string.IsNullOrEmpty(huds[i].m_Text))
                    {
                        string text = huds[i].m_Text + "\n <color=white>[" + string.Format("{0:N0}m", Distance) + "]</color>";
                        Vector2 size = TextStyle.CalcSize(new GUIContent(text));
                        ClampedTextPosition.x = Mathf.Clamp(ClampedTextPosition.x, (size.x + offScreenIconSize) + 30, ((Screen.width - offScreenIconSize) - 10) - size.x);
                        ClampedTextPosition.y = Mathf.Clamp(ClampedTextPosition.y, (size.y + offScreenIconSize) + 35, ((Screen.height - size.y) - offScreenIconSize) - 20);
                        GUI.Label(HudUtility.ScalerRect(new Rect(ClampedTextPosition.x - (size.x / 2), (ClampedTextPosition.y - (size.y / 2)), size.x, size.y)), text, TextStyle);
                    }
                    else
                    {
                        string text = "<color=white>[" + string.Format("{0:N0}m", Distance) + "]</color>";
                        Vector2 size = TextStyle.CalcSize(new GUIContent(text));
                        ClampedTextPosition.x = Mathf.Clamp(ClampedTextPosition.x, (size.x + offScreenIconSize) + 30, ((Screen.width - offScreenIconSize) - 10) - size.x);
                        ClampedTextPosition.y = Mathf.Clamp(ClampedTextPosition.y, (size.y + offScreenIconSize) + 35, ((Screen.height - size.y) - offScreenIconSize) - 20);
                        GUI.Label(HudUtility.ScalerRect(new Rect(ClampedTextPosition.x - (size.x / 2) , (ClampedTextPosition.y - (size.y / 2)), size.x, size.y)),text, TextStyle);
                    }
                }
                // GUI.DrawTexture(HudUtility.ScalerRect(new Rect(mPivot.x + marge.x,(mPivot.y + ((!Huds[i].ShowDistance) ? 10 : 20)) + marge.y, 25, 25)), Huds[i].m_Icon);
            }
            GUI.color = Color.white;
        }
    }

    //Add a new Huds from instance to the list
    public void CreateHud(HudInfo info)
    {
        huds.Add(info);
    }

    public void RemoveHud(int i)
    {
        huds.RemoveAt(i);
    }

    public void RemoveHud(HudInfo hud)
    {
        if (huds.Contains(hud))
        {
            huds.Remove(hud);
        }
        else
        {
            Debug.Log("Huds list don't contain this hud!");
        }
    }

    public void HideStateHud(int i,bool hide = false)
    {
        if (huds[i] != null)
        {
            huds[i].Hide = hide;
        }
    }


    public void HideStateHud(HudInfo hud, bool hide = false)
    {
        if (huds.Contains(hud))
        {
            for (int i = 0; i < huds.Count; i++)
            {
                if (huds[i] == hud)
                {
                    huds[i].Hide = hide;
                }
            }
        }
    }


    private float GetHudDistance(int i)
    {
        //if transform destroy, then remove from list
        if (huds[i] == null || huds[i].m_Target == null)
        {
            huds.Remove(huds[i]);
            return 0;
        }
        //Calculate Position of target
        Vector3 RelativePosition = huds[i].m_Target.position + huds[i].Offset;
        float Distance = Vector3.Distance(this.localPlayer.position, RelativePosition);
        return Distance;
    }

    private void Palpating(HudInfo hud)
    {
        if (hud.m_Color.a <= 0)
        {
            hud.tip = false;
        }
        else if (hud.m_Color.a >= 1)
        {
            hud.tip = true;
        }
        //Create a loop
        if (hud.tip == false)
        {
            hud.m_Color.a += Time.deltaTime * 0.5f;
        }
        else
        {
            hud.m_Color.a -= Time.deltaTime * 0.5f;
        }
    }

    // void OnDrawGizmosSelected()
    // {
    //     if (!useGizmos)
    //         return;
    //
    //     for (int i = 0; i < huds.Count; i++)
    //     {
    //         if (huds[i].m_Target != null)
    //         {
    //             Gizmos.color = new Color(0, 0.35f, 0.9f, 0.9f);
    //             Gizmos.DrawWireSphere(huds[i].m_Target.position, 3);
    //             Gizmos.color = new Color(0, 0.35f, 0.9f, 0.3f);
    //             Gizmos.DrawSphere(huds[i].m_Target.position, 3);
    //             if (i < huds.Count - 1)
    //             {
    //                 Gizmos.DrawLine(huds[i].m_Target.position, huds[i + 1].m_Target.position);
    //             }
    //             else
    //             {
    //                 Gizmos.DrawLine(huds[i].m_Target.position, huds[0].m_Target.position);
    //             }
    //         }
    //     }
    // }

}