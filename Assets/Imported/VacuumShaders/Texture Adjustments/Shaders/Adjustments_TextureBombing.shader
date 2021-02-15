Shader "Hidden/VacuumShaders/Texture Adjustments/Texture Bombing"
{
    Properties
    {
        _MainTex("", 2D) = "black" {}
    }

    CGINCLUDE
	#include "cginc/Adjustments_Math.cginc"


	sampler2D _MainTex;	
	float4 _MainTex_TexelSize;
//
sampler2D _V_TA_TextureBombing_BombTexture;
float _V_TA_TextureBombing_IS_FlipBook;
float2 _V_TA_TextureBombing_FlipBook_ColumnsRows;

//
float _V_TA_TextureBombing_Seed;
float _V_TA_TextureBombing_Bomb_Count;
float2 _V_TA_TextureBombing_Scale;

//
float _V_TA_TextureBombing_RandomRotate;
float _V_TA_TextureBombing_RandomHue;
float _V_TA_TextureBombing_RandomColor;
float _V_TA_TextureBombing_RandomAlpha;

float _V_TA_TextureBombing_SaveAlpha;




	//Array size is 1000
	//Plus additional 10 for random offset
	float4 _RandomFloatArray[1010];	
	
	float GetRandomValue(float k)
	{
		int index = floor(k / 4);
		float rgb = floor(k - index * 4);

		return _RandomFloatArray[index][rgb];
	} 

	float2 FlipBook(float2 uv, float tile, float2 widthHeight)
	{
		float res = widthHeight.x * widthHeight.y;
		tile = tile - res * floor(tile/res);	

	    float2 tileCount = float2(1.0, 1.0) / widthHeight;
	    float tileY = abs(widthHeight.y - (floor(tile * tileCount.x) + 1));
		float tileX = abs(((tile - widthHeight.x * floor(tile * tileCount.x))));
		return (uv + float2(tileX, tileY)) * tileCount;
	}

	half4 frag_adj (v2f_img i) : SV_Target     
	{      
		float4 c = tex2D(_MainTex, i.uv);
		
		
		float aspectRatio = _MainTex_TexelSize.w / _MainTex_TexelSize.z;
		i.uv *= _MainTex_TexelSize.z > _MainTex_TexelSize.w ? float2(1.0, aspectRatio) : float2(1.0 / aspectRatio, 1.0);
		

		int k = 0;
		for(k = 0; k < _V_TA_TextureBombing_Bomb_Count; k++)
		{

			//ScaleOffset
			float scale = lerp(_V_TA_TextureBombing_Scale.x, _V_TA_TextureBombing_Scale.y, GetRandomValue(k));
			float offsetX = lerp(-1, scale, GetRandomValue(k + 1));
			float offsetY = lerp(-1, scale, GetRandomValue(k + 2));
			float2 uv = i.uv * scale - float2(offsetX, offsetY);
 
			//Rotate
			float rotateAngle = lerp(0, 360, GetRandomValue(k + 3));
			float2 rotUV = Rotate2x2(uv, rotateAngle, float2(0.5, 0.5));					
			uv = lerp(uv, rotUV, _V_TA_TextureBombing_RandomRotate);


			//Saturate
			uv = saturate(uv);

					
			//FlipBook
			float randomFlipBookIndex = floor(lerp(0, _V_TA_TextureBombing_FlipBook_ColumnsRows.x * _V_TA_TextureBombing_FlipBook_ColumnsRows.y, GetRandomValue(k + 4)));
			float2 tileUV = FlipBook(uv, randomFlipBookIndex, _V_TA_TextureBombing_FlipBook_ColumnsRows);			
			uv = lerp(uv, tileUV, _V_TA_TextureBombing_IS_FlipBook);

 

			//Read Texture
			float4 bomb = tex2D(_V_TA_TextureBombing_BombTexture, uv);


			//Hue
			float3 hue = RGBtoHSV(bomb);
			hue.x = lerp(hue.x - _V_TA_TextureBombing_RandomHue, hue.x + _V_TA_TextureBombing_RandomHue, GetRandomValue(k + 5));			
			bomb.rgb = HSVToRGB(hue); 


			//Color and Alpha
			float4 color = float4(GetRandomValue(k + 6), GetRandomValue(k + 7), GetRandomValue(k + 8), GetRandomValue(k + 9));
			color.rgb = lerp(float3(1, 1, 1), color.rgb, _V_TA_TextureBombing_RandomColor);
			color.a = lerp(1, color.a, _V_TA_TextureBombing_RandomAlpha);
			bomb *= color; 


			c.rgb = lerp(c.rgb, bomb.rgb, bomb.a);

			c.a += bomb.a * _V_TA_TextureBombing_SaveAlpha;
		}
 
		c.a = saturate(c.a);


		ADJUST_COLOR_BY_COLOR_SPACE(c)
		return c;
	}     


    ENDCG

    Subshader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
			Fog { Mode off }

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag_adj
			#pragma target 3.0

            ENDCG
        }
    }
}
