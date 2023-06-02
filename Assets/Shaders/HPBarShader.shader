Shader "Custom/HPBarShader"
{
    Properties
    {
        _HPValue("HP Value", Range(0, 1)) = 1
        _MaxHPValue("Max HP Value", Range(0, 1)) = 1
        _BackgroundColor("Background Color", Color) = (1, 1, 1, 1)
        _FillColor("Fill Color", Color) = (1, 0, 0, 1)
    }

        SubShader
    {
        Tags{ "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _HPValue;
            float _MaxHPValue;
            fixed4 _BackgroundColor;
            fixed4 _FillColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float currentHP = _HPValue * _MaxHPValue; // Calculate the current HP value
                float fillAmount = saturate(currentHP / _MaxHPValue); // Calculate the fill amount based on the current HP value
                fixed4 color = lerp(_BackgroundColor, _FillColor, step(i.uv.x, fillAmount)); // Lerp between the background and fill color based on the fillAmount
                return color;
            }
            ENDCG
        }
    }
}
