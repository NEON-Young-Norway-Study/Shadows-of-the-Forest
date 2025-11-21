Shader "Custom/ColorAdjustable"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Strength ("Strength", Range(0,1)) = 1 // 1 = full grayscale, 0 = full color
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Strength; // control parameter

            fixed4 frag(v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                fixed4 grayscaleColor = float4(gray, gray, gray, col.a);
                // Blend between original color and grayscale
                return lerp(col, grayscaleColor, _Strength);
            }
            ENDCG
        }
    }
}
