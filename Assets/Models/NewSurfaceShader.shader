Shader "Custom/cleaningShader"
{
    Properties
    {
        _DirtyTex ("Dirty Texture", 2D) = "black" {}
        _CleanTex ("Clean Texture", 2D) = "white" {}
        _Blend ("Blend", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _DirtyTex;
        sampler2D _CleanTex;
        float _Blend;

        struct Input
        {
            float2 uv_DirtyTex;
            float2 uv_CleanTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 dirtyCol = tex2D(_DirtyTex, IN.uv_DirtyTex);
            float4 cleanCol = tex2D(_CleanTex, IN.uv_CleanTex);
            o.Albedo = lerp(dirtyCol, cleanCol, _Blend).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
