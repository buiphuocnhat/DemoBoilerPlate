﻿using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project1.Entities
{
    [Table("Test.Company")]
    public class Company : Entity
    {
        public string Name { get; set; }
    }
}
