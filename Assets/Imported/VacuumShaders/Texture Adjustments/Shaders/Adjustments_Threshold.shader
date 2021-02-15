Shader "Hidden/VacuumShaders/Texture Adjustments/Threshold"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
		_Noise("", Float) = 0
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
	
//Range(0, 1)      default = 0.5
half _V_TA_Threshold_Level;

//Range(0, 1)      default = 0
half _V_TA_Threshold_Noise; 

//
float _Noise;


	half4 frag_adj (v2f_img i) : SV_Target     
	{           
		
		half4 srcColor = tex2D(_MainTex, i.uv);		 

		half n = Random(i.uv) * _V_TA_Threshold_Noise - _V_TA_Threshold_Noise / 2.0;
		half s = step(dot(srcColor.rgb, half3(0.222, 0.707, 0.071)), _V_TA_Threshold_Level + lerp(0, n, _Noise));

		float4 finalColor = lerp(1, 0, s);


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
			
            ENDCG
        }
    }
}
