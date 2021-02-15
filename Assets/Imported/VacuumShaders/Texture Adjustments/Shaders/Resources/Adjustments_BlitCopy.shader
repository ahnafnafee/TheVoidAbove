Shader "Hidden/VacuumShaders/Texture Adjustments/BlitCopy" 
{
	Properties 
	{
		_MainTex ("Texture", any) = "" {}
	}
	
	CGINCLUDE
	#include "UnityCG.cginc" 
	#include "../cginc/Adjustments_Math.cginc"
		 
sampler2D _MainTex;	

	float4 frag_adj (v2f_img i) : SV_Target     
	{           		 
		float4 c = tex2D(_MainTex, i.uv);


		#ifdef UNITY_COLORSPACE_GAMMA

			return c;
			 
		#else
		
			float4 adjustedColor = c;
			ADJUST_COLOR_BY_COLOR_SPACE(adjustedColor)

			return adjustedColor;

		#endif
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