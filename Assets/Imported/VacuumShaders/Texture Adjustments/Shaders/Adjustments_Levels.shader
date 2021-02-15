Shader "Hidden/VacuumShaders/Texture Adjustments/Levels" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
	
//Range(0.0  _V_TA_Levels_InputMax)     default = 0
float4 _V_TA_Levels_InputMin; 

//Range(_V_TA_Levels_InputMin  1.0)     default = 1
float4 _V_TA_Levels_InputMax;

//Range(0.1  3.0)                       default = 1
float4 _V_TA_Levels_InputGamma;

//Range(0.0  _V_TA_Levels_OutputMax)    default = 0
float4 _V_TA_Levels_OutputMin;

//Range(_V_TA_Levels_OutputMin  1.0)    default = 1
float4 _V_TA_Levels_OutputMax;

	 

	float4 frag_adj (v2f_img i) : SV_Target     
	{           
		float4 srcColor = (tex2D(_MainTex, i.uv));   	

		float4 a = max(srcColor - _V_TA_Levels_InputMin, 0) / max(0.001, _V_TA_Levels_InputMax - _V_TA_Levels_InputMin); 
	
		float4 p = pow(min(a, 1), 1.0 / _V_TA_Levels_InputGamma); 
	
		float4 final = lerp(_V_TA_Levels_OutputMin, _V_TA_Levels_OutputMax, p);


		ADJUST_COLOR_BY_COLOR_SPACE(final)
		return final;
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