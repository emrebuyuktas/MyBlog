using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} adı boş geçilmemelidir")]//{0} yerine üst kısımda verdiğimiz display name gelecek
        [MaxLength(70, ErrorMessage = "{0} {1} karakterlerden büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterlerden küçük olmamalıdır")]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterlerden büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterlerden küçük olmamalıdır")]
        public string Description { get; set; }

        [DisplayName("Kategori not alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterlerden büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterlerden küçük olmamalıdır")]
        public string Note { get; set; }

        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} {boş geçilmemelidir")]
        public bool IsActive { get; set; }

        [DisplayName("Silindi mi?")]
        [Required(ErrorMessage = "{0} {boş geçilmemelidir")]
        public bool IsDeleted { get; set; }
    }
}
