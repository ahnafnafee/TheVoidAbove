Shader "Hidden/VacuumShaders/Texture Adjustments/Rotate" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
		_Angle("", vector) = (0, 0, 0, 0)
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	

//
float _Angle;

	float4 frag_adj (v2f_img i) : SV_Target     
	{      
		i.uv = Rotate2x2(i.uv, _Angle, float2(0.5, 0.5));						     		
		float4 finalColor = tex2D(_MainTex, i.uv);

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