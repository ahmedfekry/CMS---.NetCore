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
    public class ContentRepository : Repository<Content>, IContentRepository
    {
        public ContentRepository(CMSDbContext cmsDbContext) : base(cmsDbContext)
        {
        }
    }
}
