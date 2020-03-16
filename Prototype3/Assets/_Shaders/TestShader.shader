Shader "Unlit/TestShader"
{
	Properties
	{
		_Color("_Color", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
		LOD 100

        Pass
        {
			ZWrite Off

			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
				float4 normal : NORMAL;
                float2 uv0 : TEXCOORD0;
            };

            struct VertexOutput
            {
                float4 clipSpacePos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float4 normal : TEXCOORD1;
				float4 worldPos : TEXCOORD2;
            };

			float4 _Color;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.clipSpacePos = UnityObjectToClipPos(v.vertex);
				o.uv0 = v.uv0;
				o.normal = v.normal;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag (VertexOutput o) : SV_Target
            {
				float PI = 3.14159265358979;
				float x = o.uv0.x;
				float y = o.uv0.y;

				//Wave effect on a quad
				/*float timeOffset = _Time.y * PI * 1;
				float extraOffset = (sin(5 * _SinTime.y * y * PI) + sin(1.5 * y * PI)) * 0.5;

				float col = sin(5 * x * PI - timeOffset + extraOffset) * (1 - x);
				
				float3 ambient = float3(0.61, 0.1, 0.21);

				return float4(saturate(_Color.rgb * (1 - col)) + ambient, 0);*/

				//Ripple effect
				/*
				x -= 0.5;
				y -= 0.5;

				float col = x * x + y * y;
				col = sin(10 * col * PI - _Time.y * PI * 1.5);
				col = (col + 1) * 0.5;

				return float4(col.xxx * _Color.rgb, 0);*/

				return float4(1, 1, 1, 0.4);
			}
            ENDCG
        }
    }
}
