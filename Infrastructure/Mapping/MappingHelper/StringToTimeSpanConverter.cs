using AutoMapper;

namespace Infrastructure.Mapping.MappingHelper
{
    public class StringToTimeSpanConverter : IValueConverter<string, TimeSpan>
    {
        public TimeSpan Convert(string sourceMember, ResolutionContext context)
        {
            return TimeSpan.Parse(sourceMember);
        }
    }
}
