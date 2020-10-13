Shader "Custom/AbientOcc"
{
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Main2Tex("AO (R)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            sampler2D _Main2Tex;

            struct Input {
                float2 uv_MainTex : TEXCOORD0;
                float2 uv2_Main2Tex : TEXCOORD1;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;

            void surf(Input IN, inout SurfaceOutputStandard o) {

                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;;

                fixed4 Texture2 = tex2D(_Main2Tex, IN.uv2_Main2Tex);
                o.Alpha = Texture2.a;
                o.Occlusion = Texture2.r;
            }
            ENDCG
        }
            FallBack "Diffuse"
}

