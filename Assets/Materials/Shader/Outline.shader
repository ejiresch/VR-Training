// draws an outline, assumes that a stencil mask is rendered before
// see StencilMask.shader
/* fix invis URP shaders: 
  - https://stackoverflow.com/questions/75852398/unity-urp-shaders-invisible
  - set maskMat Render Queue to at least 2501
  - set outlineMat to something higher than maskMat (e.g. 2502)
  - ELSE THIS SHADER WILL NOT RENDER
*/
Shader "Unlit/Outline"
{
    Properties
    {
        // properties that can be set in the Outline Shader
        _Color("Outline color", Color) = (.25, .5, .5, 1)
        _Scale("Outline Scale", Range(1.0, 2.0)) = 1.5
        [IntRange] _StencilID("StencilID", Range(0,255)) = 0
    }
        SubShader
    {
        Tags {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"}

        Pass
        {
            // check for stencil here
            Stencil
            {
                Ref[_StencilID] // reference value = write this stencil value
                Comp NotEqual // if the stencil value is equal, then fail, i.e., do not render
                // never change the stencil value
                Pass Keep
                Fail Keep
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;

                UNITY_VERTEX_OUTPUT_STEREO
            };

            // declare the properties here again so they work in the shader
            float4 _Color;
            float _Scale; 

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v); //Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

                float4 scaledVert = v.vertex;
                scaledVert.xyz *= _Scale; // make the object bigger, so the outline is visible
                o.vertex = UnityObjectToClipPos(scaledVert);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // just set the color, no other shading
                fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
