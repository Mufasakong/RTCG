Shader "Custom/CutterMask"
{
    SubShader
    {
        Tags { "Queue" = "Geometry-10" }

        // Only write stencil, no color
        ColorMask 0
        ZWrite Off

        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace   // write "1" into stencil
        }

        Pass {}
    }
}
