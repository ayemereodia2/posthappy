using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog_core.Dto
{
    public record ErrorDto
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}