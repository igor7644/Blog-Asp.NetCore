using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using Business.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IAddPostCommand _addPost;
        private readonly IGetPostsCommand _getPosts;
        private readonly IGetPostCommand _getOnePost;
        private readonly IEditPostCommand _editPost;
        private readonly IDeletePostCommand _deletePost;

        public PostsController(IAddPostCommand addPost, IGetPostsCommand getPosts, IGetPostCommand getOnePost, IEditPostCommand editPost, IDeletePostCommand deletePost)
        {
            _addPost = addPost;
            _getPosts = getPosts;
            _getOnePost = getOnePost;
            _editPost = editPost;
            _deletePost = deletePost;
        }


        // GET: Posts
        public ActionResult Index(PostSearch search)
        {
            var posts = _getPosts.Execute(search);
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var dto = _getOnePost.Execute(id);
                return View(dto);
            }
            catch (Exception)
            {
                return View();
            }

            
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostDTO collection)
        {
            if(!ModelState.IsValid)
            {
                return View(collection);
            }

            try
            {
                _addPost.Execute(collection);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityExistException)
            {
                TempData["error"] = "Post with same name already exist!";
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occured!";
            }

            return View();
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getOnePost.Execute(id);
                return View(dto);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _editPost.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityExistException)
            {
                TempData["error"] = "Post with same name already exist!";
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PostDTO dto)
        {
            try
            {
                _deletePost.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(EntityNotFoundException)
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
    }
}