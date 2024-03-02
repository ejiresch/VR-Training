// Stencil Tutorial: https://www.youtube.com/watch?v=EzM8LGzMjmc
// see also: https://theslidefactory.com/see-through-objects-with-stencil-buffers-using-unity-urp/
/* fix invis URP shaders: 
  - https://stackoverflow.com/questions/75852398/unity-urp-shaders-invisible
  - set maskMat Render Queue to at least 2501
  - set outlineMat to something higher than maskMat (e.g. 2502)
  - ELSE THIS SHADER WILL NOT RENDER
*/
Shader "Unlit/StencilMask"
{
    Properties
    {
        // Unity Editor Param: which stencil value to write
        [IntRange] _StencilID ("StencilID", Range(0,255)) = 0
    }
    SubShader
    {
        Tags { 
            "RenderType"="Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry" // "Geometry-1" to render this before all others?
        }

        Pass
        {
            Blend Zero One // ignore color of this object, just render previous pixel color
            Zwrite Off  // do not write to the Z-Buffer

            Stencil
            {
                Ref [_StencilID] // reference value = write this stencil value
                Comp Always // always pass the stencil test, no matter the current stencil value on the PixelShader
                Pass Replace
                Fail Keep
            }
        }
    }
}
