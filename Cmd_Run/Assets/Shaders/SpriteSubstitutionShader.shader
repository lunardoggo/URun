Shader "URun/Sprite Substitution Shader"
{
    Properties
    {
        _MainTex("Original Texture", 2D) = "white" {}
        _SubTex("Substitution Texture", 2D) = "white" {}
        _Substitute("Substitute", float) = 0
    }
    SubShader
    {
        Tags 
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
        }
        
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            CGPROGRAM
            
            //pragmas
            #pragma vertex vert
            #pragma fragment frag
            
            //includes
            #include "UnityCG.cginc"
            
            //Variablen deklarationen
            sampler2D _MainTex;
            sampler2D _SubTex;
            float _Substitute;
            
            
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
            fragmentInput vert (vertexInput i)
            {
                fragmentInput o;
                
                o.vertex = UnityObjectToClipPos(i.vertex);
                o.uv = i.uv;
                
                return o;
            }
            
            fixed4 frag (fragmentInput i) : SV_Target
            {
                if(_Substitute > 0.0)
                    return tex2D(_SubTex, i.uv);
                else
                    return tex2D(_MainTex, i.uv);
            }
            
            ENDCG
        }
    }
}
