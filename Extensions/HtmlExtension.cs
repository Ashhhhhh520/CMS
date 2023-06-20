namespace CMS.Extensions
{
    public static class HtmlExtension
    {
        /// <summary>
        /// 获取wwwroot文件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="subfolder"></param>
        /// <param name="perfix"></param>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GenUrlPath(this int index,string subfolder,string perfix,string folder="imgs",string file="png")
        {
            return $"{folder}/{subfolder}/{perfix}{index}.{file}";
        }
    }
}
