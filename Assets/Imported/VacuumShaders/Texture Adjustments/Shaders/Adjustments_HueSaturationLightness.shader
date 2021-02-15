Shader "Hidden/VacuumShaders/Texture Adjustments/Hue, Sauration and Lightness" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"		 

	sampler2D _MainTex;	
	
//Range(-180.0  180.0) - default = 0
float _V_TA_HueSaturationLightness_Hue;

//Range(-100.0  100.0) - default = 0
float _V_TA_HueSaturationLightness_Saturation;

//Range(-100.0  100.0) - default = 0
float _V_TA_HueSaturationLightness_Lightness;

 
//default = white
float4 _V_TA_HueSaturationLightness_KeyColor;

//Range(0, 6) - default = 2
float _V_TA_HueSaturationLightness_Threshold;

//Range(1, 16) - default = 1
float _V_TA_HueSaturationLightness_Boost;


//Range( 0.0,  360.0) - default = 0
float _V_TA_HueSaturationLightness_Angle;

//Range(-1.0,  1.0) - default = -0.25
float _V_TA_HueSaturationLightness_RangeMin;

//Range(-1.0,  1.0) - default = 0.25
float _V_TA_HueSaturationLightness_RangeMax;

//
int _SelectionTypeIndex;



	inline float4 Default(float4 _srcColor)
	{
		float H = _V_TA_HueSaturationLightness_Hue * -0.0174532;
		float S = _V_TA_HueSaturationLightness_Saturation <= 0.0f ? (_V_TA_HueSaturationLightness_Saturation / 100.0f + 1.0f) : (_V_TA_HueSaturationLightness_Saturation / 50.0f + 1.0f);
		float L = _V_TA_HueSaturationLightness_Lightness * 0.01f;

		float VSU = S * cos(H);
		float VSW = S * sin(H);

		float4 ret = float4(0, 0, 0, 0);
		ret.r = (0.299f + 0.701f * VSU + 0.168f * VSW) * _srcColor.r +
				(0.587f - 0.587f * VSU + 0.330f * VSW) * _srcColor.g +
				(0.114f - 0.114f * VSU - 0.497f * VSW) * _srcColor.b;
		ret.g = (0.299f - 0.299f * VSU - 0.328f * VSW) * _srcColor.r +
				(0.587f + 0.413f * VSU + 0.035f * VSW) * _srcColor.g +
				(0.114f - 0.114f * VSU + 0.292f * VSW) * _srcColor.b;
		ret.b = (0.299f - 0.3f * VSU + 1.25f * VSW) * _srcColor.r +
				(0.587f - 0.588f * VSU - 1.05f * VSW) * _srcColor.g +
				(0.114f + 0.886f * VSU - 0.203f * VSW) * _srcColor.b;
				 

		//Lightness
		ret = lerp(ret * (1 + L), (1.0 - ret) * L + ret, step(0, L));
		

		ret.a = _srcColor.a;

		return ret;
	}


	inline float4 SelectiveByColor(float4 _srcColor, float4 hsl)
	{	
		fixed diff = saturate (_V_TA_HueSaturationLightness_Threshold * length (_srcColor.rgb - _V_TA_HueSaturationLightness_KeyColor.rgb));
	
		return lerp (hsl, _srcColor, pow(diff, _V_TA_HueSaturationLightness_Boost));
	}
 

	inline float4 SelectiveByRange(float4 _srcColor, float4 hsl)
	{	
		//hue range value
		float h = RGBtoHUE(_srcColor.rgb);
	 
		if (_V_TA_HueSaturationLightness_RangeMax > 1.0 && h < _V_TA_HueSaturationLightness_RangeMax - 1.0) h += 1.0;
		if (_V_TA_HueSaturationLightness_RangeMin < 0.0 && h > _V_TA_HueSaturationLightness_RangeMin + 1.0) h -= 1.0;
		

		float2 smoothStep = smoothstep(float2(_V_TA_HueSaturationLightness_Angle, _V_TA_HueSaturationLightness_RangeMin), float2(_V_TA_HueSaturationLightness_RangeMax, _V_TA_HueSaturationLightness_Angle), h);

		_srcColor.rgb = lerp(lerp(hsl.rgb, _srcColor.rgb, smoothStep.x),
							 lerp(_srcColor.rgb, hsl.rgb, smoothStep.y), 
							 step(h, _V_TA_HueSaturationLightness_Angle)); 

		return _srcColor; 
	}
	
	float4 frag_adj (v2f_img i) : SV_Target     
	{          
		float4 mainColor = tex2D(_MainTex, i.uv);
		float4 hsl = Default(mainColor);

		float4 values[3] = {hsl, SelectiveByColor(mainColor, hsl), SelectiveByRange(mainColor, hsl)};


		float4 c = values[_SelectionTypeIndex];


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
			#pragma target 3.0

		    ENDCG
		}  
	}

	Fallback off
	
} // shader