Shader "Hidden/VacuumShaders/Texture Adjustments/Color Space" 
{
	Properties 
	{
		_MainTex ("Screen Blended", 2D) = "" {}
	}
	
	CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


sampler2D _MainTex;	
		 
//0 - gamma
//1 - linear
int _ColorSpaceIndex;

	float4 frag_adj (v2f_img i) : SV_Target     
	{       
		float4 c = tex2D(_MainTex, i.uv);

		c.rgb = lerp(GammaToLinearSpace(c.rgb), LinearToGammaSpace(c.rgb), _ColorSpaceIndex);

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