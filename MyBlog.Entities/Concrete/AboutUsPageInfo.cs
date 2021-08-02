using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concrete
{
    public class AboutUsPageInfo
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string Header { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(1500, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string Content { get; set; }

        [DisplayName("Seo Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string SeoTags { get; set; }

        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(60, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string SeoAuthor { get; set; }
    }
}
