Shader "Hidden/VacuumShaders/Texture Adjustments/GenerateShape"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
	
//
float2 _V_TA_GenerateShape_CountHW;
float2 _V_TA_GenerateShape_WidthHeight;
float  _V_TA_GenerateShape_Sides;
float  _V_TA_GenerateShape_Angle;
float _V_TA_GenerateShape_Thickness;

float4 _V_TA_GenerateShape_ShapeColor;
float4 _V_TA_GenerateShape_BackgroundColor;


	float Shape(float2 uv, float sides, float2 res)
    {		
        uv = (uv * 2 - 1) / (res * cos(PI / sides));
        uv.y *= -1;

        float pCoord = atan2(uv.x, uv.y);
        float r = PI_2 / sides;
        float distance = cos(floor(0.5 + pCoord / r) * r - pCoord) * length(uv);
        
		return saturate((1 - distance) / fwidth(distance));
    }

		

	half4 frag_adj (v2f_img i) : SV_Target     
	{     
		float2 uv = frac(i.uv * _V_TA_GenerateShape_CountHW);


		uv = Rotate2x2(uv, _V_TA_GenerateShape_Angle, float2(0.5, 0.5));

		float outRect  = Shape(uv, _V_TA_GenerateShape_Sides, _V_TA_GenerateShape_WidthHeight);
		float innerRect = Shape(uv, _V_TA_GenerateShape_Sides, _V_TA_GenerateShape_WidthHeight * (1 - _V_TA_GenerateShape_Thickness));

		float4 finalColor = lerp(_V_TA_GenerateShape_BackgroundColor, _V_TA_GenerateShape_ShapeColor, outRect * (1 - innerRect));
		
		
		
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
