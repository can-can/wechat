using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wechat.Api.Request.Message
{


    /// <summary>
    /// 视频消息
    /// </summary>
    public class VideoMessage : RequestBase
    {
        /// <summary>
        /// 发送的微信ID
        /// </summary>
        [Required]
        public IList<string> ToWxIds { get; set; }

        /// <summary>
        /// 播放时长 秒
        /// </summary>
        [Required]
        public int PlayLength { get; set; }
 
        /// <summary>
        /// oss ObjectName
        /// </summary>
        [Required]
        public string ObjectName { get; set; }


        /// <summary>
        /// oss ObjectName
        /// </summary>
        [Required]
        public string ImageObjectName { get; set; }
    }
}