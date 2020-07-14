Shader "Unlit/LightningShader"
{
	Properties
	{
		_Thickness("Thickness", Range(0.01, 0.5)) = 0.05
		_NoisePrimary("Primary Noise", 2D) = "white" {}
		_StrengthPrimary("Primary Strength", Range(0, 1)) = 0.2
		_NoiseSecondary("Secondary Noise", 2D) = "white" {}
		_StrengthSecondary("Secondary Strength", Range(0, 1)) = 0.2
		_Color("Color", Color) = (1, 1, 1, 1)
		_ColorStrength("Color Strength", Float) = 0.1
	}
		SubShader
		{
			Tags 
			{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}

			Blend One OneMinusSrcAlpha
			ZWrite Off
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				float _Thickness;
				sampler2D _NoisePrimary;
				float _StrengthPrimary;
				sampler2D _NoiseSecondary;
				float _StrengthSecondary;
				float4 _Color;
				float _ColorStrength;

				float inverseLerp(float a, float b, float t)
				{
					return (t - a) / (b - a);
				}

				float4 calcLightning(float mid, sampler2D noiseTex, float noiseStr, float2 uv, float thickness)
				{
					float noise = tex2D(noiseTex, uv + float2(0, _Time.y));
					noise = noise * 2 - 1;

					mid += 0.5 * noise * noiseStr;

					float lbound = mid - thickness * 0.5;
					float ubound = mid + thickness * 0.5;

					float4 lightning;

					if (uv.x > lbound && uv.x < ubound) lightning = float4(1, 1, 1, 1);
					else
					{
						float4 glow = float4(1, 1, 1, 1) * 0.5;

						if (uv.x < lbound) glow = glow * inverseLerp(lbound - thickness, lbound, uv.x);
						else if(uv.x > ubound) glow = glow * inverseLerp(ubound + thickness, ubound, uv.x);

						lightning = glow * _Color * _ColorStrength;
					}

					return saturate(lightning);
				}

				float4 frag(v2f i) : SV_Target
				{
					float4 mainLightning = calcLightning(0.5, _NoisePrimary, _StrengthPrimary * sin(i.uv.y * 3.1415), i.uv, _Thickness * sin(i.uv.y * 3.1415));
					float4 subLightning = calcLightning(0.5, _NoiseSecondary, _StrengthSecondary * sin(i.uv.y * 3.1415), i.uv + float2(0, _Time.x), _Thickness * sin(i.uv.y * 3.1415));

					return saturate(mainLightning + subLightning);
				}
				ENDCG
			}
		}
}