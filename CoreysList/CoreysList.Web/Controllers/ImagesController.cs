using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CoreysList.Entity;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Simple.ImageResizer;
using CoreysList.Web.Models;

namespace CoreysList.Web.Controllers
{
    public class ImagesController : Controller
    {
        public ActionResult TestUpload()
        {
            return View("TestUpload");
        }

        public ActionResult Upload(int id)
        {
            int listingId = id;
            CoreysListEntities Db = new CoreysListEntities();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                try
                {
                    CoreysList.Entity.Image newImage = new CoreysList.Entity.Image();
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                    //Use the following properties to get file's name, size and MIMEType
                    newImage.ImageSize = file.ContentLength;
                    newImage.FileName = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                    newImage.ImageType = file.ContentType;
                    byte[] imageData = new byte[file.ContentLength];
                    file.InputStream.Read(imageData, 0, (int)file.ContentLength);
                    //byte[] imageData = null;
                    //using (var binaryReader = new BinaryReader(file.InputStream))
                    //{
                    //    imageData = binaryReader.ReadBytes(file.ContentLength);
                    //}
                    file.InputStream.Position = 0;
                    System.Drawing.Image sysImg = System.Drawing.Image.FromStream(file.InputStream);
                    newImage.ImageHeight = sysImg.Height;
                    newImage.ImageWidth = sysImg.Width;
                    newImage.ImageContent = imageData;

                    ImageEncoding imgResizerEnc = new ImageEncoding();
                    int thumbHeight = 75;
                    int thumbWidth = 75;
                    switch (file.ContentType)
                    {
                        case "image/jpeg":
                            imgResizerEnc = ImageEncoding.Jpg90;
                            break;
                        case "image/gif":
                            imgResizerEnc = ImageEncoding.Gif;
                            break;
                        case "image/png":
                            imgResizerEnc = ImageEncoding.Png;
                            break;

                    }
                    ImageResizer resizer = new ImageResizer(imageData);
                    byte[] thumbData = resizer.Resize(thumbHeight, thumbWidth, imgResizerEnc);
                    newImage.ThumbContent = thumbData;
                    newImage.ThumbSize = thumbData.Length;
                    newImage.ThumbWidth = thumbWidth;
                    newImage.ThumbHeight = thumbHeight;

                    newImage.ListingID = listingId;
                    newImage.CreatedDate = DateTime.Now;
                    newImage.CreatedBy = System.Web.HttpContext.Current.Session["UserId"].ToString();
                    //To save file, use SaveAs method
                    Db.Images.Add(newImage);
                    Db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            EditListingImagesViewModel editListingImagesViewModel = new EditListingImagesViewModel(listingId);
            return PartialView("~/views/Accounts/_EditListingImages.cshtml", editListingImagesViewModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetImage( int id )
        {
            int imageId = id;
            CoreysListEntities Db = new CoreysListEntities();
            CoreysList.Entity.Image image = Db.Images.FirstOrDefault(i => i.ImageID == imageId);
            byte[] buffer = image.ImageContent;
            return File(buffer, image.ImageType, image.FileName);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetThumb(int id)
        {
            int imageId = id;
            CoreysListEntities Db = new CoreysListEntities();
            CoreysList.Entity.Image image = Db.Images.FirstOrDefault(i => i.ImageID == imageId);
            byte[] buffer = image.ThumbContent;
            return File(buffer, image.ImageType, image.FileName);
        }

        public ActionResult Delete(int id)
        {
            CoreysListEntities Db = new CoreysListEntities();

            CoreysList.Entity.Image imgToDelete = Db.Images.FirstOrDefault(i => i.ImageID == id);
            int listingId = imgToDelete.ListingID;
            Db.Images.Remove(imgToDelete);
            Db.SaveChanges();
            EditListingImagesViewModel editListingImagesViewModel = new EditListingImagesViewModel(listingId);
            return PartialView("~/views/Accounts/_EditListingImages.cshtml", editListingImagesViewModel);
        }

    }
}
