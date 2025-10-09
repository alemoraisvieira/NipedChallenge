using MedicalReports.Models;

namespace MedicalReports.Domain
{
    public class HealthEvaluator
    {
        private readonly MedicalGuidelines _g;

        public HealthEvaluator(MedicalGuidelines guidelines)
        {
            _g = guidelines ?? throw new ArgumentNullException(nameof(guidelines));
        }

        public string EvaluateMetric(double val, MetricGuideline? g)
        {
            if (g == null) return "Guideline Missing";
            if (Check(val, g.Optimal)) return "Optimal";
            if (Check(val, g.NeedsAttention)) return "Needs Attention";
            if (Check(val, g.SeriousIssue)) return "Serious Issue";
            return "Out of Range";
        }

        public string EvaluateBloodPressure(int sys, int dia, BloodPressureGuideline? g)
        {
            if (g == null) return "Guideline Missing";

            if (CheckBP(sys, dia, g.SeriousIssue, orMode: true)) return "Serious Issue";
            if (CheckBP(sys, dia, g.NeedsAttention)) return "Needs Attention";
            if (CheckBP(sys, dia, g.Optimal)) return "Optimal";

            return "Out of Range";
        }

        public string EvaluateQualitative(string? val, QualitativeGuideline? g)
        {
            if (g == null) return "Guideline Missing";
            if (string.IsNullOrWhiteSpace(val)) return "Missing Value";

            if (val.Contains(g.Optimal, StringComparison.OrdinalIgnoreCase)) return "Optimal";
            if (val.Contains(g.NeedsAttention, StringComparison.OrdinalIgnoreCase)) return "Needs Attention";
            if (val.Contains(g.SeriousIssue, StringComparison.OrdinalIgnoreCase)) return "Serious Issue";

            return "Uncategorized";
        }

        private static bool Check(double v, string? rule)
        {
            if (string.IsNullOrWhiteSpace(rule)) return false;
            rule = rule.Trim();

            if (rule.StartsWith("<=") && double.TryParse(rule[2..], out var max1)) return v <= max1;
            if (rule.StartsWith("<") && double.TryParse(rule[1..], out var max2)) return v < max2;
            if (rule.StartsWith(">=") && double.TryParse(rule[2..], out var min1)) return v >= min1;
            if (rule.StartsWith(">") && double.TryParse(rule[1..], out var min2)) return v > min2;
            if (rule.Contains('-') && double.TryParse(rule.Split('-')[0], out var low) && double.TryParse(rule.Split('-')[1], out var high)) return v >= low && v <= high;
            return double.TryParse(rule, out var exact) && v == exact;
        }

        private bool CheckBP(int sys, int dia, BloodPressureRange bp, bool orMode = false)
        {
            if (string.IsNullOrWhiteSpace(bp.Systolic) || string.IsNullOrWhiteSpace(bp.Diastolic)) return false;
            var sOk = Check(sys, bp.Systolic);
            var dOk = Check(dia, bp.Diastolic);
            return orMode ? (sOk || dOk) : (sOk && dOk);
        }
    }
}
