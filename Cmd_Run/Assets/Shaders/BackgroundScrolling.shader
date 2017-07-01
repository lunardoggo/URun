Shader "URun/Background Scrolling Shader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Speed("Scrolling Speed", Range(0.05, 5.0)) = 0.0
    }
    SubShader
    {
        Pass
        {
            Tags 
            {
                "Queue" = "Background"
                "RenderType" = "Opaque"
            }
            
            CGPROGRAM
            
            //pragmas
            #pragma vertex vert
            #pragma fragment frag
            
            //includes
            #include "UnityCG.cginc"
            
            //Variablen deklarationen
            sampler2D _MainTex;
            float _Speed;
            
            //structs
            struct vertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct fragmentInput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            //functions
            
            //frag and vert
            fragmentInput vert (vertexInput v)
            {
                fragmentInput o;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                return o;
            }
            
            fixed4 frag (fragmentInput i) : SV_Target
            {
                float2 displacement = float2(i.uv.x / 2 + _Time.x * _Speed, 0.0);
                
                return tex2D(_MainTex, i.uv + displacement);
            }
            
            ENDCG
        }
    }
}
