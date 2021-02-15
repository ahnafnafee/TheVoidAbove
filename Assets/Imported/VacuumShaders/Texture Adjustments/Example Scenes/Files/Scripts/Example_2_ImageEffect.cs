using UnityEngine;
using System.Collections;

using VacuumShaders.TextureAdjustments;

[ExecuteInEditMode]
public class Example_2_ImageEffect : MonoBehaviour 
{
    //Variables //////////////////////////////////////////////////////////////////
    public UnityEngine.UI.Text uiText;

    public Adjust_BlurGaussian blur = new Adjust_BlurGaussian();
    public Adjust_Grayscale grayscale = new Adjust_Grayscale();
    public Adjust_Noise noise = new Adjust_Noise();
    public Adjust_HueSaturateLightness hue = new Adjust_HueSaturateLightness();
    public Adjust_TilingOffset offset = new Adjust_TilingOffset();

    RenderTexture adjstedTexture;
    //Unity Functions ////////////////////////////////////////////////////////////
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //Saving time before rendering 
        float t = Time.realtimeSinceStartup;

        //All rendering happens here
        TextureAdjustments.RenderAll(src, ref adjstedTexture, false, blur, grayscale, noise, hue, offset);

        //Calculating Texture Adustments working speed
        if (uiText != null)
            uiText.text = "Rendering speed: " + (Time.realtimeSinceStartup - t).ToString("f6") + "sec";


        //Coping final texture into 'dest' for Unity
        Graphics.Blit(adjstedTexture, dest);
    }
}
