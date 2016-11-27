Shader "Sprites/WorldTiledBumped"
{
	Properties
	{
		[Header(Main Textures)]
	[NoScaleOffset][PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	[NoScaleOffset][Normal] _BumpMap("Normalmap (RGB)", 2D) = "bump" {}
	[PerRendererData] _Color("Tint", Color) = (1,1,1,1)

	[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Geometry"
		"RenderType" = "TransparentCutout"
		"PreviewType" = "Plane"
	}
		LOD 500
		Cull Off

		CGPROGRAM
#pragma surface surf Lambert vertex:vert addshadow fullforwardshadows
#pragma multi_compile _ PIXELSNAP_ON
#pragma target 3.0

	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;
	sampler2D _AlphaTex;
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
		o.uv_MainTex = v.texcoord.xy;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float2 uv = IN.worldPos * 16 * _MainTex_TexelSize.xy;
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
		c *= _Color;
		o.Normal = UnpackNormal(tex2D(_BumpMap,uv));
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}
		Fallback "Transparent/VertexLit"
}