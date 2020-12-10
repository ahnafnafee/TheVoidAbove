using UnityEngine;
using UnityEngine.Serialization;

public class Hud : MonoBehaviour {

    public HudInfo hudInfo;

    void Start()
    {
        if (HudManager.instance != null)
        {
            if (hudInfo.m_Target == null) { hudInfo.m_Target = this.GetComponent<Transform>(); }
            if (hudInfo.ShowDynamically) { hudInfo.Hide = true; }
            HudManager.instance.CreateHud(this.hudInfo);
        }
        else
        {
            Debug.LogError("Need have a Hud Manager in scene");
        }
    }

    public void Show()
    {
        if (HudManager.instance != null)
        {
            HudManager.instance.HideStateHud(hudInfo, false);
        }
        else
        {
            Debug.LogWarning("the instance of HudManager in scene wasn't found.");
        }
    }

    public void Hide()
    {
        if (HudManager.instance != null)
        {
            HudManager.instance.HideStateHud(hudInfo, true);
        }
        else
        {
            Debug.LogWarning("the instance of HudManager in scene wasn't found.");
        }
    }

}