Shader "Hidden/VacuumShaders/Texture Adjustments/LUT"
{
    Properties
    {
        _MainTex("", 2D) = "white" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
sampler3D _V_TA_LUT_Tex;
float _V_TA_LU_Contribution;


	half4 frag_adj (v2f_img i) : SV_Target     
	{     
		#ifdef UNITY_COLORSPACE_GAMMA
			float4 c = tex2D(_MainTex, i.uv);
			c.rgb = lerp(c.rgb, tex3D(_V_TA_LUT_Tex, c.rgb).rgb, _V_TA_LU_Contribution);

			return c;
		#else
			float4 c = tex2D(_MainTex, i.uv);
			c.rgb = sqrt(c.rgb);
			c.rgb = lerp(c.rgb, tex3D(_V_TA_LUT_Tex, c.rgb).rgb, _V_TA_LU_Contribution);
			c.rgb = c.rgb*c.rgb;

			return c;
		#endif
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
			#pragma target 3.0

            ENDCG
        }
    }
}
