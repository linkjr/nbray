using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray.Drawing
{
    /// <summary>
    /// 表示生成缩略图的类。
    /// </summary>
    public class Thumbnail
    {
        /// <summary>
        /// 有损压缩。将原图根据目标尺寸比例拉伸。
        /// </summary>
        /// <param name="sourceFileName">源文件路径。</param>
        /// <param name="thumbnaiFileName">缩略图路径。</param>
        /// <param name="thumbnailWidth">缩略图宽度。</param>
        /// <param name="thumbnailHeight">缩略图高度。</param>
        public static void LossyCompress(string sourceFileName, string thumbnaiFileName, int thumbnailWidth, int thumbnailHeight)
        {
            var sourceImage = Image.FromFile(sourceFileName);
            var originalWidth = sourceImage.Width;
            var originalHeight = sourceImage.Height;
            //压缩图片宽高尺寸超出或者等于原图尺寸大小，返回原图
            if (thumbnailWidth >= originalWidth && thumbnailHeight >= originalHeight)
            {
                File.Copy(sourceFileName, thumbnaiFileName, true);
                return;
            }

            using (var bitmap = new Bitmap(thumbnailWidth, thumbnailHeight))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.SmoothingMode = SmoothingMode.HighQuality;

                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.DrawImage(sourceImage, new Rectangle(0, 0, thumbnailWidth, thumbnailHeight));
                }
                bitmap.Save(thumbnaiFileName);
            }
        }

        /// <summary>
        /// 等比例拉伸。压缩后图片不变形，取图片正中部位。
        /// </summary>
        /// <param name="sourceFileName">源文件路径。</param>
        /// <param name="thumbnaiFileName">缩略图路径。</param>
        /// <param name="thumbnailWidth">缩略图宽度。</param>
        /// <param name="thumbnailHeight">缩略图高度。</param>
        public static void ProportionalCompress(string sourceFileName, string thumbnaiFileName, int thumbnailWidth, int thumbnailHeight)
        {
            var sourceImage = Image.FromFile(sourceFileName);
            var originalWidth = sourceImage.Width;
            var originalHeight = sourceImage.Height;
            //压缩图片宽高尺寸超出或者等于原图尺寸大小，返回原图
            if (thumbnailWidth >= originalWidth && thumbnailHeight >= originalHeight)
            {
                File.Copy(sourceFileName, thumbnaiFileName, true);
                return;
            }

            var destRect = new Rectangle();
            var srcRect = new Rectangle();
            //裁剪长度和宽度都小于原始图片尺寸，比例大的一边直接压缩，小的一边根据大的比例压缩后再裁剪
            if (thumbnailWidth < originalWidth && thumbnailHeight < originalHeight)
            {
                destRect.Width = thumbnailWidth;
                destRect.Height = thumbnailHeight;

                //等比例裁剪
                var widthRatio = (double)thumbnailWidth / originalWidth;
                var heightRatio = (double)thumbnailHeight / originalHeight;
                if (widthRatio > heightRatio)
                {
                    srcRect.Width = originalWidth;
                    srcRect.Height = (int)(thumbnailHeight / widthRatio);
                    srcRect.Y = (int)(originalHeight - srcRect.Height) / 2;
                }
                else if (widthRatio < heightRatio)
                {
                    srcRect.Width = (int)(thumbnailWidth / heightRatio);
                    srcRect.Height = originalHeight;
                    srcRect.X = (int)(originalWidth - srcRect.Width) / 2;
                }
                else
                {
                    srcRect.Width = originalWidth;
                    srcRect.Height = originalHeight;
                }
            }
            if (thumbnailWidth >= originalWidth && thumbnailHeight < originalHeight)
            {
                //原图宽度，裁剪高度
                destRect.Width = originalWidth;
                destRect.Height = thumbnailHeight;
                //截取图片位置
                srcRect.Y = (originalHeight - thumbnailHeight) / 2;
                srcRect.Width = originalWidth;
                srcRect.Height = thumbnailHeight;
            }
            if (thumbnailWidth < originalWidth && thumbnailHeight >= originalHeight)
            {
                //裁剪宽度，原图高度
                destRect.Width = thumbnailWidth;
                destRect.Height = originalHeight;
                //截取图片位置
                srcRect.X = (originalWidth - thumbnailWidth) / 2;
                srcRect.Width = thumbnailWidth;
                srcRect.Height = originalHeight;
            }

            using (var bitmap = new Bitmap(destRect.Width, destRect.Height))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.SmoothingMode = SmoothingMode.HighQuality;

                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.DrawImage(sourceImage, destRect, srcRect, GraphicsUnit.Pixel);
                }
                bitmap.Save(thumbnaiFileName);
            }
        }
    }
}
