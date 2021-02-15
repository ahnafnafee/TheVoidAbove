Shader "Hidden/VacuumShaders/Texture Adjustments/GenerateGradientNoise"
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
int _V_TA_Noise_Octaves;
float2 _V_TA_Noise_Scale;
float _V_TA_Noise_Power;
float _V_TA_Noise_Offset;
float2 _V_TA_Noise_Scroll;

float _AspectRatio;

//0 or 1
float _V_TA_Noise_IsFixed;

//Colors
float4 _V_TA_Noise_Color1;
float4 _V_TA_Noise_Color2;

//Gradient Texture
sampler2D _V_TA_Noise_GradientTexture;

//
int _ColorMode;




	//https://github.com/keijiro/NoiseShader
	float4 mod289(float4 x)
	{
		return x - floor(x / 289.0) * 289.0;
	}

	float4 permute(float4 x)
	{
		return mod289(((x*34.0)+1.0)*x);
	}

	float4 taylorInvSqrt(float4 r)
	{
		return (float4)1.79284291400159 - r * 0.85373472095314;
	}

	float2 fade(float2 t) 
	{
		return t*t*t*(t*(t*6.0-15.0)+10.0);
	}

	float Noise(float2 P)
	{
  		float4 Pi = floor(P.xyxy) + float4(0.0, 0.0, 1.0, 1.0);
		float4 Pf = frac (P.xyxy) - float4(0.0, 0.0, 1.0, 1.0);
		Pi = mod289(Pi); // To avoid truncation effects in permutation
		float4 ix = Pi.xzxz;
		float4 iy = Pi.yyww;
		float4 fx = Pf.xzxz;
		float4 fy = Pf.yyww;
		float4 i = permute(permute(ix) + iy);

		float4 gx = frac(i / 41.0) * 2.0 - 1.0 ;
		float4 gy = abs(gx) - 0.5 ;
		float4 tx = floor(gx + 0.5);
		gx = gx - tx;

		float2 g00 = float2(gx.x,gy.x);
		float2 g10 = float2(gx.y,gy.y);
		float2 g01 = float2(gx.z,gy.z);
		float2 g11 = float2(gx.w,gy.w);

		float4 norm = taylorInvSqrt(float4(dot(g00, g00), dot(g01, g01), dot(g10, g10), dot(g11, g11)));
		g00 *= norm.x;
		g01 *= norm.y;
		g10 *= norm.z;
		g11 *= norm.w;

		float n00 = dot(g00, float2(fx.x, fy.x));
		float n10 = dot(g10, float2(fx.y, fy.y));
		float n01 = dot(g01, float2(fx.z, fy.z));
		float n11 = dot(g11, float2(fx.w, fy.w));

		float2 fade_xy = fade(Pf.xy);
		float2 n_x = lerp(float2(n00, n01), float2(n10, n11), fade_xy.x);
		float n_xy = lerp(n_x.x, n_x.y, fade_xy.y);
		
		return 2.3 * n_xy;
	 }
	
	float4 frag_adj (v2f_img i) : SV_Target     
	{           		
		float o = 0.5;
		float s = 1.0;
		float w = 0.5;

		//AspectRatio
		float aspectRatio = _MainTex_TexelSize.w / _MainTex_TexelSize.z;
		i.uv *= _MainTex_TexelSize.z > _MainTex_TexelSize.w ? float2(1.0, aspectRatio) : float2(1.0 / aspectRatio, 1.0);

		for (int k = 0; k < _V_TA_Noise_Octaves; k++)
		{
		    o += Noise(i.uv * _V_TA_Noise_Scale * s + _V_TA_Noise_Scroll) * w;                
           
			s *= 2.0;
			w *= 0.5;
		}



		o = saturate(o);
		o = saturate(o + _V_TA_Noise_Offset);
		o = pow(o, _V_TA_Noise_Power);			
				

		if(_V_TA_Noise_IsFixed > 0.5)	
		{
			o = ((o - 0.5) * 2) < (_V_TA_Noise_Offset * 0.999) ? 0 : 1;			
		}
				        
		float4 colors[3] = {o.xxxx, lerp(_V_TA_Noise_Color2, _V_TA_Noise_Color1, o), tex2D(_V_TA_Noise_GradientTexture, float2(o, 0.5))};
		
		
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
