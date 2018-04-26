using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CarDealer.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTagPaging(string tag, int pageIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTagPaging(string tagId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tagId && p.Status
                        orderby p.CreatedDate descending
                        select p;

            totalRow = query.Count();

            query = query.OrderByDescending(x=>x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }
    }
}