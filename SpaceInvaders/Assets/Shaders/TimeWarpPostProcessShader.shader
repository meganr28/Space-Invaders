Shader "Hidden/TimeWarpPostProcessShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;

            float4 frag (v2f_img i) : COLOR
            {
                float4 base = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return base * float4(0.5, 0.0, 1.0, 1.0);
            }
            ENDCG
        }
    }
}
