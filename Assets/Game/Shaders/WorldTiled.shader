Shader "Sprites/WorldTiled"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_BlendTex1("Blend Red", 2D) = "black" {}
		_BlendTex2("Blend Green", 2D) = "black" {}
		_BlendTex3("Blend Blue", 2D) = "black" {}
		_BlendTex4("Blend Alpha", 2D) = "black" {}
		_BlendMask("BlendMask", 2D) = "black" {}
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
		sampler2D _BlendTex1;
		sampler2D _BlendTex2;
		sampler2D _BlendTex3;
		sampler2D _BlendTex4;
		sampler2D _BlendMask;
		fixed4 _Color;
		sampler2D _AlphaTex;
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
		o.color = v.color * _Color;
		o.worldPos = v.vertex.xy;
		o.uv_MainTex = v.texcoord;
	}

	fixed4 SampleSpriteTexture(float2 uv)
	{
		fixed4 color = tex2D(_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
		color.a = tex2D(_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA

		return color;
	}

	fixed4 Blend(fixed4 c, fixed mask, sampler2D blendTex, float2 world_UV)
	{
		return c * (1-mask) + tex2D(blendTex, world_UV) * mask;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float2 uv = IN.worldPos * 16 * _MainTex_TexelSize.xy;
		
		fixed4 c = SampleSpriteTexture(uv) * IN.color;
		fixed4 blendMask = tex2D(_BlendMask, IN.uv_MainTex);

		c = Blend(c, blendMask.r, _BlendTex1, uv);
		c = Blend(c, blendMask.g, _BlendTex2, uv);
		c = Blend(c, blendMask.b, _BlendTex3, uv);
		c = Blend(c, blendMask.a, _BlendTex4, uv);

		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, uv));
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}
