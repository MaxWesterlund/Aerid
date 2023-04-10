Shader "Unlit/Terrain"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlatColor ("Flat Color", Color) = (1, 1, 1, 1)
        _SteepColor ("Steep Color", Color) = (1, 1, 1, 1)
        [HDR]
        _ShadowColor ("Shadow Color", Color) = (0.4, 0.4, 0.4, 1)
    }
    SubShader
    {
        Tags 
        {
            "LightMode"="ForwardBase"
            "PassFlags"="OnlyDirectional"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                TRANSFER_SHADOW(o)

                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float4 _FlatColor;
            float4 _SteepColor;
            float4 _ShadowColor;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);

                float shadow = SHADOW_ATTENUATION(i);
                float lightIntensity = smoothstep(0, 0.5, NdotL * shadow);

                fixed4 col = tex2D(_MainTex, i.uv);

                float slope = dot(i.worldNormal, float3(0, 1, 0));
                slope = (slope + 1) / 2;

                float4 color = lerp(_SteepColor, _FlatColor, slope);

                return color * (_ShadowColor + lightIntensity);
            }
            ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
