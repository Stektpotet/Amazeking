Shader "Sprites/WorldTiledMultiTex"
{
	Properties
	{
	[Header(Main Textures)]
	[NoScaleOffset] _MainTex("Sprite Texture", 2D) = "white" {}
	[NoScaleOffset][Normal] _BumpMap("Normalmap (RGB)", 2D) = "bump" {}
	[PerRendererData] _Color("Tint", Color) = (1,1,1,1)
	[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Geometry"
		"RenderType" = "Opaque"
		"PreviewType" = "Plane"
	}
		LOD 500
		Cull Off
		Lighting On
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog keepalpha
#pragma multi_compile _ PIXELSNAP_ON
#pragma target 3.0

	sampler2D _MainTex;
	fixed4 _Color;
	sampler2D _BumpMap;
	float4 _MainTex_TexelSize;

	struct Input
	{
		float2 worldPos;
		float2 uv_MainTex;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color;
		o.worldPos = v.vertex.xy;
		o.uv_MainTex = v.texcoord;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float2 uv = float2((IN.worldPos*16*_MainTex_TexelSize).x,IN.uv_MainTex.y);

		fixed4 c = tex2D(_MainTex, uv) * IN.color;
		c *= _Color;
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;

		fixed4 bump = tex2D(_BumpMap, uv);
		o.Normal = UnpackNormal(bump);

	}
	ENDCG
	}
		Fallback "Transparent/VertexLit"
}
