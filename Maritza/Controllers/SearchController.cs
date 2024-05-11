using Maritza.Controllers.Base;
using MaritzaBusness;
using MaritzaData;
using MaritzaData.ConfigClasses;
using MaritzaData.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maritza.Controllers
{
    public class SearchController : BaseController
    {
        ProyectCommentsB ProyectCommentsB = new ProyectCommentsB();
        CharacteristicsB CharacteristicsB = new CharacteristicsB();
        CommentsLikesB CommentsLikesB = new CommentsLikesB();
        ProyectLikesB ProyectLikesB = new ProyectLikesB();
        ProyectTypesB ProyectTypesB = new ProyectTypesB();
        SearchB SearchB = new SearchB();
        TopicsB TopicsB = new TopicsB();
        GenderB GenderB = new GenderB();
        [AllowAnonymous]
        public ActionResult Search(fltSearch filters = null)
        {
            ViewBag.lstProyectTypes = ProyectTypesB.GetList();
            ViewBag.lstTopics = TopicsB.GetList();
            ViewBag.lstGenders = GenderB.GetList();
            ViewBag.filters = filters;
            ViewBag.lstCharacteristics = CharacteristicsB.getList();
            var items = SearchB.GetAllItems(filters);
            if (Request.IsAjaxRequest())
                return PartialView("_Search", items);
            return View(items);
        }
        [AllowAnonymous]
        public ActionResult ProyectDetails(int iId)
        {
            var model = SearchB.GetProyectDetails(iId);
            if (model == null)
                return HttpNotFound();
            model.lstComments = ProyectCommentsB.getListCommentsByProyectIDNoanswers(iId);
            if (Request.IsAuthenticated)
                model.tblProyectLikes = ProyectLikesB.getByUserIDAndProyectDetailsID(CurrentUserID, model.ProyectDetailID);
            return View(model);
        }
        [AllowAnonymous]
        public JsonResult CommentProyect(string Comment, int ProyectID,bool IsAnswer,int sProyectCommentID)
        {
            tblProyectComments tblProyectComments = new tblProyectComments();
            tblProyectComments.Comment = Comment;
            tblProyectComments.ProyectID = ProyectID;
            tblProyectComments.UserID = CurrentUserID;
            tblProyectComments.IsAnswer = IsAnswer;
            tblProyectComments.sProyectCommentID = sProyectCommentID;
            tblProyectComments.Active = true;
            tblProyectComments.CreatedBy = tblProyectComments.UpdatedBy = CurrentUserID;
            tblProyectComments.CreatedDate = tblProyectComments.UpdatedDate = DateTime.Now;

            Response response = ProyectCommentsB.Create(tblProyectComments);
            if (response.Result != Result.Ok)
            {
                return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "success", message = "Se ha guardado correctamente.", comment = Comment }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LikeComment(bool? like, int ProyectCommentID)
        {
            if (ProyectLikesB.CheckIfExistsByUserIDAndProyectDetailsID(CurrentUserID, ProyectCommentID))
            {
                tblCommentsLikes tblCommentsLikes = CommentsLikesB.getByUserIDAndProyectCommentID(CurrentUserID, ProyectCommentID);
                tblCommentsLikes.IsLiked = like;
                tblCommentsLikes.UpdatedBy = CurrentUserID;
                tblCommentsLikes.UpdatedDate = DateTime.Now;
                Response response = CommentsLikesB.Update(tblCommentsLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tblCommentsLikes tblCommentsLikes = new tblCommentsLikes();
                tblCommentsLikes.IsLiked = like;
                tblCommentsLikes.ProyectCommentID = ProyectCommentID;
                tblCommentsLikes.UserID = CurrentUserID;
                tblCommentsLikes.Active = true;
                tblCommentsLikes.CreatedBy = tblCommentsLikes.UpdatedBy = CurrentUserID;
                tblCommentsLikes.CreatedDate = tblCommentsLikes.UpdatedDate = DateTime.Now;
                Response response = CommentsLikesB.Create(tblCommentsLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "success", message = "Se ha guardado correctamente.", IsLiked = like }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DislikeComment(bool? dislike, int ProyectCommentID)
        {
            bool? isnull = null;
            if (ProyectLikesB.CheckIfExistsByUserIDAndProyectDetailsID(CurrentUserID, ProyectCommentID))
            {
                tblCommentsLikes tblCommentsLikes = CommentsLikesB.getByUserIDAndProyectCommentID(CurrentUserID, ProyectCommentID);
                tblCommentsLikes.IsLiked = dislike != null ? false : isnull;
                tblCommentsLikes.UpdatedBy = CurrentUserID;
                tblCommentsLikes.UpdatedDate = DateTime.Now;
                Response response = CommentsLikesB.Update(tblCommentsLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tblCommentsLikes tblCommentsLikes = new tblCommentsLikes();
                tblCommentsLikes.IsLiked = dislike;
                tblCommentsLikes.ProyectCommentID = ProyectCommentID;
                tblCommentsLikes.UserID = CurrentUserID;
                tblCommentsLikes.Active = true;
                tblCommentsLikes.CreatedBy = tblCommentsLikes.UpdatedBy = CurrentUserID;
                tblCommentsLikes.CreatedDate = tblCommentsLikes.UpdatedDate = DateTime.Now;
                Response response = CommentsLikesB.Create(tblCommentsLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "success", message = "Se ha guardado correctamente.", IsLiked = dislike }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LikeProyect(bool? like, int ProyectDetailID)
        {

            if (ProyectLikesB.CheckIfExistsByUserIDAndProyectDetailsID(CurrentUserID, ProyectDetailID))
            {

                tblProyectLikes tblProyectLikes = ProyectLikesB.getByUserIDAndProyectDetailsID(CurrentUserID, ProyectDetailID);
                tblProyectLikes.IsLiked = like;
                tblProyectLikes.UpdatedBy = CurrentUserID;
                tblProyectLikes.UpdatedDate = DateTime.Now;
                Response response = ProyectLikesB.Update(tblProyectLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tblProyectLikes tblProyectLikes = new tblProyectLikes();
                tblProyectLikes.IsLiked = like;
                tblProyectLikes.ProyectDetailID = ProyectDetailID;
                tblProyectLikes.UserID = CurrentUserID;
                tblProyectLikes.Active = true;
                tblProyectLikes.CreatedBy = tblProyectLikes.UpdatedBy = CurrentUserID;
                tblProyectLikes.CreatedDate = tblProyectLikes.UpdatedDate = DateTime.Now;
                Response response = ProyectLikesB.Create(tblProyectLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "success", message = "Se ha guardado correctamente.", IsLiked = like }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DislikeProyect(bool? dislike, int ProyectDetailID)
        {
            bool? isnull = null;
            if (ProyectLikesB.CheckIfExistsByUserIDAndProyectDetailsID(CurrentUserID, ProyectDetailID))
            {
                tblProyectLikes tblProyectLikes = ProyectLikesB.getByUserIDAndProyectDetailsID(CurrentUserID, ProyectDetailID);
                tblProyectLikes.IsLiked = dislike != null ? false : isnull;
                tblProyectLikes.UpdatedBy = CurrentUserID;
                tblProyectLikes.UpdatedDate = DateTime.Now;
                Response response = ProyectLikesB.Update(tblProyectLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tblProyectLikes tblProyectLikes = new tblProyectLikes();
                tblProyectLikes.IsLiked = dislike;
                tblProyectLikes.ProyectDetailID = ProyectDetailID;
                tblProyectLikes.UserID = CurrentUserID;
                tblProyectLikes.Active = true;
                tblProyectLikes.CreatedBy = tblProyectLikes.UpdatedBy = CurrentUserID;
                tblProyectLikes.CreatedDate = tblProyectLikes.UpdatedDate = DateTime.Now;
                Response response = ProyectLikesB.Create(tblProyectLikes);
                if (response.Result != Result.Ok)
                    return Json(new { status = "Error", message = "Algo fallo." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "success", message = "Se ha guardado correctamente.", IsLiked = dislike }, JsonRequestBehavior.AllowGet);
        }

    }
}
