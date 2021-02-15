Shader "Hidden/VacuumShaders/Texture Adjustments/Channel Import" 
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
float4 _MainTex_TexelSize;

sampler2D _V_TA_ChannelImport_SourceTexture;

//
float4 _ChannelsEnabled;
int _Red; 
int _Green;
int _Blue;
int _Alpha;
 

	half4 frag_adj (v2f_img i) : SV_Target     
	{         
		half4 finalColor = tex2D(_MainTex, i.uv);  
		
		    	
		#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0)
				i.uv.y = 1 - i.uv.y;	
		#endif

		half4 srcColor = tex2D(_V_TA_ChannelImport_SourceTexture, i.uv);   	

		finalColor = lerp(finalColor, half4(srcColor[_Red], srcColor[_Green], srcColor[_Blue], srcColor[_Alpha]), _ChannelsEnabled);



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