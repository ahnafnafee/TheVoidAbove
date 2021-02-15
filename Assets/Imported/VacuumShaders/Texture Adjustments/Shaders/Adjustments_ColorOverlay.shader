Shader "Hidden/VacuumShaders/Texture Adjustments/Color Overaly" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
float4 _MainTex_TexelSize;
	 
//default = Color.green
fixed4 _V_TA_ColorOvarLay_Color;

//Default = White (1)
sampler2D _V_TA_ColorOvarLay_ColorTexure;

//Default = (1, 1, 0, 0)
float4 _V_TA_ColorOvarLay_ColorTexureTilingOffset; 

//Range(0.0f, 1.0)    default = 0.5
float _V_TA_ColorOvarLay_Intensity; 
 
	 

	float4 frag_adj (v2f_img i) : SV_Target     
	{        
		float4 srcColor = (tex2D(_MainTex, i.uv));   		
		half4 finalColor = srcColor;


		#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			i.uv.y = 1 - i.uv.y;	
		#endif


		half4 overlayColor = _V_TA_ColorOvarLay_Color * tex2D(_V_TA_ColorOvarLay_ColorTexure, i.uv * _V_TA_ColorOvarLay_ColorTexureTilingOffset.xy + _V_TA_ColorOvarLay_ColorTexureTilingOffset.zw);


		#if defined(COLOROVERLAY_DARKEN)
			finalColor = min(overlayColor, srcColor);
		#elif defined(COLOROVERLAY_MULTIPLY)
			finalColor = overlayColor * srcColor;
		#elif defined(COLOROVERLAY_COLORBURN)
			finalColor = 1.0 - (1.0 - srcColor) / overlayColor;
		#elif defined(COLOROVERLAY_LINEARBURN)
			finalColor = overlayColor + srcColor - 1.0;
		#elif defined(COLOROVERLAY_LIGHTEN)
			finalColor = max(overlayColor, srcColor);
		#elif defined(COLOROVERLAY_SCREEN)
			finalColor = 1.0 - (1.0 - overlayColor) * (1.0 - srcColor);
		#elif defined(COLOROVERLAY_COLORDODGE)
			finalColor = srcColor / (1.0 - overlayColor);
		#elif defined(COLOROVERLAY_LINEARDODGE)
			finalColor = overlayColor + srcColor;    
		#elif defined(COLOROVERLAY_OVERLAY)
			finalColor = lerp((1.0 - (1.0 - 2.0f * (srcColor - 0.5)) * (1.0 - overlayColor)), (2.0f * srcColor * overlayColor), step(srcColor, 0.5));
		#elif defined(COLOROVERLAY_HARDLIGHT)
			finalColor = lerp((1.0 - (1.0 - 2.0f * (overlayColor - 0.5)) * (1.0 - srcColor)), (2.0f * overlayColor * srcColor), step(overlayColor, 0.5));
		#elif defined(COLOROVERLAY_VIVIDLIGHT)
			finalColor = lerp(srcColor / ((1.0 - overlayColor) * 2.0f), 1.0 - ((1.0 - srcColor) * 0.5f) / overlayColor, step(overlayColor, 0.5));
		#elif defined(COLOROVERLAY_LINEARLIGHT)
			finalColor = lerp((srcColor + 2.0f * overlayColor - 1.0), (srcColor + 2.0f * (overlayColor - 0.5)), step(overlayColor, 0.5));
		#elif defined(COLOROVERLAY_PINLIGHT)
			finalColor = lerp(max(srcColor, 2.0f * (overlayColor - 0.5)), min(srcColor, 2.0f * overlayColor), step(overlayColor, 0.5));
		#elif defined(COLOROVERLAY_HARDMIX)
			finalColor = round(0.5f * (overlayColor + srcColor));
		#elif defined(COLOROVERLAY_DIFFERENCE)
			finalColor = abs(overlayColor - srcColor);
		#elif defined(COLOROVERLAY_EXCLUSION)
			finalColor = 0.5 - 2.0f * (overlayColor - 0.5) * (srcColor - 0.5);
		#elif defined(COLOROVERLAY_SUBTRACT)
			finalColor = srcColor - overlayColor; 
		#elif defined(COLOROVERLAY_DIVIDE)  
			finalColor = srcColor / overlayColor; 
    
 
		#elif defined(COLOROVERLAY_HUE) 
			float3 srcColorHSL = RGBToHSL(srcColor);
		    finalColor.rgb = HSLToRGB(float3(RGBToHSL(overlayColor).r, srcColorHSL.g, srcColorHSL.b));
			
		#elif defined(COLOROVERLAY_SATURATION) 
			float3 srcColorHSL = RGBToHSL(srcColor);
			finalColor.rgb = HSLToRGB(float3(srcColorHSL.r, RGBToHSL(overlayColor).g, srcColorHSL.b));

		#elif defined(COLOROVERLAY_COLOR)
			float3 overlayColorHSL = RGBToHSL(overlayColor);
			finalColor.rgb = HSLToRGB(float3(overlayColorHSL.r, overlayColorHSL.g, RGBToHSL(srcColor).b));
			
		#elif defined(COLOROVERLAY_LUMINOSITY)
			float3 baseHSL = RGBToHSL(srcColor);
			finalColor.rgb = HSLToRGB(float3(baseHSL.r, baseHSL.g, RGBToHSL(overlayColor).b));


		#elif defined(COLOROVERLAY_WATERMARK)
			finalColor.rgb = lerp(finalColor.rgb, overlayColor.rgb, overlayColor.a);
		#else
			finalColor = overlayColor;			
		#endif

							
		finalColor = lerp(srcColor, saturate(finalColor), _V_TA_ColorOvarLay_Intensity);


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

			#pragma shader_feature COLOROVERLAY_NORMAL COLOROVERLAY_DARKEN COLOROVERLAY_MULTIPLY COLOROVERLAY_COLORBURN COLOROVERLAY_LINEARBURN COLOROVERLAY_LIGHTEN COLOROVERLAY_SCREEN COLOROVERLAY_COLORDODGE COLOROVERLAY_LINEARDODGE COLOROVERLAY_OVERLAY COLOROVERLAY_HARDLIGHT COLOROVERLAY_VIVIDLIGHT COLOROVERLAY_LINEARLIGHT COLOROVERLAY_PINLIGHT COLOROVERLAY_HARDMIX COLOROVERLAY_DIFFERENCE COLOROVERLAY_EXCLUSION COLOROVERLAY_SUBTRACT COLOROVERLAY_DIVIDE COLOROVERLAY_HUE COLOROVERLAY_SATURATION COLOROVERLAY_COLOR COLOROVERLAY_LUMINOSITY COLOROVERLAY_WATERMARK

		    ENDCG
		}  
	}

	Fallback off
	
} // shader