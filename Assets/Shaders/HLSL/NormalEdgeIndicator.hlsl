#ifndef NORMAL_EDGE_INDICATOR_INCLUDED
#define NORMAL_EDGE_INDICATOR_INCLUDED

struct IndicatorData {
    float depthDiff;
    float3 neighborNormal;
    float3 normal;
    float3 normalEdgeBias;
};

float GetIndicator(IndicatorData d) {
    float normalDiff = dot(d.normal - d.neighborNormal, d.normalEdgeBias);
    float depthIndicator = saturate(sign(d.depthDiff * 0.25 + .0025));
    float normalIndicator = saturate(normalDiff);

    return (1.0 - dot(d.normal, d.neighborNormal)) * depthIndicator * normalIndicator;
}

void CalculateNormalEdgeIndicator_float(float DepthDiff, float3 NeighborNormal, float3 Normal, float3 NormalEdgeBias, out float Indicator) {
    IndicatorData d;

    d.depthDiff = DepthDiff;
    d.neighborNormal = NeighborNormal;
    d.normal = Normal;
    d.normalEdgeBias = NormalEdgeBias;

    Indicator = GetIndicator(d);
}

#endif