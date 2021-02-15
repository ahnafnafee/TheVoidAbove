Shader "Hidden/VacuumShaders/Texture Adjustments/Channel Swap" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
		_ChannelsEnabled("", vector) = (0, 0, 0, 0)
		_Red("", Float) = 0
		_Green("", Float) = 0
		_Blue("", Float) = 0
		_Alpha("", Float) = 0
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"

		 
	sampler2D _MainTex;	
	
//
float _Red; 
float _Green;
float _Blue;
float _Alpha;


	float4 frag_adj (v2f_img i) : SV_Target     
	{           
		float4 srcColor = (tex2D(_MainTex, i.uv));   	

		float values[10] = {srcColor.r, 1 - srcColor.r, srcColor.g, 1 - srcColor.g, srcColor.b, 1 - srcColor.b, srcColor.a, 1 - srcColor.a, 1, 0};


		float4 finalColor = half4(values[_Red], values[_Green], values[_Blue], values[_Alpha]);

		
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