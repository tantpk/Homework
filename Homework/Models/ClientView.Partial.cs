namespace Homework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ClientViewMetaData))]
    public partial class ClientView
    {
    }
    
    public partial class ClientViewMetaData
    {
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
        public Nullable<int> 銀行帳戶數量 { get; set; }
    }
}
