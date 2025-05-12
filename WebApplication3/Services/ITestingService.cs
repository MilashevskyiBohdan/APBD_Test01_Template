using test1template.Models;

namespace test1template.Services;

public interface ITestingService
{
    Task<SampleDTO?> GetSample(int id);
    Task<string> AddSample(SampleDTO dto);
    Task<string> DeleteSample(int id);
}