Shader "Hidden/VacuumShaders/Texture Adjustments/Gradient Ramp" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
	
//Ramp texture
sampler2D _V_TA_GradientRamp_Tex;

//Range(-∞  ∞)     default = 0
half _V_TA_GradientRamp_Offset;

//Range(0.0  1.0)  default = 0
half _V_TA_GradientRamp_Intensity; 

	 

	float4 frag_adj (v2f_img i) : SV_Target     
	{           
		float4 srcColor = (tex2D(_MainTex, i.uv));   			 
 
		half2 uv = half2 (Luminance(srcColor) + _V_TA_GradientRamp_Offset, 0.5);

		float4 finalColor = lerp(srcColor, tex2D(_V_TA_GradientRamp_Tex, uv), _V_TA_GradientRamp_Intensity);


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