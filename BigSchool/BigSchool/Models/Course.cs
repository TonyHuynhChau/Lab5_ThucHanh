namespace BigSchool.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string LectureId { get; set; }

        [Required(ErrorMessage ="Lỗi Ở Đây")]
        [StringLength(255)]
        public string Place { get; set; }
        [Required(ErrorMessage = "Lỗi Ở Đây")]
        public DateTime? DateTime { get; set; }
        [Required(ErrorMessage = "Lỗi Ở Đây")]
        public int? CategoryId { get; set; }

        public List<Category> ListCategory = new List<Category>();

        public virtual Category Category { get; set; }
        public string Name;
    }
}
