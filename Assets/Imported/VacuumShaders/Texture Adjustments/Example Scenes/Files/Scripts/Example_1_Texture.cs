using UnityEngine;
using System.Collections;

using VacuumShaders.TextureAdjustments;

public class Example_1_Texture : MonoBehaviour
{
    //Variables //////////////////////////////////////////////////////////////////
    public bool useRenderTexture;

    public Adjust_GenerateGradientNoise noise = new Adjust_GenerateGradientNoise();

    public Adjust_HueSaturateLightness hue = new Adjust_HueSaturateLightness();
    public Adjust_BlurGaussian blur = new Adjust_BlurGaussian();
    public Adjust_BrightnessContrast contrast = new Adjust_BrightnessContrast();    


    Material activeMaterial;
    Texture originalTexture;

    Texture2D adjTexture_T2D;
    RenderTexture adjTexture_RT;
    //Functions //////////////////////////////////////////////////////////////////
    void Start()
    {
        activeMaterial = GetComponent<Renderer>().material;

        originalTexture = activeMaterial.mainTexture;
    }

    void Update()
    {
        //Not optimized!
        //It is better to calculate adjustments manually and not in every 'Update'

        if (useRenderTexture)
        {
            //Adjusting 'originalTexture' and saving results in 'adjustedTexture'
            TextureAdjustments.RenderAll(originalTexture, ref adjTexture_RT, false, noise, hue, blur, contrast);

            //Assigning 'adjustedTexture' to the material
            activeMaterial.mainTexture = adjTexture_RT;
        }
        else
        {
            //Adjusting 'originalTexture' and saving results in 'adjustedTexture'
            TextureAdjustments.RenderAll(originalTexture, ref adjTexture_T2D, noise, hue, blur, contrast);

            //Assigning 'adjustedTexture' to the material
            activeMaterial.mainTexture = adjTexture_T2D;
        }
    }
}
