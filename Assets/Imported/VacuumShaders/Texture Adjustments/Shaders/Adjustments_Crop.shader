Shader "Hidden/VacuumShaders/Texture Adjustments/Crop" 
{
	Properties 
	{
		_MainTex ("Main Tex", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
	float2 _MainTexOffset;
	float2 _MainTexTilling;
		
	float4 frag (v2f_img i) : SV_Target     
	{           		
		float4 c = tex2D(_MainTex, i.uv * _MainTexTilling + _MainTexOffset);


		ADJUST_COLOR_BY_COLOR_SPACE(c)
		return c;
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
		    #pragma fragment frag

		    ENDCG
		}  
	}

	Fallback off
	
} // shader