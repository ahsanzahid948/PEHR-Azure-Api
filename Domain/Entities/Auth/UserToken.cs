using System;

namespace Domain.Entities.Auth
{
    public class UserToken
    {
        public virtual long Seq_Num { get; set; }
        public virtual long User_Seq_Num { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime Created_Date { get; set; }
        public virtual DateTime Expiry_Date { get; set; }
        public virtual bool IsExpired => DateTime.UtcNow >= Expiry_Date;
        
        public virtual bool IsActive => !IsExpired;
    }
}