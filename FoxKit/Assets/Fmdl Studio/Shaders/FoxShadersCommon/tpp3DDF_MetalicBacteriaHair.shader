Shader "FoxShaders/tpp3DDF_MetalicBacteriaHair" {
Properties {
	_MatParamIndex_0("MatParamIndex_0", Vector) = (0.0, 0.0, 0.0, 0.0)
	_Anistropic_MainLightDir("Anistropic_MainLightDir", Vector) = (0.0, 0.0, 0.0, 0.0)
	_Anistropic_Diffusion("Anistropic_Diffusion", Vector) = (0.0, 0.0, 0.0, 0.0)
	_HairShiftScale("HairShiftScale", Vector) = (0.0, 0.0, 0.0, 0.0)
	_EdgeRoughness("EdgeRoughness", Vector) = (0.0, 0.0, 0.0, 0.0)
	_EdgeTrans("EdgeTrans", Vector) = (0.0, 0.0, 0.0, 0.0)
	_Incidence_Roughness("Incidence_Roughness", Vector) = (0.0, 0.0, 0.0, 0.0)
	_MimeticCenter("MimeticCenter", Vector) = (0.0, 0.0, 0.0, 0.0)
	_MimeticRadiusMin("MimeticRadiusMin", Vector) = (0.0, 0.0, 0.0, 0.0)
	_MimeticRadiusMax("MimeticRadiusMax", Vector) = (0.0, 0.0, 0.0, 0.0)
	_InnerMimeticRadiusMin("InnerMimeticRadiusMin", Vector) = (0.0, 0.0, 0.0, 0.0)
	_InnerMimeticRadiusMax("InnerMimeticRadiusMax", Vector) = (0.0, 0.0, 0.0, 0.0)
	_MimeticRateAlphaRadiusMin("MimeticRateAlphaRadiusMin", Vector) = (0.0, 0.0, 0.0, 0.0)
	_MimeticRateAlphaRadiusMax("MimeticRateAlphaRadiusMax", Vector) = (0.0, 0.0, 0.0, 0.0)
	_MimeticAlphaOffseet("MimeticAlphaOffseet", Vector) = (0.0, 0.0, 0.0, 0.0)
	_Incidence_Color("Incidence_Color", Vector) = (0.0, 0.0, 0.0, 0.0)
	_Base_Tex_SRGB("Base_Tex_SRGB", 2D) = "white" {}
	_SpecularMap_Tex_LIN("SpecularMap_Tex_LIN", 2D) = "white" {}
	_Translucent_Tex_LIN("Translucent_Tex_LIN", 2D) = "white" {}
	_MetalicLayer_Tex_LIN("MetalicLayer_Tex_LIN", 2D) = "white" {}
	_MetalicBacteria_Tex_LIN("MetalicBacteria_Tex_LIN", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 400

CGPROGRAM
#pragma surface surf BlinnPhong alphatest:_Cutoff
#pragma target 3.0

sampler2D _Base_Tex_SRGB;
sampler2D _SpecularMap_Tex_LIN;

struct Input {
	float2 uv_Base_Tex_SRGB;
	float2 uv_SpecularMap_Tex_LIN;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_Base_Tex_SRGB, IN.uv_Base_Tex_SRGB);
	o.Albedo = tex.rgb;
	o.Gloss = tex.a;
	o.Alpha = tex.a;
	fixed4 specularTex = tex2D(_SpecularMap_Tex_LIN, IN.uv_SpecularMap_Tex_LIN);
	o.Specular = specularTex.r;
}
ENDCG
}

FallBack "Standard"
}