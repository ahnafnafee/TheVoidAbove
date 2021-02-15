Shader "Hidden/VacuumShaders/Texture Adjustments/Flip" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
		_Flip("", vector) = (0, 0, 0, 0)
	}
	
	CGINCLUDE 
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	

//
float2 _Flip;

	float4 frag_adj (v2f_img i) : SV_Target     
	{           		
		float4 final =(tex2D(_MainTex, lerp(i.uv, i.uv * -1, _Flip)));


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