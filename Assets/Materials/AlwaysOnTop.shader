Shader "Custom/AlwaysOnTop"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _RoughnessTex ("RoughnessTex ", 2D) = "white" {}
    }
    SubShader {
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha  // Standard alpha blending
            ZWrite On
            ZTest Always  // Always render regardless of depth buffer content
            ColorMask RGB
            CGPROGRAM
            #pragma vertex vertexShader
            #pragma fragment pixelShader
            #pragma target 3.0
            #include "UnityCG.cginc"
            
            // a texture we can set from Unity
            uniform sampler2D _MainTex;
            uniform sampler2D _RoughnessTex;

            // this data is the input for the vertex shader
            // (TRANSFORM)
            struct vertInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float3 normal : NORMAL;
            };

            // this data is returned by the vertex shader
            // as input for the pixel Shader
            // (LIGHTING)
            struct pixelInput {
                float4 pos : SV_POSITION;
                float4 texcoord0 : TEXCOORD0;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD1;
            };

            // vertex shader
            // TRANSFORM
            pixelInput vertexShader(vertInput vi) {
                pixelInput result;
                // transform the vertex, including perspective
                result.pos = UnityObjectToClipPos (vi.vertex);      
                // texture coordinate is not transformed
                result.texcoord0 = vi.texcoord0;   
                // transform normal, without perspective
                result.normal = UnityObjectToWorldNormal(vi.normal);
                // calculate view direction, useful for special stuff
                result.viewDir = normalize(WorldSpaceViewDir(vi.vertex)); 

                return result;
            }

            // pixel shader
            // LIGHTING
            float4 pixelShader(pixelInput pi) : SV_Target {
                //float4 color =  float4(0,1,0,1); // RGBA
                //float4 color =  float4(pi.texcoord0.x, pi.texcoord0.y, 0, 1); // show texture coords
                float4 color = tex2D(_MainTex, pi.texcoord0.xy); // get the color from a texture

                // TODO diffuse (Lambert) lighting
                // dot product between normal and light direction
                // use _WorldSpaceLightPos0.xyz and the dot() method
                float d = dot(_WorldSpaceLightPos0.xyz, pi.normal);
                

                return color;
            }

            ENDCG
        }
    }
}
