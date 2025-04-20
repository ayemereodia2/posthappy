using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_core.Dto.ResponseDto;

namespace blog_core.Dto.RequestDto
{
    public class LoginResponseDto: BaseResponseDto
    {
        public string Token { get; set; }
    }
}