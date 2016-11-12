Shader "Sprites/WorldTiled"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_BumpMap("Normalmap", 2D) = "bump" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
		LOD 500
		Cull Off
		Lighting On
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog keepalpha
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

#pragma target 3.0
		sampler2D _MainTex;
		fixed4 _Color;
		sampler2D _AlphaTex;
		sampler2D _BumpMap;
		float4 _MainTex_TexelSize;
	struct Input
	{
		float2 worldPos;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color;
		o.worldPos = v.vertex.xy;
	}

	fixed4 SampleSpriteTexture(float2 uv)
	{
		fixed4 color = tex2D(_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
		color.a = tex2D(_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA

		return color;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float2 uv = IN.worldPos * 16 * _MainTex_TexelSize.xy;

		fixed4 c = SampleSpriteTexture(uv) * IN.color;
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, uv));
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}
