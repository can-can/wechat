using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Wechat.Api.Abstracts;
using Wechat.Api.Extensions;
using Wechat.Api.Helper;
using Wechat.Api.Request.Common;
using Wechat.Api.Request.FriendCircle;
using Wechat.Api.Response.FriendCircle;
using Wechat.Protocol;
using Wechat.Util.Exceptions;
using Wechat.Util.Extensions;

namespace Wechat.Api.Controllers
{
    /// <summary>
    /// 朋友圈
    /// </summary>
    public class FriendCircleController : WebchatControllerBase
    {

        /// <summary>
        /// 获取特定人朋友圈
        /// </summary>
        /// <param name="friendCircle"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FriendCircle/GetFriendCircleDetail")]
        public Task<HttpResponseMessage> GetFriendCircleDetail(FriendCircle friendCircle)
        {
            ResponseBase<FriendCircleResponse> response = new ResponseBase<FriendCircleResponse>();
            try
            {
                var result = wechat.SnsUserPage(friendCircle.FristPageMd5, friendCircle.WxId, friendCircle.ToWxId);
                if (result == null || result.baseResponse.ret != (int)MMPro.MM.RetConst.MM_OK)
                {
                    response.Success = false;
                    response.Code = "402";
                    response.Message = result.baseResponse.errMsg.@string ?? "获取失败";
                }
                else
                {
                    response.Data = new FriendCircleResponse()
                    {
                        FristPageMd5 = result.fristPageMd5,
                        ObjectList = result.objectList
                    };
                }

            }
            catch (ExpiredException ex)
            {
                response.Success = false;
                response.Code = "401";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Code = "500";
                response.Message = ex.Message;
            }
            return response.ToHttpResponseAsync();
        }

        /// <summary>
        /// 获取自己朋友圈列表
        /// </summary>
        /// <param name="friendCircleList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FriendCircle/GetFriendCircleList")]
        public Task<HttpResponseMessage> GetFriendCircleList(FriendCircleList friendCircleList)
        {
            ResponseBase<FriendCircleResponse> response = new ResponseBase<FriendCircleResponse>();
            try
            {
                var result = wechat.SnsTimeLine(friendCircleList.WxId, friendCircleList.FristPageMd5);
                if (result == null || result.baseResponse.ret != (int)MMPro.MM.RetConst.MM_OK)
                {
                    response.Success = false;
                    response.Code = "402";
                    response.Message = result.baseResponse.errMsg.@string ?? "获取失败";
                }
                else
                {
                    response.Data = new FriendCircleResponse()
                    {
                        FristPageMd5 = result.fristPageMd5,
                        ObjectList = result.objectList
                    };
                }

            }
            catch (ExpiredException ex)
            {
                response.Success = false;
                response.Code = "401";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Code = "500";
                response.Message = ex.Message;
            }
            return response.ToHttpResponseAsync();
        }

        /// <summary>
        /// 操作朋友圈 1删除朋友圈2设为隐私3设为公开4删除评论5取消点赞
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FriendCircle/SetFriendCircle")]
        public Task<HttpResponseMessage> SetFriendCircle(SetFriendCircle setFriendCircle)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                var result = wechat.GetSnsObjectOp(setFriendCircle.Id, setFriendCircle.WxId, setFriendCircle.Type);
                if (result == null || result.baseResponse.ret != (int)MMPro.MM.RetConst.MM_OK)
                {
                    response.Success = false;
                    response.Code = "402";
                    response.Message = result.baseResponse.errMsg.@string ?? "操作失败";
                }
                else
                {
                    response.Message = "操作成功";
                }

            }
            catch (ExpiredException ex)
            {
                response.Success = false;
                response.Code = "401";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Code = "500";
                response.Message = ex.Message;
            }
            return response.ToHttpResponseAsync();
        }

        /// <summary>
        /// 发送朋友圈
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FriendCircle/SendFriendCircle")]
        public Task<HttpResponseMessage> SendFriendCircle(SendFriendCircle sendFriendCircle)
        {
            ResponseBase<MMPro.MM.SnsObject> response = new ResponseBase<MMPro.MM.SnsObject>();
            try
            {
                string content = null;
                switch (sendFriendCircle.Type)
                {
                    case 0: content = SendSnsConst.GetContentTemplate(sendFriendCircle.Content, sendFriendCircle.Title, sendFriendCircle.ContentUrl, sendFriendCircle.Description); break;
                    case 1: content = SendSnsConst.GetImageTemplate(sendFriendCircle.Content, sendFriendCircle.MediaInfos, sendFriendCircle.Title, sendFriendCircle.ContentUrl, sendFriendCircle.Description); break;
                    case 2: content = SendSnsConst.GetVideoTemplate(sendFriendCircle.Content, sendFriendCircle.MediaInfos, sendFriendCircle.Title, sendFriendCircle.ContentUrl, sendFriendCircle.Description); break;
                    case 3: content = SendSnsConst.GetLinkTemplate(sendFriendCircle.Content, sendFriendCircle.MediaInfos, sendFriendCircle.Title, sendFriendCircle.ContentUrl, sendFriendCircle.Description); break;
                    case 4: content = SendSnsConst.GetImageTemplate3(sendFriendCircle.Content, sendFriendCircle.MediaInfos, sendFriendCircle.Title, sendFriendCircle.ContentUrl, sendFriendCircle.Description); break;
                    case 5: content = SendSnsConst.GetImageTemplate4(sendFriendCircle.Content, sendFriendCircle.MediaInfos, sendFriendCircle.Title, sendFriendCircle.ContentUrl, sendFriendCircle.Description); break;
                    case 6: content = SendSnsConst.GetImageTemplate5(sendFriendCircle.Content, sendFriendCircle.MediaInfos, sendFriendCircle.Title, sendFriendCircle.ContentUrl, sendFriendCircle.Description); break;

                }

                var result = wechat.SnsPost(sendFriendCircle.WxId, content, sendFriendCircle.BlackList, sendFriendCircle.WithUserList);
                if (result == null || result.baseResponse.ret != (int)MMPro.MM.RetConst.MM_OK)
                {
                    response.Success = false;
                    response.Code = "402";
                    response.Message = result.baseResponse.errMsg.@string ?? "发送失败";
                }
                else
                {
                    response.Message = "发送成功";
                    response.Data = result.snsObject;
                }

            }
            catch (ExpiredException ex)
            {
                response.Success = false;
                response.Code = "401";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Code = "500";
                response.Message = ex.Message;
            }
            return response.ToHttpResponseAsync();
        }

        /// <summary>
        /// 同步朋友圈
        /// </summary>
        /// <param name="wxId">微信Id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FriendCircle/SyncFriendCircle/{wxId}")]
        public Task<HttpResponseMessage> SyncFriendCircle(string wxId)
        {

            ResponseBase response = new ResponseBase();
            try
            {
                var result = wechat.SnsSync(wxId);
                if (result == null || result.baseResponse.ret != (int)MMPro.MM.RetConst.MM_OK)
                {
                    response.Success = false;
                    response.Code = "402";
                    response.Message = result.baseResponse.errMsg.@string ?? "同步失败";
                }
                else
                {
                    response.Message = "同步成功";
                }

            }
            catch (ExpiredException ex)
            {
                response.Success = false;
                response.Code = "401";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Code = "500";
                response.Message = ex.Message;
            }
            return response.ToHttpResponseAsync();
        }

        /// <summary>
        /// 上传朋友圈图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FriendCircle/SendFriendCircleImage")]
        public async Task<HttpResponseMessage> SendFriendCircleImage(SendFriendCircleImage sendFriendCircleImage)
        {
            ResponseBase<IList<micromsg.SnsUploadResponse>> response = new ResponseBase<IList<micromsg.SnsUploadResponse>>();
            try
            {
                IList<micromsg.SnsUploadResponse> list = new List<micromsg.SnsUploadResponse>();
                foreach (var item in sendFriendCircleImage.ObjectNames)
                {
                    var buffer = await FileStorageHelper.DownloadToBufferAsync(item);
                    var result = wechat.SnsUpload(sendFriendCircleImage.WxId, new MemoryStream(buffer));
                    if (result == null)
                    {
                        throw new Exception("上传失败");
                    }
                    list.Add(result);
                }
                response.Data = list;
                response.Message = "上传成功";
            }
            catch (ExpiredException ex)
            {
                response.Success = false;
                response.Code = "401";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Code = "500";
                response.Message = ex.Message;
            }
            return await response.ToHttpResponseAsync();
        }


        /// <summary>
        /// 发送评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/FriendCircle/SendFriendCircleComment")]
        public async Task<HttpResponseMessage> SendFriendCircleComment(FriendCircleComment friendCircleComment)
        {
            ResponseBase<micromsg.SnsObject> response = new ResponseBase<micromsg.SnsObject>();
            try
            {
                var result = wechat.SnsComment(Convert.ToUInt64(friendCircleComment.Id), friendCircleComment.WxId, friendCircleComment.WxId, friendCircleComment.ReplyCommnetId, friendCircleComment.Content, (MMPro.MM.SnsObjectType)friendCircleComment.Type);
                if (result == null || result.BaseResponse.Ret != (int)MMPro.MM.RetConst.MM_OK)
                {
                    response.Success = false;
                    response.Code = "402";
                    response.Message = result.BaseResponse.ErrMsg.String ?? "发送失败";
                }
                else
                {
                    response.Data = result.SnsObject;
                    response.Message = "发送成功";
                }
            }
            catch (ExpiredException ex)
            {
                response.Success = false;
                response.Code = "401";
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Code = "500";
                response.Message = ex.Message;
            }
            return await response.ToHttpResponseAsync();
        }



    }
}