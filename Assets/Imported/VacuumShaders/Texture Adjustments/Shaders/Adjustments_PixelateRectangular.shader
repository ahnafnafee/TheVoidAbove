Shader "Hidden/VacuumShaders/Texture Adjustments/PixelateRectangular"
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
float4 _V_TA_PixelateRectangular_CellSize;

//
float _V_TA_PixelateRectangular_GridSize;
float4 _V_TA_PixelateRectangular_GridColor;
float  _V_TA_PixelateRectangular_GridIntensity;


	half4 frag_adj (v2f_img i) : SV_Target     
	{     
		float2 res = 1.0 / _V_TA_PixelateRectangular_CellSize.xy;

		float2 celUV = floor(i.uv / res) * res;

		float4 finalColor = tex2D(_MainTex, celUV);
		


		//Grid	
		float2 uv = frac(i.uv * _V_TA_PixelateRectangular_CellSize.xy);
		float2 gSize = min(1, _V_TA_PixelateRectangular_GridSize.xx / _V_TA_PixelateRectangular_CellSize.zw * _V_TA_PixelateRectangular_CellSize.xy);
		float grid = 0;
		if(gSize.x > uv.x || (1 - gSize.x) < (uv.x) || gSize.y > uv.y || (1 - gSize.y) < (uv.y))
			grid = _V_TA_PixelateRectangular_GridIntensity;



		finalColor.rgb = lerp(finalColor.rgb, _V_TA_PixelateRectangular_GridColor.rgb, grid);


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
