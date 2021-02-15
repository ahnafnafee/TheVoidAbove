Shader "Hidden/VacuumShaders/Texture Adjustments/GenerateSimpleNoise"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	

//
float V_TA_SimpleNoise_Monochromatic;

	float4 frag_adj (v2f_img i) : SV_Target     
	{           		
		float4 c = float4(Random(i.uv), Random(i.uv + float2(23.25, 0.05)), Random(i.uv + float2(3.26, 8.6)), Random(i.uv + float2(6.124, 23)));
        c = V_TA_SimpleNoise_Monochromatic > 0.5 ? c.rrrr : c;

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
			#pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag_adj

            ENDCG
        }
    }
}
