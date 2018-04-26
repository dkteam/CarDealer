using CarDealer.Common;
using CarDealer.Data.Infrastructure;
using CarDealer.Data.Repositories;
using CarDealer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Service
{
    public interface IPostService
    {
        Post Add(Post post);
        void Update(Post post);
        Post Delete(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAll(string keyWord);
        IEnumerable<Post> GetLatestPosts(int top);
        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);
        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);
        IEnumerable<Post> GetPostsByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);
        Post GetById(int id);
        IEnumerable<Post> GetAllByTagPaging(string tagId, int page, int pageSize, out int totalRow);
        IEnumerable<Tag> GetListTagByPostId(int id);
        IEnumerable<Post> GetReatedPosts(int id, int top);
        Tag GetTag(string tagId);
        void IncreaseView(int id);
        void SaveChanges();
    }

    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;
        private IPostTagRepository _postTagRepository;
        private IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository,
                            ITagRepository tagRepository,
                            IPostTagRepository postTagRepository,
                            IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._tagRepository = tagRepository;
            this._postTagRepository = postTagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Post Add(Post post)
        {
            var newPost = _postRepository.Add(post);
            _unitOfWork.Commit();

            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.PostTag;

                        _tagRepository.Add(tag);
                    }
                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = tagId;

                    _postTagRepository.Add(postTag);
                }
            }
            return newPost;
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _postRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord) || x.PostCategory.Name.Contains(keyWord));
            else
                return _postRepository.GetAll();
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tagId, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all post by tag
            return _postRepository.GetAllByTagPaging(tagId, page, pageSize, out totalRow);

        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.PostTag;

                        _tagRepository.Add(tag);
                    }
                    _postTagRepository.DeleteMulti(x => x.PostID == post.ID);
                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = tagId;

                    _postTagRepository.Add(postTag);
                }

            }
        }
        public IEnumerable<Post> GetLatestPosts(int top)
        {
            return _postRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Post> GetPostsByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _postRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case "new":
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
                case "viewcount":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;

            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Post> GetReatedPosts(int id, int top)
        {
            var product = _postRepository.GetSingleById(id);
            return _postRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Tag> GetListTagByPostId(int id)
        {
            return _postTagRepository.GetMulti(x => x.PostID == id, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        public void IncreaseView(int id)
        {
            var car = _postRepository.GetSingleById(id);
            if (car.ViewCount.HasValue)
                car.ViewCount += 1;
            else
                car.ViewCount = 1;
            _unitOfWork.Commit();
        }

    }
}
