Shader "Hidden/VacuumShaders/Texture Adjustments/GenerateCheckerboard"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
    float4 _MainTex_TexelSize;

//
float _V_TA_Checkerboard_Scale;

//Colors
float4 _V_TA_Checkerboard_Color1;
float4 _V_TA_Checkerboard_Color2;



	float4 frag_adj (v2f_img i) : SV_Target     
	{           		
		float aspectRatio = _MainTex_TexelSize.w / _MainTex_TexelSize.z;
		i.uv *= _MainTex_TexelSize.z > _MainTex_TexelSize.w ? float2(1.0, aspectRatio) : float2(1.0 / aspectRatio, 1.0);
	 
		float2 uv = floor(i.uv * _V_TA_Checkerboard_Scale) / 2;
        float checker = (frac(uv.x + uv.y) * 2) > 0.5 ? 1 : 0;

        float4 c = lerp(_V_TA_Checkerboard_Color1, _V_TA_Checkerboard_Color2, checker);


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
