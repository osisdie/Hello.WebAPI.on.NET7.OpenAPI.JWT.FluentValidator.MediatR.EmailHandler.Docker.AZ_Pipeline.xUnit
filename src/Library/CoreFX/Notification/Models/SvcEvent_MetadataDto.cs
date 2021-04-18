using System.Collections.Generic;
using System.Linq;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts;
using MediatR;

namespace CoreFX.Notification.Models
{
    public class SvcEvent_MetadataDto : SvcResponseDto, INotification
    {
        public string From { get; set; }
        public string Category { get; set; }
        public string User { get; set; }
        public object RequestDto { get; set; }
        public ISvcResponseBaseDto ResponseDto { get; set; }
        public object Context { get; set; }

        public SvcEvent_MetadataDto(IDictionary<string, string> meta = null)
        {
            if (meta?.Count > 0)
            {
                ExtMap = ExtMap.Union(meta).GroupBy(d => d.Key)
                    .ToDictionary(d => d.Key, d => d.First().Value);
            }
        }
    }
}
