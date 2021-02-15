Shader "Hidden/VacuumShaders/Texture Adjustments/Rotator" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	

//
float4 _V_TA_Rotator_ST;
	
//
half _V_TA_Rotator_Angle;

//float2 
float2 _V_TA_Rotator_PivotPoint;
	 

	float4 frag_adj (v2f_img i) : SV_Target     
	{      
		float2 uv = Rotate2x2(i.uv, _V_TA_Rotator_Angle, _V_TA_Rotator_PivotPoint);

		float4 finalColor = tex2D(_MainTex, uv * _V_TA_Rotator_ST.xy + _V_TA_Rotator_ST.zw);


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