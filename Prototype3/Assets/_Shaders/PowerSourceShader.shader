Shader "Custom/PowerSourceShader"
{
    Properties
    {
        [PerRendererData][HDR]_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard nolightmap
        #pragma target 3.0

        struct Input
        {
            fixed4 color : COLOR;
        };
		
        fixed4 _Color;
		int _Intensity;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			o.Emission = _Color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
