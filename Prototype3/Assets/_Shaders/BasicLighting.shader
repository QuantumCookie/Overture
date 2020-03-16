Shader "Unlit/Basic Lighting"
{
	Properties
	{
		_Color("_Color", Color) = (1, 1, 1, 1)
		_Gloss("_Gloss", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

        Pass
        {
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
			float _Gloss;

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
				float3 normal = normalize(o.normal);
				//float3 normal = o.normal;

				float3 lightDir = _WorldSpaceLightPos0.xyz;
				float3 lightColor = _LightColor0.rgb;
				float3 ambientLightColor = float3(0.1, 0.1, 0.1);

				//Direct Light
				float lightFallOff = (max(0, dot(lightDir, normal)));
				float3 directDiffuseLighting = lightColor * lightFallOff;

				//Ambient Light
				float3 diffuseLight = directDiffuseLighting + ambientLightColor;


				//Direct Specular Lighting
				float3 camPos = _WorldSpaceCameraPos;
				float3 fragToCam = camPos - o.worldPos;
				float3 viewDir = normalize(fragToCam);

				float3 viewReflect = reflect(-viewDir, normal);

				float specularFallOff = max(0, dot(viewReflect, lightDir));
				specularFallOff = pow(specularFallOff, _Gloss);
				float3 directSpecular = specularFallOff * lightColor;

				//Composite with surface color
				float3 finalSurfaceColor = diffuseLight * _Color.rgb + directSpecular;

				return float4(finalSurfaceColor, 0);
            }
            ENDCG
        }
    }
}
