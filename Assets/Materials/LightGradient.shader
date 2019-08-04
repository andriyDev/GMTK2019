// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Light"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_LightPosition("Light", Vector) = (0.5, 0.5, 0.5, 0.5)
		_LightRange("Range", Float) = 0.5
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "False"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 pos  : TEXCOORD0;
				};

				uniform sampler2D _MainTex;
				uniform fixed4 _Color;
				uniform float4 _LightPosition;
				uniform float _LightRange;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.pos = mul(unity_ObjectToWorld, IN.vertex).xy;
					OUT.color = IN.color * _Color;

					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					float4 c = IN.color;
					c.a *= max(1 - length(IN.pos.xy - _LightPosition) / _LightRange, 0);
					return c;
				}
			ENDCG
			}
		}
}