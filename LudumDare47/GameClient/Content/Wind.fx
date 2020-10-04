#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler ColorMapSampler : register(s0);

float time;
float2 wind_direction;
float wind_strength;
float magic_number;

float4 PixelShaderWind(float4 pos : SV_POSITION, float4 color : COLOR, float4 texCoord : TEXCOORD0) : SV_TARGET0
{
    float2 wind = sin(time + (texCoord.x + texCoord.y) * magic_number) * wind_direction * wind_strength;
    texCoord.xy += wind;

    return tex2D(ColorMapSampler, texCoord.xy) * color;
}

technique wind
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL PixelShaderWind();
    }
}
