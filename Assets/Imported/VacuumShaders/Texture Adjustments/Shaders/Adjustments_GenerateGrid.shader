Shader "Hidden/VacuumShaders/Texture Adjustments/GenerateGrid"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
	
//
float4 _V_TA_GenerateGrid_CellSize;
float  _V_TA_GenerateGrid_GridSize;
float  _V_TA_GenerateGrid_GridIntensity;

float4 _V_TA_GenerateGrid_GridColor;
float4 _V_TA_GenerateGrid_BackgroundColor;


	half4 frag_adj (v2f_img i) : SV_Target     
	{     
		float2 uv = frac(i.uv * _V_TA_GenerateGrid_CellSize.xy);
		float2 gSize = min(1, _V_TA_GenerateGrid_GridSize.xx / _V_TA_GenerateGrid_CellSize.zw * _V_TA_GenerateGrid_CellSize.xy);
		float grid = 0;
		if(gSize.x > uv.x || (1 - gSize.x) < (uv.x) || gSize.y > uv.y || (1 - gSize.y) < (uv.y))
			grid = _V_TA_GenerateGrid_GridIntensity;



		float4 finalColor = lerp(_V_TA_GenerateGrid_BackgroundColor, _V_TA_GenerateGrid_GridColor, grid);


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
