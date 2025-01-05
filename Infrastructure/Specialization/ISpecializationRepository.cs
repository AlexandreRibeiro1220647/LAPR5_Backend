using TodoApi.Models.Shared;
using TodoApi.Models.Specialization;

namespace TodoApi.Infrastructure.Specialization
{
    public interface ISpecializationRepository: IRepository<Models.Specialization.Specialization, SpecializationId>
    {
    }
}