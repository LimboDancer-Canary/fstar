using System;

namespace FStarEquations
{
    public static class VarianceAmplification
    {
        /// <summary>
        /// (4) Var(O) = M^2 * Var(F)
        /// </summary>
        public static double OutputVariance(double m, double varianceF)
        {
            return m * m * varianceF;
        }

        /// <summary>
        /// (4a) Lower bound of output variance = M^2 * Var(F), same formula as (4).
        /// </summary>
        public static double OutputVarianceLowerBound(double m, double varianceF)
        {
            return m * m * varianceF;
        }

        /// <summary>
        /// (4a) Correlated output variance: Var(O) = M² × Var(F) × (1 + ρ × M).
        /// When M correlates positively with F (ρ > 0), high-force engineers
        /// extract more from the tool, so actual variance exceeds the M² prediction.
        /// At ρ = 0 this equals Eq (4). The cubic term ρ × M³ × Var(F) captures
        /// the divergence that grows faster than quadratic.
        /// </summary>
        public static double OutputVarianceCorrelated(double m, double varianceF, double rho)
        {
            return m * m * varianceF * (1.0 + rho * m);
        }

        /// <summary>
        /// (5) DeltaO = M * (F_H - F_L)
        /// </summary>
        public static double AbsoluteOutputGap(double m, double forceHigh, double forceLow)
        {
            return m * (forceHigh - forceLow);
        }

        /// <summary>
        /// (6) Piecewise market value:
        /// if F >= thresholdHigh => premiumHigh * F
        /// if F >= thresholdLow  => wageMid
        /// else                  => floorLow
        /// </summary>
        public static double MarketValue(double force, double thresholdLow, double thresholdHigh, double premiumHigh, double wageMid, double floorLow)
        {
            if (force >= thresholdHigh)
                return premiumHigh * force;
            else if (force >= thresholdLow)
                return wageMid;
            else
                return floorLow;
        }
    }
}
