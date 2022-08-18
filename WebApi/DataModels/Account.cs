using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataModels
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Key, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(200)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public bool Active { get; set; }
    }

    public class GroupJob
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        //Staff, Supervisor, Administrator, Cashier
        //Unique
        [Required, MaxLength(100)]
        public string Job { get; set; }

        public bool Active { get; set; }
    }

    public class UserRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }
        public long GroupJobId { get; set; }

        [ForeignKey("UserName")]
        public virtual Account Account { get; set; }

        [ForeignKey("GroupJobId")]
        public virtual GroupJob GroupJob { get; set; }

    }

    //Transaksi roles
    //GroupId   Role
    //2         Category
    //2         Variant
    //2         Product
    //2         Order
    //3         Category
    //3         Variant
    //3         Product
    //4         Order
    //Staff: CatVariantegory, Variant, Product
    //Cashier: OProductrder
    //Supervisor: Staff*, Cashier*
    //Administrator: *
    //public string Role { get; set; }

    public class TransRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long GroupJobId { get; set; }

        [Required, MaxLength(100)]
        public string Role { get; set; }

        [ForeignKey("GroupJobId")]
        public virtual GroupJob GroupJob { get; set; }
    }
}

