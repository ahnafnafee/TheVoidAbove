Shader "Hidden/VacuumShaders/Texture Adjustments/Twirl" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	 

//
float4 _V_TA_Twirl_ST;
	
//  
half _V_TA_Twirl_Strength;

//float2 
float2 _V_TA_Twirl_PivotPoint;
	 

	float4 frag_adj (v2f_img i) : SV_Target     
	{          
		
		float2 delta = i.uv - _V_TA_Twirl_PivotPoint;
		float angle = _V_TA_Twirl_Strength * length(delta) * 0.0174533;
		float x = cos(angle) * delta.x - sin(angle) * delta.y;
		float y = sin(angle) * delta.x + cos(angle) * delta.y;
		float2 uv = float2(x + _V_TA_Twirl_PivotPoint.x, y + _V_TA_Twirl_PivotPoint.y);
				
		float4 finalColor = tex2D(_MainTex, uv * _V_TA_Twirl_ST.xy + _V_TA_Twirl_ST.zw);


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