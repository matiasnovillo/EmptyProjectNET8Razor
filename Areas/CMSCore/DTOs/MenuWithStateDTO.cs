using FiyiStore.Areas.CMSCore.Entities;

namespace FiyiStore.Areas.CMSCore.DTOs
{
    public class MenuWithStateDTO : Menu
    {
        public bool IsSelected { get; set; }
    }
}
