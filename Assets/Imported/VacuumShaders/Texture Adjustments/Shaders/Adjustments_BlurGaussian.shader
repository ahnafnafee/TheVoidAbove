Shader "Hidden/VacuumShaders/Texture Adjustments/Blur Gaussian"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"



    sampler2D _MainTex;
    float4 _MainTex_TexelSize;

	sampler2D _Original;

		
    half4 GaussianBlur(float2 uv, float2 coeff)
    {
        half4 s = tex2D(_MainTex, uv) * 0.2271;

        float2 offset = coeff * 1.3846;
        s += (tex2D(_MainTex, uv + offset) + tex2D(_MainTex, uv - offset)) * 0.3162;

        offset = coeff * 3.2308;
        s += (tex2D(_MainTex, uv + offset) + tex2D(_MainTex, uv - offset)) * 0.0703;

        return s;
    }
	    

    half4 frag_h(v2f_img i) : SV_Target
    { 
        float4 c = GaussianBlur(i.uv, float2(_MainTex_TexelSize.x, 0));


		ADJUST_COLOR_BY_COLOR_SPACE(c)
		return c;
    }

    half4 frag_v(v2f_img i) : SV_Target
    {
        float4 c = GaussianBlur(i.uv, float2(0, _MainTex_TexelSize.y));


		ADJUST_COLOR_BY_COLOR_SPACE(c)
		return c;
    }

    ENDCG

    Subshader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_h

            ENDCG
        }
        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_v			

            ENDCG
        }
    }
}
