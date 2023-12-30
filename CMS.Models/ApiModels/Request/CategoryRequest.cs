using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.ApiModels.Request
{
    public class CategoryRequest
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
