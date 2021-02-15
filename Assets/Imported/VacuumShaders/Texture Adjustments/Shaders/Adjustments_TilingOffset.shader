Shader "Hidden/VacuumShaders/Texture Adjustments/Tiling and Offset" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE	
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
	
//Default = (1, 1, 0, 0)
half4 _V_TA_TilingOffset_ST;

	float4 frag_adj (v2f_img i) : SV_Target     
	{       
		float4 finalColor = tex2D(_MainTex, i.uv * _V_TA_TilingOffset_ST.xy + _V_TA_TilingOffset_ST.zw);


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