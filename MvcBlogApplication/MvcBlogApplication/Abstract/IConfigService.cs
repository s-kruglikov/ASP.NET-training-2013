
namespace MvcBlog.WebUI.Abstract
{
    public interface IConfigService
    {
        int PostsPerPage { get; set; }

        int PostThumbImageHeight { get; set; }

        int PostThumbImageWidth { get; set; }

        int PostFeaturedImageHeight { get; set; }

        int PostFeaturedImageWidth { get; set; }

        string PostThumbPath { get; set; }

        string PostFeaturedPath { get; set; }

        int AvatarImageHeight { get; set; }

        int AvatarImageWidth { get; set; }

        string AvatarImagePath { get; set; }
    }
}
