using AutoMapper;
using MedicalReports.Data.Entities;
using MedicalReports.Models;

namespace MedicalReports.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- Client Mapping
            CreateMap<Client, ClientEntity>().ReverseMap();
            CreateMap<Models.MedicalData, MedicalDataEntity>().ReverseMap();

            CreateMap<Bloodwork, BloodworkEntity>()
                .ForMember(dest => dest.CholesterolTotal, opt => opt.MapFrom(src => src.Cholesterol.Total))
                .ForMember(dest => dest.CholesterolHdl, opt => opt.MapFrom(src => src.Cholesterol.Hdl))
                .ForMember(dest => dest.CholesterolLdl, opt => opt.MapFrom(src => src.Cholesterol.Ldl))
                .ForMember(dest => dest.BloodPressureSystolic, opt => opt.MapFrom(src => src.BloodPressure.Systolic))
                .ForMember(dest => dest.BloodPressureDiastolic, opt => opt.MapFrom(src => src.BloodPressure.Diastolic))
                .ReverseMap()
                .ForPath(dest => dest.Cholesterol.Total, opt => opt.MapFrom(src => src.CholesterolTotal))
                .ForPath(dest => dest.Cholesterol.Hdl, opt => opt.MapFrom(src => src.CholesterolHdl))
                .ForPath(dest => dest.Cholesterol.Ldl, opt => opt.MapFrom(src => src.CholesterolLdl))
                .ForPath(dest => dest.BloodPressure.Systolic, opt => opt.MapFrom(src => src.BloodPressureSystolic))
                .ForPath(dest => dest.BloodPressure.Diastolic, opt => opt.MapFrom(src => src.BloodPressureDiastolic));

            CreateMap<Questionnaire, QuestionaireEntity>().ReverseMap();

            // --- Guidelines Mapping
            CreateMap<MedicalGuidelines, GuidelineEntity>()
                // Cholesterol
                .ForMember(d => d.CholesterolTotalOptimal, opt => opt.MapFrom(s => s.Cholesterol.Total.Optimal))
                .ForMember(d => d.CholesterolTotalNeedsAttention, opt => opt.MapFrom(s => s.Cholesterol.Total.NeedsAttention))
                .ForMember(d => d.CholesterolTotalSeriousIssue, opt => opt.MapFrom(s => s.Cholesterol.Total.SeriousIssue))

                .ForMember(d => d.CholesterolHdlOptimal, opt => opt.MapFrom(s => s.Cholesterol.Hdl.Optimal))
                .ForMember(d => d.CholesterolHdlNeedsAttention, opt => opt.MapFrom(s => s.Cholesterol.Hdl.NeedsAttention))
                .ForMember(d => d.CholesterolHdlSeriousIssue, opt => opt.MapFrom(s => s.Cholesterol.Hdl.SeriousIssue))

                .ForMember(d => d.CholesterolLdlOptimal, opt => opt.MapFrom(s => s.Cholesterol.Ldl.Optimal))
                .ForMember(d => d.CholesterolLdlNeedsAttention, opt => opt.MapFrom(s => s.Cholesterol.Ldl.NeedsAttention))
                .ForMember(d => d.CholesterolLdlSeriousIssue, opt => opt.MapFrom(s => s.Cholesterol.Ldl.SeriousIssue))

                // Blood sugar
                .ForMember(d => d.BloodSugarOptimal, opt => opt.MapFrom(s => s.BloodSugar.Optimal))
                .ForMember(d => d.BloodSugarNeedsAttention, opt => opt.MapFrom(s => s.BloodSugar.NeedsAttention))
                .ForMember(d => d.BloodSugarSeriousIssue, opt => opt.MapFrom(s => s.BloodSugar.SeriousIssue))

                // Blood pressure
                .ForMember(d => d.BloodPressureOptimalSystolic, opt => opt.MapFrom(s => s.BloodPressure.Optimal.Systolic))
                .ForMember(d => d.BloodPressureOptimalDiastolic, opt => opt.MapFrom(s => s.BloodPressure.Optimal.Diastolic))
                .ForMember(d => d.BloodPressureNeedsAttentionSystolic, opt => opt.MapFrom(s => s.BloodPressure.NeedsAttention.Systolic))
                .ForMember(d => d.BloodPressureNeedsAttentionDiastolic, opt => opt.MapFrom(s => s.BloodPressure.NeedsAttention.Diastolic))
                .ForMember(d => d.BloodPressureSeriousIssueSystolic, opt => opt.MapFrom(s => s.BloodPressure.SeriousIssue.Systolic))
                .ForMember(d => d.BloodPressureSeriousIssueDiastolic, opt => opt.MapFrom(s => s.BloodPressure.SeriousIssue.Diastolic))

                // Lifestyle
                .ForMember(d => d.ExerciseWeeklyMinutesOptimal, opt => opt.MapFrom(s => s.ExerciseWeeklyMinutes.Optimal))
                .ForMember(d => d.ExerciseWeeklyMinutesNeedsAttention, opt => opt.MapFrom(s => s.ExerciseWeeklyMinutes.NeedsAttention))
                .ForMember(d => d.ExerciseWeeklyMinutesSeriousIssue, opt => opt.MapFrom(s => s.ExerciseWeeklyMinutes.SeriousIssue))

                .ForMember(d => d.SleepQualityOptimal, opt => opt.MapFrom(s => s.SleepQuality.Optimal))
                .ForMember(d => d.SleepQualityNeedsAttention, opt => opt.MapFrom(s => s.SleepQuality.NeedsAttention))
                .ForMember(d => d.SleepQualitySeriousIssue, opt => opt.MapFrom(s => s.SleepQuality.SeriousIssue))

                .ForMember(d => d.StressLevelsOptimal, opt => opt.MapFrom(s => s.StressLevels.Optimal))
                .ForMember(d => d.StressLevelsNeedsAttention, opt => opt.MapFrom(s => s.StressLevels.NeedsAttention))
                .ForMember(d => d.StressLevelsSeriousIssue, opt => opt.MapFrom(s => s.StressLevels.SeriousIssue))

                .ForMember(d => d.DietQualityOptimal, opt => opt.MapFrom(s => s.DietQuality.Optimal))
                .ForMember(d => d.DietQualityNeedsAttention, opt => opt.MapFrom(s => s.DietQuality.NeedsAttention))
                .ForMember(d => d.DietQualitySeriousIssue, opt => opt.MapFrom(s => s.DietQuality.SeriousIssue))

                .ReverseMap();
        }
    }
}