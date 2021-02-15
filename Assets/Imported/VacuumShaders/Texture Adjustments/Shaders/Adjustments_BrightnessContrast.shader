Shader "Hidden/VacuumShaders/Texture Adjustments/Brightness and Contrast" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"

sampler2D _MainTex;	
	
//Range(-100.0  100.0)      default = 0
half _V_TA_BrightnessContrast_Brightness;

//Range(-100.0  100.0)      default = 0
half _V_TA_BrightnessContrast_Contrast;

//Range(0.0  1.0)			default = 0.5
half4 _V_TA_BrightnessContrast_Coeff;	 



	half4 frag_adj (v2f_img i) : SV_Target     
	{      
		half4 srcColor = (tex2D(_MainTex, i.uv));   	

		half4 retColor = (srcColor * ((_V_TA_BrightnessContrast_Brightness + 100.0f) * 0.01f) - _V_TA_BrightnessContrast_Coeff) * ((_V_TA_BrightnessContrast_Contrast + 100.0f) * 0.01f) + _V_TA_BrightnessContrast_Coeff;

		retColor = saturate(retColor);


		ADJUST_COLOR_BY_COLOR_SPACE(retColor)
		return retColor;
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