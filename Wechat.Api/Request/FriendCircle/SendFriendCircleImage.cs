using micromsg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Wechat.Api.Request.FriendCircle
{
    /// <summary>
    /// 上传朋友圈图片
    /// </summary>
    public class SendFriendCircleImage : RequestBase
    {
        /// <summary>
        /// oss ObjectName
        /// </summary>
        [Required]
        public IList<string> ObjectNames { get; set; }

    }
}