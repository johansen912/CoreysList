using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreysList.Entity;
using CoreysList.Web.Models;
using Simple.ImageResizer;

namespace CoreysList.Web.Controllers
{
    public class ImagesController : Controller
    {
        // called when a user uploads images recieving the listing id
        public ActionResult Upload(int id)
        {
            int listingId = id;
            CoreysListEntities db = new CoreysListEntities();

            // for each of the requested files
            for (int i = 0; i < Request.Files.Count; i++)
            {
                try
                {
                    // create a new [image] 
                    CoreysList.Entity.Image newImage = new CoreysList.Entity.Image();

                    // Uploaded file
                    HttpPostedFileBase file = Request.Files[i];

                    // Get the size of the file
                    newImage.ImageSize = file.ContentLength;

                    // get the file name
                    newImage.FileName = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);

                    // get the type of file .jpg .gif .png etc..
                    newImage.ImageType = file.ContentType;

                    // create a new byte array to fit the content size
                    byte[] imageData = new byte[file.ContentLength];

                    // read in the file withe the byte array and content size
                    file.InputStream.Read(imageData, 0, (int)file.ContentLength);

                    // reposition the input stream to the beginning
                    file.InputStream.Position = 0;

                    // stream the file again into a System.Drawing.Image
                    System.Drawing.Image sysImg = System.Drawing.Image.FromStream(file.InputStream);

                    // assign the sizes from system image to coreyslist image
                    newImage.ImageHeight = sysImg.Height;
                    newImage.ImageWidth = sysImg.Width;
                    newImage.ImageContent = imageData;

                    // create encoding object to send image type
                    ImageEncoding imgResizerEnc = new ImageEncoding();

                    // set the values for thumb images
                    int thumbHeight = 75;
                    int thumbWidth = 75;

                    // switch statement to get the content type 
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

                    // create a resizer and send the image content
                    ImageResizer resizer = new ImageResizer(imageData);

                    // call the resizer method along with the desired height, width and img type
                    byte[] thumbData = resizer.Resize(thumbHeight, thumbWidth, imgResizerEnc);

                    // save the new thumb data for the coreyslist image entity
                    newImage.ThumbContent = thumbData;
                    newImage.ThumbSize = thumbData.Length;
                    newImage.ThumbWidth = thumbWidth;
                    newImage.ThumbHeight = thumbHeight;

                    // connect image to the correct listing through listing ID
                    newImage.ListingID = listingId;
                    newImage.CreatedDate = DateTime.Now;
                    newImage.CreatedBy = System.Web.HttpContext.Current.Session["UserId"].ToString();

                    // To save file, use SaveAs method
                    db.Images.Add(newImage);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            // Return partial view 
            EditListingImagesViewModel editListingImagesViewModel = new EditListingImagesViewModel(listingId);
            return PartialView("~/views/Accounts/_EditListingImages.cshtml", editListingImagesViewModel);
        }

        // Used to supply different views with the correct image : ViewListing, ViewListings, DisplaySearchResults etc..
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetImage(int id)
        {
            int imageId = id;

            // if the image id is not = to -1 then there is a real image
            if (imageId != -1)
            {
                // retrieve image from database
                CoreysListEntities db = new CoreysListEntities();
                CoreysList.Entity.Image image = db.Images.FirstOrDefault(i => i.ImageID == imageId);

                // convert image data to byte array
                byte[] buffer = image.ImageContent;

                // return byte[](content), type of image, image filename
                return File(buffer, image.ImageType, image.FileName);
            }
            else
            {
                // return path to default image
                var dir = Server.MapPath("/Content/Images/defaultListingImage.gif");
                var path = Path.Combine(dir);
                return base.File(path, "image/gif");
            }
        }

        // Used to supply different views with the correct thumb-image : ViewListing, ViewListings, DisplaySearchResults etc..
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetThumb(int id)
        {
            // if the image id is not = to -1 then there is a real image
            if (id != -1)
            {
                int imageId = id;

                // retrieve image from database
                CoreysListEntities db = new CoreysListEntities();
                CoreysList.Entity.Image image = db.Images.FirstOrDefault(i => i.ImageID == imageId);

                // convert image data to byte array
                byte[] buffer = image.ThumbContent;

                // return byte[](content), type of image, image filename
                return File(buffer, image.ImageType, image.FileName);
            }
            else
            {
                // return path to default image
                var dir = Server.MapPath("/Content/Images/defaultListingThumb.gif");
                var path = Path.Combine(dir);
                return base.File(path, "image/gif");
            }
        }

        // called when user clicks delete linke in editImages partial
        public ActionResult Delete(int id)
        {
            CoreysListEntities db = new CoreysListEntities();

            // get reference to image
            CoreysList.Entity.Image imgToDelete = db.Images.FirstOrDefault(i => i.ImageID == id);

            // get the listing associated with the image
            int listingId = imgToDelete.ListingID;

            // remove row from table and save
            db.Images.Remove(imgToDelete);
            db.SaveChanges();

            // return partial 
            EditListingImagesViewModel editListingImagesViewModel = new EditListingImagesViewModel(listingId);
            return PartialView("~/views/Accounts/_EditListingImages.cshtml", editListingImagesViewModel);
        }
    }
}
