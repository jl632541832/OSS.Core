﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OSS.Common.BasicMos;
using OSS.Common.BasicMos.Resp;
using OSS.Core.Infrastructure.Web.Attributes.Auth;
using OSS.Core.Infrastructure.Const;
using OSS.Core.Services.Plugs.File;
using OSS.Core.Services.Plugs.File.Reqs;

namespace OSS.Core.CoreApi.Controllers.Plugs.File
{
    [ModuleName(ModuleNames.File)]
    [Route("p/[controller]/[action]")]
    public class FileController : BaseController
    {
        private static readonly FileService _service = new FileService();

        ///// <summary>
        /////  获取用户上传的图片列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<PageListResp<UploadFileMo>> GetUploadImgs([FromBody] SearchReq req)
        //{
        //    if (req == null)
        //        return new PageListResp<UploadFileMo>().WithResp(RespTypes.ParaError, "参数错误！");
        //    return await _service.GetUploadImgs(req);
        //}

        #region 获取上传参数

        /// <summary>
        ///  获取上传头像的参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Resp<BucketUploadPara> AvatarUploadPara(string name)
        {
            return _service.AvatarUploadPara(name);
        }

        /// <summary>
        ///  获取商品上传的参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Resp<BucketUploadPara> GetGoodsUploadParas(string name)
        {
            return _service.GetGoodsUploadParas(name);
        }

        /// <summary>
        ///  获取商品上传的参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Resp<BucketUploadPara> GetEditorUploadParas(string name)
        {
            return _service.GetEditorUploadParas(name);
        }

        /// <summary>
        ///  获取商品上传的参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Resp<BucketUploadPara> GetConfigUploadParas(string name)
        {
            return _service.GetConfigUploadParas(name);
        }
        #endregion
    }

    [ModuleName(ModuleNames.File)]
    public class FileUploadController : BasePartnerController
    {
        private static readonly UploadService _service = new UploadService();

        /// <summary>
        ///  获取上传所需相关参数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AppPartnerName("FileCenter")]
        public Resp Upload([FromForm] UploadFileReq req)
        {
            if (!ModelState.IsValid)
            {
                return GetInvalidResp();
            }

            return Request.Form.Files.Count == 0 ? GetInvalidResp()
                : _service.Upload(req, Request.Form.Files[0].OpenReadStream());
        }

        //[HttpPost]
        //[AppPartnerName("AliOSS")]
        //public async Task<Resp> ali([FromQuery] string t)
        //{
        //    var token =  t;

        //    var size = Request.Form["size"].FirstOrDefault();
        //    var bucket = Request.Form["bucket"].FirstOrDefault();
        //    var key = Request.Form["object"].FirstOrDefault();
        //    var mime_type = Request.Form["mimeType"].FirstOrDefault();

        //    var mo = new UploadFileMo
        //    {
        //        mime_type = mime_type,
        //        size = size.ToInt32(),
        //        bucket = bucket,
        //        status = CommonStatus.Original
        //    };

        //    return await _service.UploadNotify(mo, token, key);
        //}

    }
}