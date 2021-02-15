Shader "Hidden/VacuumShaders/Texture Adjustments/Noise"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
		_Monochromatic("", Float) = 0
		_Luminance("", Float) = 0
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;
	
//Range(0, 1)      default = 0.1
half _V_TA_Noise_Strength;

//Range(0, 1)      default = 0
half _V_TA_Noise_LumContribution; 

//
float _Monochromatic;
float _Luminance;



	half4 frag_adj (v2f_img i) : SV_Target     
	{           
		half4 srcColor = tex2D(_MainTex, i.uv);
		half n = Random(i.uv);
		

		half4 nColor = srcColor * 2.0 * lerp(half4(frac(n), frac(n * 1.2154), frac(n * 1.3453), frac(n * 1.3647)), n, _Monochromatic);

		float lum = (1.0 - lerp(0.0, dot(srcColor.rgb, half3(0.222, 0.707, 0.071)), _V_TA_Noise_LumContribution));
		_V_TA_Noise_Strength *= lerp(1, lum, _Luminance);


		float4 final = lerp(srcColor, nColor, _V_TA_Noise_Strength);


		ADJUST_COLOR_BY_COLOR_SPACE(final)
		return final;
	}     


    ENDCG

    Subshader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_adj

            ENDCG
        }
    }
}
