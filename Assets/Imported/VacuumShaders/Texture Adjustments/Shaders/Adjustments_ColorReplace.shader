Shader "Hidden/VacuumShaders/Texture Adjustments/Color Replace" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"
		 

	sampler2D _MainTex;	
	
//default = white
half4 _V_TA_ReplaceColor_KeyColor; 

//Range(0, 6) - default = 2
half _V_TA_ReplaceColor_Threshold;

//Range(1, 16) - default = 1
half _V_TA_ReplaceColor_Boost;
		

//Range( 0.0,  360.0)    default = 0
half _V_TA_ReplaceColor_Angle;

//Range(-1.0,  1.0)      default = -0.25
half _V_TA_ReplaceColor_RangeMin;

//Range(-1.0,  1.0)      default = 0.25
half _V_TA_ReplaceColor_RangeMax; 


//default = white
half4 _V_TA_ReplaceColor_TargetColor; 

//Range(0.0,  1.0)       default = 1
half _V_TA_ReplaceColor_Intensity; 
 
//
int _SelectionTypeIndex;

	 
	inline float4 ReplaceColorByColor(float4 _srcColor)
	{
		half diff = saturate (_V_TA_ReplaceColor_Threshold * length (_srcColor.rgb - _V_TA_ReplaceColor_KeyColor.rgb));
	
		half4 c = lerp (_V_TA_ReplaceColor_TargetColor, _srcColor, pow(diff, _V_TA_ReplaceColor_Boost));
		c.a = _srcColor.a;

		return lerp(_srcColor, c, _V_TA_ReplaceColor_Intensity);
	} 


	inline float4 ReplaceColorByRange(float4 _srcColor)
	{
		//hue range value
		float h = RGBtoHUE(_srcColor.rgb);
	
		if (_V_TA_ReplaceColor_RangeMax > 1.0 && h < _V_TA_ReplaceColor_RangeMax - 1.0) h += 1.0;
		if (_V_TA_ReplaceColor_RangeMin < 0.0 && h > _V_TA_ReplaceColor_RangeMin + 1.0) h -= 1.0;

		float2 smoothStep = smoothstep(float2(_V_TA_ReplaceColor_Angle, _V_TA_ReplaceColor_RangeMin), float2(_V_TA_ReplaceColor_RangeMax, _V_TA_ReplaceColor_Angle), h);
						
		half4 c = 0;
		c.rgb = lerp(lerp(_V_TA_ReplaceColor_TargetColor.rgb, _srcColor.rgb, smoothStep.x),
					 lerp(_srcColor.rgb, _V_TA_ReplaceColor_TargetColor.rgb, smoothStep.y),
					 step(h, _V_TA_ReplaceColor_Angle)); 
		c.a = _srcColor.a;

		return lerp(_srcColor, c, _V_TA_ReplaceColor_Intensity);
	}
	 


	float4 frag_adj (v2f_img i) : SV_Target     
	{   
		float4 mainTex = tex2D(_MainTex, i.uv);
		float4 c = lerp(ReplaceColorByColor(mainTex), ReplaceColorByRange(mainTex), _SelectionTypeIndex);
			

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