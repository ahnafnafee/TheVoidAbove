Shader "Hidden/VacuumShaders/Texture Adjustments/Blur Directional"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
	
//Range(1  32)      default = 5
half _V_TA_BlurDirectional_Samples;

//default = 0
half2 _V_TA_BlurDirectional_Direction; 
 
  

	half4 frag_adj (v2f_img i) : SV_Target     
	{         
		half4 finalColor = half4(0.0, 0.0, 0.0, 0.0);

		for (int j = -_V_TA_BlurDirectional_Samples; j < _V_TA_BlurDirectional_Samples; j++)
			finalColor += tex2Dlod(_MainTex, half4(i.uv - _V_TA_BlurDirectional_Direction * j, 0.0, 0.0));

		finalColor = finalColor / (_V_TA_BlurDirectional_Samples * 2.0);


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
