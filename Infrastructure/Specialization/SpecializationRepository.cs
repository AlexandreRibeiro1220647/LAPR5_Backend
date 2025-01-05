using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Staff;
using TodoApi.DTOs;
using TodoApi.Models.Specialization;
using TodoApi.Infrastructure.Shared;
using TodoApi.Infrastructure;

namespace TodoApi.Infrastructure.Specialization
{
    public class SpecializationRepository : BaseRepository<Models.Specialization.Specialization, SpecializationId>, ISpecializationRepository
    {

        public SpecializationRepository(IPOContext context):base(context.Specializations)
        {

        }

    }
}