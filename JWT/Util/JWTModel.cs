using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Util
{
    public class JWTModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long UId { get; set; }
        /// <summary>
        /// Confirm power
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Work { get; set; }
    }
}
