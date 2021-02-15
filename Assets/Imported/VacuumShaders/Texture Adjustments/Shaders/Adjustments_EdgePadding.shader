Shader "Hidden/VacuumShaders/Texture Adjustments/Edge Padding"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}		
	}

	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


    sampler2D _MainTex;
    float2 _Resolution;

	sampler2D _AlphaSource;
	

	float4 frag4(v2f_img i) : SV_Target
	{
		float4 B = tex2D(_MainTex, i.uv + float2( 0, -1) * _Resolution);
		float4 D = tex2D(_MainTex, i.uv + float2(-1,  0) * _Resolution);
		float4 E = tex2D(_MainTex, i.uv + float2( 0,  0) * _Resolution);
		float4 F = tex2D(_MainTex, i.uv + float2( 1,  0) * _Resolution);
		float4 H = tex2D(_MainTex, i.uv + float2( 0,  1) * _Resolution);

		float4 BH = H;
		if (B.a > H.a)
			BH = B;

		float4 FD = D;
		if (F.a > D.a)
			FD = F;

		float4 BHFD = FD;
		if (BH.a > FD.a)
			BHFD = BH;

		float4 final = E;
		if (BHFD.a > E.a)
			final = BHFD;

		

		ADJUST_COLOR_BY_COLOR_SPACE(final)
		return final;
	}

	float4 frag8(v2f_img i) : SV_Target
	{
		float4 B = tex2D(_MainTex, i.uv + float2( 0, -1) * _Resolution);
		float4 D = tex2D(_MainTex, i.uv + float2(-1,  0) * _Resolution);
		float4 E = tex2D(_MainTex, i.uv + float2( 0,  0) * _Resolution);
		float4 F = tex2D(_MainTex, i.uv + float2( 1,  0) * _Resolution);
		float4 H = tex2D(_MainTex, i.uv + float2( 0,  1) * _Resolution);

		float4 I = tex2D(_MainTex, i.uv + float2(-1,  1) * _Resolution);
		float4 J = tex2D(_MainTex, i.uv + float2( 1,  1) * _Resolution);
		float4 K = tex2D(_MainTex, i.uv + float2(-1, -1) * _Resolution);
		float4 L = tex2D(_MainTex, i.uv + float2( 1, -1) * _Resolution);

		float4 BH = H;
		if (B.a > H.a)
			BH = B;

		float4 FD = D;
		if (F.a > D.a)
			FD = F;

		float4 BHFD = FD;
		if (BH.a > FD.a)
			BHFD = BH;


		float4 IL = L;
		if (I.a > L.a)
			IL = I;

		float4 JK = K;
		if (J.a > K.a)
			JK = J;

		float4 ILJK = JK;
		if (IL.a > JK.a)
			ILJK = IL;


		float4 T = BHFD;
		if (ILJK.a > BHFD.a)
			T = ILJK;


		float4 final = E;
		if (T.a > E.a)
			final = T;

		

		ADJUST_COLOR_BY_COLOR_SPACE(final)
		return final;
	}

	float4 frag_CopyAlpha(v2f_img i) : SV_Target
	{
		float4 c = float4(tex2D(_MainTex, i.uv).rgb, tex2D(_AlphaSource, i.uv).a);
		

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
            #pragma fragment frag4

            ENDCG
        }

        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag8

            ENDCG
        }
			 
		Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_CopyAlpha

            ENDCG
        }
    }
}
