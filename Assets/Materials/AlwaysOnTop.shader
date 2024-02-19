Shader "Custom/AlwaysOnTop"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _RoughnessTex ("RoughnessTex ", 2D) = "white" {}
    }
    SubShader {
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest Always
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #include "UnityCG.cginc"
            
            struct appdata_t {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float3 normal : NORMAL;

                UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
            };
            
            struct v2f {
                float4 pos : SV_POSITION;
                float4 texcoord0 : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;

                UNITY_VERTEX_OUTPUT_STEREO //Insert
            };
            
            sampler2D _MainTex;
            sampler2D _RoughnessTex;
            
            v2f vert(appdata_t v) {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v); //Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

                o.pos = UnityObjectToClipPos(v.vertex);
                o.texcoord0 = v.texcoord0;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = ObjSpaceViewDir(v.vertex);
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target {
                fixed4 color = tex2D(_MainTex, i.texcoord0.xy);
                float d = dot(_WorldSpaceLightPos0.xyz, i.normal);
                return color;
            }
            ENDCG
        }
    }
}
