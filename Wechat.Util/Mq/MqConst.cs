using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wechat.Util.Mq
{
    public class MqConst
    {
        public const string SyncMessageCusomerGroup = "SYNC_WECHAT_MESSAGE_CG";

        public const string UploadOssCusomerGroup = "UPLOAD_FILE_TO_OSS_CG";

        public const string SyncMessageProducerGroup = "SYNC_WECHAT_MESSAGE_PG";

        public const string UploadOssProducerGroup = "UPLOAD_FILE_TO_OSS_PG";

        public const string SyncMessageTopic = "SYNC_WECHAT_MESSAGE_TOPIC";

        public const string UploadOssTopic = "UPLOAD_FILE_TO_OSS_TOPIC";
    }
}
