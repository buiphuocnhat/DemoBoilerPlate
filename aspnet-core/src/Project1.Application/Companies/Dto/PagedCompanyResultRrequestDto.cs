using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Companies.Dto
{
    public class PagedCompanyResultRrequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
