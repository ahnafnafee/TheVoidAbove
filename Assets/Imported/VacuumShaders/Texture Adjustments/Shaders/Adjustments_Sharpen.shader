Shader "Hidden/VacuumShaders/Texture Adjustments/Sharpen"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
	
//Range(0, 10)      default = 2
half _V_TA_Sharpen_Strength;

//Width & Height
half2 _V_TA_MainTex_Dimension;

	half4 frag_adj (v2f_img i) : SV_Target     
	{   
		half4 blur =  tex2D(_MainTex, i.uv + half2(-_V_TA_MainTex_Dimension.x, -_V_TA_MainTex_Dimension.y) * 1.5);
			  blur += tex2D(_MainTex, i.uv + half2( _V_TA_MainTex_Dimension.x, -_V_TA_MainTex_Dimension.y) * 1.5);
			  blur += tex2D(_MainTex, i.uv + half2(-_V_TA_MainTex_Dimension.x,  _V_TA_MainTex_Dimension.y) * 1.5);
			  blur += tex2D(_MainTex, i.uv + half2( _V_TA_MainTex_Dimension.x,  _V_TA_MainTex_Dimension.y) * 1.5);
			  blur *= 0.25;

		half4 srcColor = tex2D(_MainTex, i.uv);

		float4 finalColor = srcColor + (srcColor - blur) * _V_TA_Sharpen_Strength;


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
