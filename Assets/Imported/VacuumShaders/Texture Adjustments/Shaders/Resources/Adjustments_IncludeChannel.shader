Shader "Hidden/VacuumShaders/Texture Adjustments/Include Channel" 
{
	Properties 
	{
		_MainTex ("", 2D) = "" {}
		_Channels("", vector) = (1, 1, 1, 0)
	}
	
	CGINCLUDE
	#include "UnityCG.cginc" 
	#include "../cginc/Adjustments_Math.cginc"
	 
	sampler2D _MainTex;			
	float4 _MainTex_TexelSize;

sampler2D _AdjustedTexture;

//
float4 _Channels;


	float4 frag_adj (v2f_img i) : SV_Target     
	{   
		half4 srcColor = tex2D(_MainTex, i.uv);
		
		        
		#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			i.uv.y = 1 - i.uv.y;	
		#endif


		float4 c = lerp(srcColor, tex2D(_AdjustedTexture, i.uv), _Channels);


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
		    #pragma fragment frag_adj

		    ENDCG
		}  
	}

	Fallback off
	
} // shader