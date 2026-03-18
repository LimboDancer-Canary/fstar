using FStarEquations;

namespace FStarEquationsTests
{
    public class VarianceAmplificationTests
    {
        // --- OutputVariance tests ---

        [Fact]
        public void OutputVariance_KnownValues_ReturnsMSquaredTimesVariance()
        {
            // M=5, Var(F)=0.04
            // Var(O) = 5^2 * 0.04 = 25 * 0.04 = 1.0
            double expected = 1.0;

            double actual = VarianceAmplification.OutputVariance(5.0, 0.04);

            Assert.Equal(expected, actual, 10);
        }

        [Theory]
        [InlineData(1.0, 0.04, 0.04)]   // 1^2 * 0.04 = 0.04
        [InlineData(2.0, 0.04, 0.16)]   // 4 * 0.04 = 0.16
        [InlineData(3.0, 0.04, 0.36)]   // 9 * 0.04 = 0.36
        [InlineData(10.0, 0.04, 4.0)]   // 100 * 0.04 = 4.0
        public void OutputVariance_ScalesAsMSquared_ReturnsCorrectValue(double m, double varianceF, double expected)
        {
            double actual = VarianceAmplification.OutputVariance(m, varianceF);

            Assert.Equal(expected, actual, 10);
        }

        // --- OutputVarianceLowerBound tests ---

        [Fact]
        public void OutputVarianceLowerBound_SameAsOutputVariance_ReturnsSameResult()
        {
            // Lower bound uses same formula: M^2 * Var(F)
            // M=5, Var(F)=0.04 => 25 * 0.04 = 1.0
            double expected = VarianceAmplification.OutputVariance(5.0, 0.04);

            double actual = VarianceAmplification.OutputVarianceLowerBound(5.0, 0.04);

            Assert.Equal(expected, actual, 10);
        }

        // --- OutputVarianceCorrelated tests ---

        [Fact]
        public void OutputVarianceCorrelated_ZeroRho_EqualsOutputVariance()
        {
            // rho=0 => M^2 * Var(F) * (1 + 0) = M^2 * Var(F)
            double expected = VarianceAmplification.OutputVariance(3.0, 2.0);

            double actual = VarianceAmplification.OutputVarianceCorrelated(3.0, 2.0, 0.0);

            Assert.Equal(expected, actual, 10);
        }

        [Theory]
        [InlineData(3.0, 2.0, 0.5, 45.0)]   // 9 * 2 * (1 + 0.5*3) = 18 * 2.5 = 45.0
        [InlineData(2.0, 1.0, 1.0, 12.0)]    // 4 * 1 * (1 + 1*2) = 4 * 3 = 12
        [InlineData(5.0, 0.04, 0.5, 3.5)]    // 25 * 0.04 * (1 + 0.5*5) = 1.0 * 3.5 = 3.5
        public void OutputVarianceCorrelated_PositiveRho_ExceedsBaseline(double m, double varF, double rho, double expected)
        {
            double actual = VarianceAmplification.OutputVarianceCorrelated(m, varF, rho);

            Assert.Equal(expected, actual, 10);
        }

        [Fact]
        public void OutputVarianceCorrelated_PositiveRho_GreaterThanUncorrelated()
        {
            double baseline = VarianceAmplification.OutputVariance(3.0, 2.0);
            double correlated = VarianceAmplification.OutputVarianceCorrelated(3.0, 2.0, 0.5);

            Assert.True(correlated > baseline, "Correlated variance should exceed baseline when rho > 0");
        }

        // --- AbsoluteOutputGap tests ---

        [Fact]
        public void AbsoluteOutputGap_KnownValues_ReturnsMTimesDifference()
        {
            // M=4, F_H=0.9, F_L=0.3
            // DeltaO = 4 * (0.9 - 0.3) = 4 * 0.6 = 2.4
            double expected = 2.4;

            double actual = VarianceAmplification.AbsoluteOutputGap(4.0, 0.9, 0.3);

            Assert.Equal(expected, actual, 10);
        }

        [Fact]
        public void AbsoluteOutputGap_EqualForces_ReturnsZero()
        {
            // M=10, F_H=0.5, F_L=0.5
            // DeltaO = 10 * (0.5 - 0.5) = 0
            double expected = 0.0;

            double actual = VarianceAmplification.AbsoluteOutputGap(10.0, 0.5, 0.5);

            Assert.Equal(expected, actual, 10);
        }

        // --- MarketValue tests ---

        [Fact]
        public void MarketValue_HighTier_ReturnsPremiumTimesForce()
        {
            // F=0.95 >= thresholdHigh=0.9 => premiumHigh * F = 100 * 0.95 = 95.0
            double expected = 95.0;

            double actual = VarianceAmplification.MarketValue(
                force: 0.95, thresholdLow: 0.4, thresholdHigh: 0.9,
                premiumHigh: 100.0, wageMid: 50.0, floorLow: 20.0);

            Assert.Equal(expected, actual, 10);
        }

        [Fact]
        public void MarketValue_MidTier_ReturnsWageMid()
        {
            // F=0.6 >= thresholdLow=0.4 but < thresholdHigh=0.9 => wageMid=50
            double expected = 50.0;

            double actual = VarianceAmplification.MarketValue(
                force: 0.6, thresholdLow: 0.4, thresholdHigh: 0.9,
                premiumHigh: 100.0, wageMid: 50.0, floorLow: 20.0);

            Assert.Equal(expected, actual, 10);
        }

        [Fact]
        public void MarketValue_LowTier_ReturnsFloorLow()
        {
            // F=0.2 < thresholdLow=0.4 => floorLow=20
            double expected = 20.0;

            double actual = VarianceAmplification.MarketValue(
                force: 0.2, thresholdLow: 0.4, thresholdHigh: 0.9,
                premiumHigh: 100.0, wageMid: 50.0, floorLow: 20.0);

            Assert.Equal(expected, actual, 10);
        }

        [Fact]
        public void MarketValue_AtHighBoundary_ReturnsPremiumTimesForce()
        {
            // F=0.9 == thresholdHigh=0.9 => premiumHigh * F = 100 * 0.9 = 90.0
            double expected = 90.0;

            double actual = VarianceAmplification.MarketValue(
                force: 0.9, thresholdLow: 0.4, thresholdHigh: 0.9,
                premiumHigh: 100.0, wageMid: 50.0, floorLow: 20.0);

            Assert.Equal(expected, actual, 10);
        }

        [Fact]
        public void MarketValue_AtLowBoundary_ReturnsWageMid()
        {
            // F=0.4 == thresholdLow=0.4 => wageMid=50
            double expected = 50.0;

            double actual = VarianceAmplification.MarketValue(
                force: 0.4, thresholdLow: 0.4, thresholdHigh: 0.9,
                premiumHigh: 100.0, wageMid: 50.0, floorLow: 20.0);

            Assert.Equal(expected, actual, 10);
        }
    }
}
