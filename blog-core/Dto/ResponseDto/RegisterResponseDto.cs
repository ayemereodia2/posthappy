using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog_core.Dto.ResponseDto
{
    public class RegisterResponseDto: BaseResponseDto
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
    }
}