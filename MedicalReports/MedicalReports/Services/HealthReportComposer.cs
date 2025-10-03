using MedicalReports.Domain;
using MedicalReports.Models;

namespace MedicalReports.Services
{
    public class HealthReportComposer : IHealthReportComposer
    {
        public ClientHealthReport BuildReport(Client client, MedicalGuidelines guidelines)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentNullException.ThrowIfNull(guidelines);

            var evaluator = new HealthEvaluator(guidelines);

            var report = new ClientHealthReport
            {
                ClientId = client.Id,
                ClientName = client.Name,
                HealthMetrics = new(),
                QualitativeMetrics = new()
            };

            AddCholesterol(report, client, guidelines, evaluator);
            AddBloodSugar(report, client, guidelines, evaluator);
            AddBloodPressure(report, client, guidelines, evaluator);
            AddLifestyle(report, client, guidelines, evaluator);

            return report;
        }

        private static void AddCholesterol(ClientHealthReport report, Client client, MedicalGuidelines g, HealthEvaluator ev)
        {
            var c = client.MedicalData?.Bloodwork?.Cholesterol;
            if (c == null) return;

            report.HealthMetrics["CholesterolTotal"] = Metric(c.Total, g.Cholesterol.Total, ev);
            report.HealthMetrics["CholesterolHdl"] = Metric(c.Hdl, g.Cholesterol.Hdl, ev);
            report.HealthMetrics["CholesterolLdl"] = Metric(c.Ldl, g.Cholesterol.Ldl, ev);
        }

        private static void AddBloodSugar(ClientHealthReport report, Client client, MedicalGuidelines g, HealthEvaluator ev)
        {
            var bw = client.MedicalData?.Bloodwork;
            if (bw == null) return;

            report.HealthMetrics["BloodSugar"] = Metric(bw.BloodSugar, g.BloodSugar, ev);
        }

        private static void AddBloodPressure(ClientHealthReport report, Client client, MedicalGuidelines g, HealthEvaluator ev)
        {
            var p = client.MedicalData?.Bloodwork?.BloodPressure;
            if (p == null) return;

            report.HealthMetrics["BloodPressure"] = new MetricResult
            {
                ClientValue = p.Systolic,
                Status = ev.EvaluateBloodPressure(p.Systolic, p.Diastolic, g.BloodPressure),
                GuidelineRange = FormatBP(g.BloodPressure)
            };
        }

        private static void AddLifestyle(ClientHealthReport report, Client client, MedicalGuidelines g, HealthEvaluator ev)
        {
            var q = client.MedicalData?.Questionnaire;
            if (q == null) return;

            report.QualitativeMetrics["ExerciseWeeklyMinutes"] = Qualitative(
                q.ExerciseWeeklyMinutes.ToString(),
                g.ExerciseWeeklyMinutes,
                ev.EvaluateMetric,
                q.ExerciseWeeklyMinutes);

            report.QualitativeMetrics["SleepQuality"] = Qualitative(q.SleepQuality, g.SleepQuality, ev.EvaluateQualitative);
            report.QualitativeMetrics["StressLevels"] = Qualitative(q.StressLevels, g.StressLevels, ev.EvaluateQualitative);
            report.QualitativeMetrics["DietQuality"] = Qualitative(q.DietQuality, g.DietQuality, ev.EvaluateQualitative);
        }

        private static MetricResult Metric(double value, MetricGuideline g, HealthEvaluator ev) =>
            new()
            {
                ClientValue = value,
                Status = ev.EvaluateMetric(value, g),
                GuidelineRange = $"{g.Optimal}/{g.NeedsAttention}/{g.SeriousIssue}"
            };

        private static QualitativeResult Qualitative(string value, QualitativeGuideline g, Func<string, QualitativeGuideline, string> ev) =>
            new()
            {
                ClientValue = value,
                Status = ev(value, g),
                GuidelineRange = $"{g.Optimal}/{g.NeedsAttention}/{g.SeriousIssue}"
            };

        private static QualitativeResult Qualitative(string value, MetricGuideline g, Func<double, MetricGuideline, string> ev, double numeric) =>
            new()
            {
                ClientValue = value,
                Status = ev(numeric, g),
                GuidelineRange = $"{g.Optimal}/{g.NeedsAttention}/{g.SeriousIssue}"
            };

        private static string FormatBP(BloodPressureGuideline g) =>
            $"Optimal(S:{g.Optimal.Systolic}, D:{g.Optimal.Diastolic}) | " +
            $"Attention(S:{g.NeedsAttention.Systolic}, D:{g.NeedsAttention.Diastolic}) | " +
            $"Serious(S:{g.SeriousIssue.Systolic}, D:{g.SeriousIssue.Diastolic})";
    }
}
