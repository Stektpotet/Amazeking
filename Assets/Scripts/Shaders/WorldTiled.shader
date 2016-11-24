Shader "Sprites/WorldTiled"
{
	Properties
	{
		[Header(Main Textures)]
	[NoScaleOffset][PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	[NoScaleOffset][Normal] _BumpMap("Normalmap (RGB)", 2D) = "bump" {}
	[PerRendererData] _Color("Tint", Color) = (1,1,1,1)
		[Space(20)]
	[Header(Blending)]
	[NoScaleOffset] _BlendTex1("Blend Red", 2D) = "black" {}
	[NoScaleOffset] _BlendTex2("Blend Green", 2D) = "black" {}
	[NoScaleOffset] _BlendTex3("Blend Blue", 2D) = "black" {}
	[NoScaleOffset] _BlendTex4("Blend Alpha", 2D) = "black" {}
	[PerRendererData] _BlendMask("BlendMask", 2D) = "black" {}
	[NoScaleOffset][Normal] _BumpMap1("Normalmap Red", 2D) = "bump" {}
	[NoScaleOffset][Normal] _BumpMap2("Normalmap Green", 2D) = "bump"{}
	[NoScaleOffset][Normal] _BumpMap3("Normalmap Blue", 2D) = "bump" {}
	[NoScaleOffset][Normal] _BumpMap4("Normalmap Alpha", 2D) = "bump" {}
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
	sampler2D _BlendTex1;
	sampler2D _BlendTex2;
	sampler2D _BlendTex3;
	sampler2D _BlendTex4;
	sampler2D _BlendMask;
	fixed4 _Color;
	sampler2D _AlphaTex;
	sampler2D _BumpMap;
	sampler2D _BumpMap1;
	sampler2D _BumpMap2;
	sampler2D _BumpMap3;
	sampler2D _BumpMap4;
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

	fixed4 Blend(fixed4 c, fixed mask, fixed4 blendTex)
	{
		return c * (1 - mask*blendTex.a) + blendTex * mask * blendTex.a;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float2 uv = IN.worldPos * 16 * _MainTex_TexelSize.xy;

		fixed4 c = tex2D(_MainTex, uv) * IN.color;
		fixed4 blendMask = tex2D(_BlendMask, IN.uv_MainTex);
		fixed4 blendTex1 = tex2D(_BlendTex1, uv);
		fixed4 blendTex2 = tex2D(_BlendTex2, uv);
		fixed4 blendTex3 = tex2D(_BlendTex3, uv);
		fixed4 blendTex4 = tex2D(_BlendTex4, uv);

		fixed4 bump = tex2D(_BumpMap, uv);
		fixed4 bumpTex1 = tex2D(_BumpMap1, uv);
		fixed4 bumpTex2 = tex2D(_BumpMap2, uv);
		fixed4 bumpTex3 = tex2D(_BumpMap3, uv);
		fixed4 bumpTex4 = tex2D(_BumpMap4, uv);


		c = Blend(c, blendMask.r, blendTex1);
		c = Blend(c, blendMask.g, blendTex2);
		c = Blend(c, blendMask.b, blendTex3);
		c = Blend(c, blendMask.a, blendTex4);

		c *= _Color;

		bump = Blend(bump, blendMask.r*blendTex1.a, bumpTex1);
		bump = Blend(bump, blendMask.g*blendTex2.a, bumpTex2);
		bump = Blend(bump, blendMask.b*blendTex3.a, bumpTex3);
		bump = Blend(bump, blendMask.a*blendTex4.a, bumpTex4);

		o.Normal = UnpackNormal(bump);
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}