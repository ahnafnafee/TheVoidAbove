Shader "Hidden/VacuumShaders/Texture Adjustments/Grayscale"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
	
//Range(0, 1)      default = (0.222, 0.707, 0.071)
half3 _V_TA_Grayscale_Luminance;

//Range(0, 1)      default = 1
half _V_TA_Grayscale_Strength;

	half4 frag_adj (v2f_img i) : SV_Target     
	{           
		half4 srcColor = tex2D(_MainTex, i.uv);
		half lum = dot(srcColor.rgb, _V_TA_Grayscale_Luminance);
		
		float4 c = lerp(srcColor, half4(lum, lum, lum, srcColor.a), _V_TA_Grayscale_Strength);


		ADJUST_COLOR_BY_COLOR_SPACE(c)
		return c;
	}     


    ENDCG

    Subshader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_adj

            ENDCG
        }
    }
}
