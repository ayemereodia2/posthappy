using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog_core.Dto.ResponseDto
{
    public class BaseResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ErrorDto>? Errors { get; set; }
    }
}