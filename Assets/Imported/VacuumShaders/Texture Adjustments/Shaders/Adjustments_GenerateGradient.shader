Shader "Hidden/VacuumShaders/Texture Adjustments/GenerateGradient" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
	
//Gradient texture
sampler2D _V_TA_Gradient_Texture;

//
half _V_TA_Gradient_Reverse;

//
half _V_TA_Gradient_Frequency;

//
half _V_TA_Gradient_Phase;

//
half _V_TA_Gradient_Angle;

//
half _V_TA_Gradient_Gamma;

//
int _GradientTypeID;
	 
		

	float4 frag_adj (v2f_img i) : SV_Target     
	{    
		float2 uvRot = Rotate2x2(i.uv, _V_TA_Gradient_Angle, float2(0.5, 0.5));
		float2 remap = Remap(uvRot, 0, 1, -1, 1);
		float2 sinUvRot = sin(uvRot * 3.1415926);

		float xValues[4] = {0, 0, 0, 0};

		xValues[0] = uvRot.x;
		xValues[1] = length(remap);
		xValues[2] = min(sinUvRot.x, sinUvRot.y);
		xValues[3] = atan2(remap.r, remap.g) / 6.2831853 + 0.5;
		

		float uvX = xValues[_GradientTypeID];


		uvX = pow(uvX, _V_TA_Gradient_Gamma);

		uvX = uvX * _V_TA_Gradient_Frequency + _V_TA_Gradient_Phase;
		uvX = lerp(uvX, 1 - uvX, _V_TA_Gradient_Reverse);

		float4 finalColor = tex2D(_V_TA_Gradient_Texture, float2(uvX, 0.5));


		ADJUST_COLOR_BY_COLOR_SPACE(finalColor)
		return finalColor;
	}     

	ENDCG 
	
	Subshader 
	{
	    ZTest Always Cull Off ZWrite Off
	    Fog { Mode off } 
		
		Pass 
		{    
		    CGPROGRAM
		    #pragma vertex vert_img
		    #pragma fragment frag_adj

		    ENDCG
		}  
	}

	Fallback off
	
} // shader