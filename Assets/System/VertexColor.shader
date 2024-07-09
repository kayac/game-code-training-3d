Shader "App/VertexColor"
{
	Properties
	{
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Cull Off
						
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct vOut
			{
				float4 color : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			struct pIn
			{
				float4 color : TEXCOORD0;
                UNITY_VPOS_TYPE vpos : VPOS;
			};

			vOut vert (appdata v)
			{
				vOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				return o;
			}

			fixed4 frag (pIn i) : SV_Target
			{
				// アルファ
				float t = (i.vpos.x * 3.0) + i.vpos.y;
				if (i.color.a <= frac(t / 16.0)) // 16階調
				{
					discard;
				}

				return fixed4(i.color.xyz, 1.0);
			}
			ENDCG
		}
	}
}
