// -----------------------------------------------------------------------
// <copyright file="MailService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.Net
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    using LumiSoft.Net;
    using LumiSoft.Net.Mime;
    using LumiSoft.Net.SMTP.Client;

    using MailMessage = System.Net.Mail.MailMessage;

    /// <summary>
    /// 发送邮件
    /// </summary>
    public class MailService
    {
        /// <summary>
        /// 调用lumisoft发送邮件
        /// </summary>
        /// <param name="fromEmailAddr">发送者的邮件地址</param>
        /// <param name="toEmailAddr">给谁发的邮件地址</param>
        /// <param name="subjectText">主题</param>
        /// <param name="bodyText">正文</param>
        /// <param name="filePath">附件路径</param>
        /// <returns>成功与否</returns>
        public static bool SendMailByLumisoft(string fromEmailAddr, string toEmailAddr, string subjectText, string bodyText, string filePath)
        {
            Mime m = new Mime();
            MimeEntity mainEntity = m.MainEntity;
            // Force to create From: header field  
            mainEntity.From = new AddressList();
            mainEntity.From.Add(new MailboxAddress(fromEmailAddr, fromEmailAddr));
            // Force to create To: header field  
            mainEntity.To = new AddressList();
            mainEntity.To.Add(new MailboxAddress(toEmailAddr, toEmailAddr));
            mainEntity.Subject = subjectText;
            //添加正文  
            mainEntity.ContentType = MediaType_enum.Multipart_mixed;
            MimeEntity textEntity = mainEntity.ChildEntities.Add();
            textEntity.ContentType = MediaType_enum.Text_html;
            textEntity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
            textEntity.DataText = bodyText;
            //发送附件  
            if (!string.IsNullOrEmpty(filePath))
            {
                MimeEntity attachmentEntity = new MimeEntity();
                attachmentEntity.ContentType = MediaType_enum.Application_octet_stream;
                attachmentEntity.ContentDisposition = ContentDisposition_enum.Attachment;
                attachmentEntity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
                attachmentEntity.ContentDisposition_FileName = filePath;
                attachmentEntity.DataFromFile(filePath);
                mainEntity.ChildEntities.Add(attachmentEntity);
            }
            try
            {
                SMTP_Client.QuickSend(m);
                return true;
            }
            catch (Exception e)
            {
                //Console.Write(e.StackTrace);
                return false;
            }
        }


        public static void Send(string fromEmailAddr,string password, string toEmailAddr, string subjectText, string bodyText,string host,int port)
        {
            // 设置发信人地址 需要密码
            MailAddress AddressFrom = null;

            // 设置收信人地址 不需要密码
            MailAddress AddressTo = null;

            // 邮件信息
            MailMessage Message = null;

            // 设置Smtp协议
            SmtpClient smtpClient = new SmtpClient();

            // 指定Smtp服务名
            // QQ:smtp.qq.com
            // sina：smtp.sina.cn
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.Timeout = 100000;

            // 创建服务器验证
            NetworkCredential networkCreadential_My = new NetworkCredential(fromEmailAddr, password);

            // 实例化发件人地址
            AddressFrom = new MailAddress(fromEmailAddr, fromEmailAddr);

            // 指定发件人信息 邮箱地址和密码
            smtpClient.Credentials = new NetworkCredential(AddressFrom.Address, password);

            // message信息
            Message = new MailMessage();

            // 发信人地址
            Message.From = AddressFrom;

            // 收信人地址
            AddressTo = new MailAddress(toEmailAddr);

            // 添加收信人地址
            Message.To.Add(AddressTo);

            // 信息的主题
            Message.Subject = subjectText;

            // 主题的编码方式
            Message.SubjectEncoding = System.Text.Encoding.UTF8;

            // 邮件正文
            Message.Body = bodyText;

            // 发送
            smtpClient.Send(Message);

        }
    }
}
