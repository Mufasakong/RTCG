Shader "Custom/CutawayObject"
{
    Properties
    {
        _BaseMap ("Texture", 2D) = "white" {}
        _BaseColor ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "Queue" = "Geometry" }

        // Only draw where stencil != 1 (outside the cutter)
        Stencil
        {
            Ref 1
            Comp NotEqual
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _BaseMap;
            float4 _BaseMap_ST;
            float4 _BaseColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = TRANSFORM_TEX(v.uv, _BaseMap);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_BaseMap, i.uv);
                return tex * _BaseColor;
            }
            ENDCG
        }
    }
}
