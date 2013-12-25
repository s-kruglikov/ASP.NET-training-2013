using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MvcBlog.WebUI.Tools
{
    /// <summary>
    /// Class for make preview images
    /// </summary>
    public static class ImagesExtensions
    {



        public static Image ResizeMinimalSqueeze(this Image img, Size maxSize)
        {
            int cropWidth = img.Width, cropHeight = img.Height; //width and height of cropped image

            int xCrop = 0, yCrop = 0; // initial coordinates of crop rectangle (X, Y)
            double widthRatio = 0; //ratio between width of original and cropped image (Ww)
            double heightRatio = 0; //ratio between height of original and cropped image (Hh)

            widthRatio = (double)img.Width / maxSize.Width;
            heightRatio = (double)img.Height / maxSize.Height;

            if (widthRatio > heightRatio)
            {
                cropWidth = (int)(maxSize.Width * heightRatio);
                xCrop = (img.Width - cropWidth) / 2;
            }
            else
            {
                cropHeight = (int)(maxSize.Height * widthRatio);
                yCrop = (img.Height - cropHeight) / 2;
            }

            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(new Rectangle(xCrop, yCrop, cropWidth, cropHeight), bmpImage.PixelFormat);

            return new Bitmap(bmpCrop, maxSize);
        }

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
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                image.Save(savePath);
            }
        }

        public static Image CropImage(this Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static bool SupportedFormat(HttpPostedFileBase file, string allowedFormats)
        {
            foreach (var format in allowedFormats.Split(';'))
            {
                if (file.ContentType == format)
                {
                    return true;
                }

            }
            return false;
        }

        public static bool CheckSize(HttpPostedFileBase file, int maxSize)
        {
            if (file.ContentLength <= maxSize * 1024)
            {
                return true;
            }
            return false;
        }
    }
}