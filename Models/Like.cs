//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API_SocialAPP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Like
    {
        public int like_id { get; set; }
        public Nullable<int> post_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string user_name { get; set; }
        public Nullable<System.DateTime> date_of_like { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}