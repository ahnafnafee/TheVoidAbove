Shader "Hidden/VacuumShaders/Texture Adjustments/Mask" 
{
	Properties 
	{
		_MainTex ("", 2D) = "" {}
		_Channels("", Float) = 1
	}
	
	CGINCLUDE
	#include "UnityCG.cginc" 
	#include "../cginc/Adjustments_Math.cginc"
	 
	sampler2D _MainTex;	
	float4 _MainTex_TexelSize;
		
//Default = Black (0)
sampler2D _V_TA_MaskTexure;

//Default = (1, 1, 0, 0)
float4 _V_TA_MaskTexureTilingOffset;

//Default = false
half _V_TA_MaskInvert;

//Default = 0
half _V_TA_MaskStrength; 

sampler2D _AdjustedTexture;

// 
int _Channels;


	half ExtractMask(float2 _uv)
	{	
		float4 t = tex2D(_V_TA_MaskTexure, _uv * _V_TA_MaskTexureTilingOffset.xy + _V_TA_MaskTexureTilingOffset.zw);
		
		float value[5] = {t.r, t.g, t.b, t.a, Luminance(t)};
		half mask = value[_Channels];
		
		mask = lerp(mask, 1 - mask, _V_TA_MaskInvert);
		mask = saturate(mask + _V_TA_MaskStrength);
		
		return mask;
	}

	float4 frag_adj (v2f_img i) : SV_Target     
	{      		
		half4 srcColor = tex2D(_MainTex, i.uv);

				
		#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			i.uv.y = 1 - i.uv.y;	
		#endif

		
		float4 c = lerp(srcColor, tex2D(_AdjustedTexture, i.uv), ExtractMask(i.uv));


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