Shader "Custom/SilhouetteShader"
{
    Properties
    {
        _Tint ("Tint", Color) = (1, 1, 1, 1)
        _Thickness ("Thickness", Range(0, 10)) = 0.5
        _Noise("Noise", 2D) = "white"{}
        _Noise2("Noise2", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityStandardBRDF.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD2;
            };

            float4 _Tint;
            float _Thickness;
            sampler2D _Noise, _Noise2;
            float4 _Noise_ST, _Noise2_ST;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _Noise);
                return o;
            }

            float4 frag (VertexOutput i) : SV_Target
            {
                i.normal = normalize(i.normal);
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
                float d = DotClamped(viewDir, i.normal);
                float f = 1 - d ;
                f *= _Thickness;
                //f = tex2D(_Noise, i.uv);
                f = min(1, _Tint.a * tex2D(_Noise, i.uv + float2(0.5, 0.5) * tex2D(_Noise2, i.uv) * _Time.y) / pow(d, _Thickness));

                return float4(_Tint.rgb, f);
            }
            ENDCG
        }
    }
}
