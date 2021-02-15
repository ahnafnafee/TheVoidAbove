Shader "Hidden/VacuumShaders/Texture Adjustments/Channel Invert" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
		_Channels("", vector) = (1, 1, 1, 0)
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"

		 
	sampler2D _MainTex;	
	
//
float4 _Channels;


	float4 frag_adj (v2f_img i) : SV_Target     
	{           
		float4 srcColor = (tex2D(_MainTex, i.uv));   	
		
		float4 finalColor = lerp(srcColor, 1 - srcColor, _Channels);



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