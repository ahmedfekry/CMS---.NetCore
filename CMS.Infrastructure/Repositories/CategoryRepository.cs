using CMS.Core.Repositories;
using CMS.DataAccess;
using CMS.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CMSDbContext cmsDbContext) : base(cmsDbContext)
        {
        }
    }
}
