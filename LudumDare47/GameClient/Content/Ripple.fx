#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler ColorMapSampler : register(s0);

float2 center;
float amplitude;
float frequency;
float phase;
float size;

float4 PixelShaderRipple(float4 pos : SV_POSITION, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
    float2 toPixel = texCoord - center;
    float distance = length(toPixel);
    float2 direction = toPixel/distance;
    float angle = atan2(direction.y, direction.x);
    float2 wave;
    float m = mad(frequency, distance, phase);
    sincos(m, wave.x, wave.y);

    float falloff = saturate(size - distance);
    falloff *= falloff;

    distance += amplitude * wave.x * falloff;
    sincos(angle, direction.y, direction.x);
    float2 uv2 = mad(direction, distance, center);

    float weight = wave.y * falloff;
    float l = mad(weight, 0.2, 0.8);
    return tex2D(ColorMapSampler, uv2) * l;
}


technique ripple
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL PixelShaderRipple();
    }
}
