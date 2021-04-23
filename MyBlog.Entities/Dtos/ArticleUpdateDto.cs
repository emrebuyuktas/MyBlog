using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Dtos
{
    public class ArticleUpdateDto
    {
        [Required()]
        public int Id { get; set; }//makale idsi

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "Başlık alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MinLength(20, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(250, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string Thumbnail { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]//tarih formatını ayarlıyoruz.
        public DateTime Date { get; set; }

        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "Başlık alanı boş geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string SeoAuthor { get; set; }

        [DisplayName("Seo Açıklama")]
        [Required(ErrorMessage = "Başlık alanı boş geçilmemelidir.")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(0, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Etiketler")]
        [Required(ErrorMessage = "Başlık alanı boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz.")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "Başlık alanı boş geçilmemelidir.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsActive { get; set; }

        [DisplayName("Silinsin mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsDeleted { get; set; }
    }
}
