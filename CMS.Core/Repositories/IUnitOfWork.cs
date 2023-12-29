using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; set; }
        IContentRepository ContentRepository { get; set; }

        void SaveChanges();
    }
}
