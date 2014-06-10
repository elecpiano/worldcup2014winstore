using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WorldCup2014WinStore.Models;

namespace WorldCup2014WinStore.Controls
{
    public class NewsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NewsTemplate { get; set; }

        public DataTemplate VideoTemplate { get; set; }

        public DataTemplate MoreButtonTemplate { get; set; }

        public DataTemplate DateHeaderTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            News news = item as News;
            DataTemplate template = null;
            if (news != null)
            {
                if (news.IsMoreButton)
                {
                    return MoreButtonTemplate;
                }
                else if (news.IsDateHeader)
                {
                    return DateHeaderTemplate;
                }
                else
                {
                    switch (news.Type)
                    {
                        case "0"://video
                            template = VideoTemplate;
                            break;
                        case "1"://news
                            template = NewsTemplate;
                            break;
                        case "2"://album
                            template = NewsTemplate;
                            break;
                        case "31"://subject
                            template = NewsTemplate;
                            break;
                        case "15"://magma
                            template = NewsTemplate;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                DiaryItem diary = item as DiaryItem;
                if (diary!=null)
                {
                    template = NewsTemplate;
                }
            }

            return template;
        }

    }
}
