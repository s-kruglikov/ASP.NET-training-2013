using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MvcBlog.WebUI.Tools
{
    public static class ImagesExtensions
    {
        public static Image ResizeProportional(this Image image, Size maxSize)
        {
            Size imageResize = new Size();

            // Calculate resized image dimensions

            // if image dimensions are less than max no resize required
            if ((maxSize.Width == 0 || maxSize.Height >= image.Height)
                && (maxSize.Width == 0 || maxSize.Width >= image.Width))
            {
                return image;
            }

            else
            {
                if (maxSize.Height == 0 && maxSize.Width > 0)
                {
                    imageResize.Width = maxSize.Width;
                    imageResize.Height = image.Height * maxSize.Width / image.Width;
                }

                if (maxSize.Height > 0 && maxSize.Width == 0)
                {
                    imageResize.Height = maxSize.Height;
                    imageResize.Width = image.Width * maxSize.Height / image.Height;
                }

                if (maxSize.Height > 0 && maxSize.Width > 0)
                {
                    imageResize.Width = maxSize.Width;
                    imageResize.Height = image.Height * maxSize.Width / image.Width;

                    if (imageResize.Height > maxSize.Height)
                    {
                        imageResize.Height = maxSize.Height;
                        maxSize.Width = image.Width * maxSize.Height / image.Height;
                    }
                }

                return new Bitmap(image, imageResize);
            }
        }

        public static void SaveToFolder(this Image image, string savePath)
        {
            if (image != null)
            {
                ImageFormat format = ImageFormat.Bmp;
                string strExtension = Path.GetExtension(savePath);

                switch (strExtension.ToLower())
                {
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;

                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }

                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                image.Save(savePath, format);
            }
        }

        public static Image CropImage(this Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static bool AllowedFormat(HttpPostedFileBase file, string allowedFormats, int maxSize)
        {
            if (file.ContentLength <= maxSize * 1024)
            {

                foreach (var format in allowedFormats.Split(';'))
                {
                    if (file.ContentType == format)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}