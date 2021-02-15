Shader "Hidden/VacuumShaders/Texture Adjustments/GeneratePixelatedNoise"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
    float4 _MainTex_TexelSize;
//
float2 _Scale;
float2 _Offset;
float _SquareAspectRation;

int _ColorMode;
float4 _Color1;
float4 _Color2;
sampler2D _GradientTexture;


	float4 frag_adj (v2f_img i) : SV_Target     
	{         
         		
        float aspectRatio = _MainTex_TexelSize.w / _MainTex_TexelSize.z;
        float2 newUV = _MainTex_TexelSize.z > _MainTex_TexelSize.w ? float2(1.0, aspectRatio) : float2(1.0 / aspectRatio, 1.0);
	    i.uv *= lerp(float2(1, 1), newUV, _SquareAspectRation);

     
        float r = Random(floor(i.uv * _Scale + _Offset));

        float4 colors[3] = {r.xxxx, lerp(_Color1, _Color2, r), tex2D(_GradientTexture, float2(r, 0.5))};


        float4 c = colors[_ColorMode];
         

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
			#pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag_adj

            ENDCG
        }
    }
}
