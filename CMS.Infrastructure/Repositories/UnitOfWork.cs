using CMS.Core.Repositories;
using CMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CMSDbContext cmsDbContext) 
        {
            CategoryRepository = new CategoryRepository(cmsDbContext);
            ContentRepository = new ContentRepository(cmsDbContext);
            _cmsDbContext = cmsDbContext;
        }

        public ICategoryRepository CategoryRepository { get; set; }
        public IContentRepository ContentRepository { get; set; }
        public CMSDbContext _cmsDbContext { get; }

        public void SaveChanges()
        {
            _cmsDbContext.SaveChanges();
        }
    }
}
