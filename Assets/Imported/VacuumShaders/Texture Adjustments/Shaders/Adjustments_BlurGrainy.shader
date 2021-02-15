Shader "Hidden/VacuumShaders/Texture Adjustments/Blur Grainy"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
float4 _MainTex_TexelSize;
	
//Range(0, +)      default = 5
half _V_TA_BlurGrainy_Radius;

//Range(1, 64)      default = 1
int _V_TA_BlurGrainy_Samples; 


	half4 frag_adj (v2f_img i) : SV_Target     
	{           
		half4 finalColor = half4(0.0, 0.0, 0.0, 0.0);
		half radius = Random(i.uv);
		half2 offset = half2(0.0, 0.0);
	
		for(int k = 0; k < _V_TA_BlurGrainy_Samples; k++)
		{
			radius = frac(3712.65 * radius + 0.61432);
			offset = (radius - 0.5) * 2.0;
			radius = frac(3712.65 * radius + 0.61432);
			offset.y = (radius - 0.5) * 2.0;

			finalColor += tex2Dlod(_MainTex, half4(i.uv + offset * _V_TA_BlurGrainy_Radius, 0.0, 0.0));
		}

		finalColor = finalColor / _V_TA_BlurGrainy_Samples;


		ADJUST_COLOR_BY_COLOR_SPACE(finalColor)
		return finalColor;
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
			#pragma target 3.0

            ENDCG
        }
    }
}
