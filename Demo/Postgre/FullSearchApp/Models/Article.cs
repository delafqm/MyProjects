using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FullSearchApp.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(512)]
        public string Abst { get; set; }

        public NpgsqlTsVector TitleVector { get; set; }
        public NpgsqlTsVector AbstVector { get; set; }

        [NotMapped]
        public string TitleHL { get; set; }

        [NotMapped]
        public string AbstHL { get; set; }
    }
}
