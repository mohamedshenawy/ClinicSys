using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping.MappingHelper
{
    public class TimeSpanToStringConverter : IValueConverter<TimeSpan, string>
    {
        public string Convert(TimeSpan sourceMember, ResolutionContext context)
        {
            return sourceMember.ToString(@"hh\:mm\:ss");
        }
    }
}
