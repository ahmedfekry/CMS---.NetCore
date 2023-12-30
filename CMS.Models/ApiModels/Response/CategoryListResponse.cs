using CMS.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.ApiModels.Response
{
    public class CategoryListResponse: Response
    {
        public List<Category> Categories { get; set; }
    }
}
