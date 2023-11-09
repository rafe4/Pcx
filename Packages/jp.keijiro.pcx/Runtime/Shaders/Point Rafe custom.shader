// Pcx - Point cloud importer & renderer for Unity
// https://github.com/keijiro/Pcx

Shader "Point Cloud/Point With Scan Pulse"
{
    Properties
    {
        _Tint("Tint", Color) = (0.5, 0.5, 0.5, 1)
        _PointSize("Point Size", Float) = 0.05
        _ScanColor("Scan Color", Color) = (1, 0, 0, 1)
        _ScanWidth("Scan Width", Float) = 0.1
        _ScanPosition("Scan Position", Range(-10, 10)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
                float3 worldPos : TEXCOORD0;
                float psize : PSIZE; // Include point size
            };

            half4 _Tint;
            float _PointSize;
            float4 _ScanColor;
            float _ScanWidth;
            float _ScanPosition; // Current position of the scan line

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _Tint;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.psize = _PointSize; // Set the point size for each vertex
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the distance from the scan position for the scan effect
                float distFromScanLine = abs(i.worldPos.y - _ScanPosition);
                
                // Normalize the distance based on the scan width
                float scanEffect = saturate(1.0 - distFromScanLine / _ScanWidth);
                
                // Apply scan line color with original color based on scanEffect value
                fixed4 col = lerp(i.color, _ScanColor, scanEffect);

                return col;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
