#ifndef VACUUM_ADJUSTMENTS_MATH_CGINC
#define VACUUM_ADJUSTMENTS_MATH_CGINC 


#include "UnityCG.cginc" 
 


#ifdef UNITY_COLORSPACE_GAMMA
	#define ADJUST_COLOR_BY_COLOR_SPACE(value) 
#else
	#define ADJUST_COLOR_BY_COLOR_SPACE(value) 
#endif

 
#define PI   3.14159265359
#define PI_2 6.28318530718


float HueToRGB(float f1, float f2, float hue)
{
	if (hue < 0.0)
		hue += 1.0;
	else if (hue > 1.0)
		hue -= 1.0;
	float res;
	if ((6.0 * hue) < 1.0)
		res = f1 + (f2 - f1) * 6.0 * hue;
	else if ((2.0 * hue) < 1.0)
		res = f2;
	else if ((3.0 * hue) < 2.0)
		res = f1 + (f2 - f1) * ((2.0 / 3.0) - hue) * 6.0;
	else
		res = f1;
	return res;
} 

float RGBtoHUE(float3 c)
{
	float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
	float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

	return abs(q.z + (q.w - q.y) / (6.0 * (q.x - min(q.w, q.y)) + 1.0e-10));
}

float3 RGBtoHSV(float4 arg1)
{
	float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 P = lerp(float4(arg1.bg, K.wz), float4(arg1.gb, K.xy), step(arg1.b, arg1.g));
    float4 Q = lerp(float4(P.xyw, arg1.r), float4(arg1.r, P.yzx), step(P.x, arg1.r));
    float D = Q.x - min(Q.w, Q.y);
    float E = 1e-10;
    return float3(abs(Q.z + (Q.w - Q.y) / (6.0 * D + E)), D / (Q.x + E), Q.x);
} 

float3 HSVToRGB(float3 arg1)
{ 
	float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    float3 P = abs(frac(arg1.xxx + K.xyz) * 6.0 - K.www);
    return arg1.z * lerp(K.xxx, saturate(P - K.xxx), arg1.y);
}

float3 RGBToHSL(float4 color)
{
	float3 hsl; // init to 0 to avoid warnings ? (and reverse if + remove first part)
	
	float fmin = min(min(color.r, color.g), color.b);    //Min. value of RGB
	float fmax = max(max(color.r, color.g), color.b);    //Max. value of RGB
	float delta = fmax - fmin;             //Delta RGB value

	hsl.z = (fmax + fmin) / 2.0; // Luminance

	if (delta == 0.0)		//This is a gray, no chroma...
	{
		hsl.x = 0.0;	// Hue
		hsl.y = 0.0;	// Saturation
	}
	else                                    //Chromatic data...
	{
		if (hsl.z < 0.5)
			hsl.y = delta / (fmax + fmin); // Saturation
		else
			hsl.y = delta / (2.0 - fmax - fmin); // Saturation
		
		float deltaR = (((fmax - color.r) / 6.0) + (delta / 2.0)) / delta;
		float deltaG = (((fmax - color.g) / 6.0) + (delta / 2.0)) / delta;
		float deltaB = (((fmax - color.b) / 6.0) + (delta / 2.0)) / delta;

		if (color.r == fmax )
			hsl.x = deltaB - deltaG; // Hue
		else if (color.g == fmax)
			hsl.x = (1.0 / 3.0) + deltaR - deltaB; // Hue
		else if (color.b == fmax)
			hsl.x = (2.0 / 3.0) + deltaG - deltaR; // Hue

		if (hsl.x < 0.0)
			hsl.x += 1.0; // Hue
		else if (hsl.x > 1.0)
			hsl.x -= 1.0; // Hue
	}

	return hsl;
}

float3 HSLToRGB(float3 hsl)
{
	float3 rgb;
	
	if (hsl.y == 0.0)
		rgb = (hsl.z); // Luminance
	else
	{
		float f2;
		
		if (hsl.z < 0.5)
			f2 = hsl.z * (1.0 + hsl.y);
		else
			f2 = (hsl.z + hsl.y) - (hsl.y * hsl.z);
			
		float f1 = 2.0 * hsl.z - f2;
		
		rgb.r = HueToRGB(f1, f2, hsl.x + (1.0/3.0));
		rgb.g = HueToRGB(f1, f2, hsl.x);
		rgb.b = HueToRGB(f1, f2, hsl.x - (1.0/3.0));
	}
	
	return rgb;
} 

inline float4 LinearToGammaSpace(float4 linRGB)
{
	linRGB.rgb = max(linRGB.rgb, float3(0.h, 0.h, 0.h));
	// An almost-perfect approximation from http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1
	return float4(max(1.055h * pow(linRGB.rgb, 0.416666667h) - 0.055h, 0.h).rgb, linRGB.a);
}

float2 Remap(float2 value, float from1, float to1, float from2, float to2)
{
	return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
}

float Random(float2 co)
{
	return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
}

float2 Rotate2x2(float2 uv, float Angle, float2 pivotPoint)
{
	float s,c;
	sincos(Angle * 0.0174533, s, c);

	uv = mul(uv - pivotPoint, float2x2(c, -s, s, c)) + pivotPoint;

	return uv;
}


#endif //file