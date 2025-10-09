using AutoMapper;
using MedicalReports.Data;
using MedicalReports.Data.Entities;
using MedicalReports.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalReports.Repositories
{
    public class GuidelineRepository : IGuidelineRepository
    {
        private readonly MedicalDataContext _context;
        private readonly IMapper _mapper;

        public GuidelineRepository(MedicalDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MedicalGuidelines?> GetMedicalGuidelinesAsync()
        {
            var entity = await _context.Guidelines.FirstOrDefaultAsync();
            return _mapper.Map<MedicalGuidelines?>(entity);
        }

        public async Task SaveMedicalGuidelinesAsync(MedicalGuidelines guidelines)
        {
            var existing = await _context.Guidelines.FirstOrDefaultAsync();

            if (existing == null)
            {
                var entity = _mapper.Map<GuidelineEntity>(guidelines);
                _context.Guidelines.Add(entity);
            }
            else
            {
                _mapper.Map(guidelines, existing);
            }

            await _context.SaveChangesAsync();
        }
    }
}
