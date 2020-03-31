using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Cars
{
    public class PagedCarResultRequestDto : PagedResultRequestDto
    {
        public string Keyword {get;set;}
    }
}
